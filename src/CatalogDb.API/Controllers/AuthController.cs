using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ITokenService TokenService { get; }
        private UserManager<ApplicationUser> UserManager { get; }
        private RoleManager<IdentityRole> RoleManager { get; }
        private IConfiguration Config { get; }

        public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            TokenService = tokenService;
            UserManager = userManager;
            RoleManager = roleManager;
            Config = config;
        }

        [HttpPost]
        [Route("create-role")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            bool roleExists = await RoleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                IdentityResult roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));

                if (roleResult.Succeeded)
                {
                    return Ok(new ResponseDTO("Success", $"Role {roleName} added successfuly!"));
                }
                else
                {
                    return BadRequest(new ResponseDTO("Error", $"Issue adding the new {roleName} role!"));
                }
            }

            return BadRequest(new ResponseDTO("Error", "Role already exists!"));
        }

        [HttpPost]
        [Route("add-user-to-role")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            ApplicationUser? user = await UserManager.FindByNameAsync(email);

            if (user != null)
            {
                IdentityResult result = await UserManager.AddToRoleAsync(user, roleName);

                if (result.Succeeded)
                {
                    return Ok(new ResponseDTO("Success", $"User {user.Email} added to the {roleName} role!"));
                }
                else
                {
                    return BadRequest(new ResponseDTO("Error", $"Unable to add user {user.Email} to the {roleName} role!"));
                }
            }

            return BadRequest("Unable to find user!");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            ApplicationUser? user = await UserManager.FindByNameAsync(loginDTO.UserName);

            if (user != null && await UserManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                IList<string> userRoles = await UserManager.GetRolesAsync(user);

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

                JwtSecurityToken token = TokenService.GenerateAccessToken(authClaims, Config);

                string refreshToken = TokenService.GenerateRefreshToken();
                _ = int.TryParse(Config["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);
                user.RefreshToken = refreshToken;

                await UserManager.UpdateAsync(user);

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
            ApplicationUser? userExists = await UserManager.FindByNameAsync(registerDTO.UserName);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO("Error", "User already exists!"));
            }

            var user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDTO.UserName
            };

            IdentityResult createUserResult = await UserManager.CreateAsync(user, registerDTO.Password);

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

            ClaimsPrincipal principal = TokenService.GetClaimsPrincipalFromExpiredToken(accessToken, Config);

            if (principal == null)
            {
                return BadRequest("The provided access or refresh token is invalid or expired!");
            }

            string userName = principal.Identity.Name;

            ApplicationUser? user = await UserManager.FindByNameAsync(userName);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("The provided access or refresh token is invalid or expired!");
            }

            JwtSecurityToken newAccessToken = TokenService.GenerateAccessToken(principal.Claims.ToList(), Config);
            string newRefreshToken = TokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await UserManager.UpdateAsync(user);

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{userName}")]
        public async Task<IActionResult> Revoke(string userName)
        {
            ApplicationUser? user = await UserManager.FindByNameAsync(userName);

            if (user == null)
            {
                return BadRequest("Ivalid user name!");
            }

            user.RefreshToken = null;

            await UserManager.UpdateAsync(user);

            return NoContent();
        }
    }
}
