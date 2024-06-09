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
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            bool roleExists = await RoleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                IdentityResult roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));

                if (roleResult.Succeeded)
                {
                    return Ok(new ResponseDTO("Success", $"Role '{roleName}' added successfully!"));
                }
                else
                {
                    return BadRequest(new ResponseDTO("Error", $"Failed to add the new role '{roleName}'!"));
                }
            }

            return Conflict(new ResponseDTO("Error", $"Role '{roleName}' already exists!"));
        }

        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            ApplicationUser? userExists = await UserManager.FindByEmailAsync(email);

            if (userExists != null)
            {
                IdentityResult result = await UserManager.AddToRoleAsync(userExists, roleName);

                if (result.Succeeded)
                {
                    return Ok(new ResponseDTO("Success", $"User '{userExists.Email}' added to the '{roleName}' role!"));
                }
                else
                {
                    return Conflict(new ResponseDTO("Error", $"Failed to add user '{userExists.Email}' to the '{roleName}' role!"));
                }
            }

            return NotFound(new ResponseDTO("Error", "User not found!"));
        }

        [HttpPost]
        [Route("Login")]
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

                foreach (string userRole in userRoles)
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
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            ApplicationUser? userExists = await UserManager.FindByNameAsync(registerDTO.UserName);

            if (userExists != null)
            {
                return Conflict(new ResponseDTO("Error", "User already exists!"));
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
                return BadRequest(new ResponseDTO("Error", "Failed to create the user!"));
            }

            return Ok(new ResponseDTO("Success", "User created successfully!"));
        }

        [HttpPost]
        [Route("RefreshToken")]
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
                return Unauthorized("The provided access or refresh token is invalid or expired!");
            }

            string userName = principal.Identity.Name;

            ApplicationUser? user = await UserManager.FindByNameAsync(userName);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return Unauthorized("The provided access or refresh token is invalid or expired!");
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
        [Route("Revoke/{userName}")]
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
