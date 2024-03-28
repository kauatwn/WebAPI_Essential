using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(ITokenService tokenService, UserManager<UserApplication> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : ControllerBase
    {
        private readonly ITokenService _tokenService = tokenService;
        private readonly UserManager<UserApplication> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IConfiguration _config = config;

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.UserName!),
                    new(ClaimTypes.Email, user.Email!),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenService.GenerateAccessToken(authClaims, _config);
                var refreshToken = _tokenService.GenerateRefreshToken();
                _ = int.TryParse(_config["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);
                user.RefreshToken = refreshToken;
                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiry = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var userExists = await _userManager.FindByNameAsync(registerDTO.UserName);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO("Error", "User already exists!"));
            }

            var user = new UserApplication()
            {
                Email = registerDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDTO.UserName
            };

            var createUserResult = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!createUserResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO("Error", "The user creation failed!"));
            }
            return Ok(new ResponseDTO("Success", "User created successfully!"));
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenDTO tokenDTO)
        {
            if (tokenDTO == null)
            {
                return BadRequest("Invalid client request!");
            }

            string accessToken = tokenDTO.AccessToken ?? throw new ArgumentNullException(nameof(tokenDTO));
            string refreshToken = tokenDTO.RefreshToken ?? throw new ArgumentNullException(nameof(tokenDTO));

            var principal = _tokenService.GetClaimsPrincipalFromExpiredToken(accessToken, _config);
            if (principal == null)
            {
                return BadRequest("The provided access or refresh token is invalid or expired!");
            }

            string userName = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("The provided access or refresh token is invalid or expired!");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _config);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken,
            });
        }
    }
}
