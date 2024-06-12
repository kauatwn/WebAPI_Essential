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
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest(new ResponseDTO("Error", "Role cannot be null or empty!"));
            }

            bool roleExists = await RoleManager.RoleExistsAsync(roleName);

            if (roleExists)
            {
                return Conflict(new ResponseDTO("Error", $"Role '{roleName}' already exists!"));
            }

            IdentityResult roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));

            if (roleResult.Succeeded)
            {
                return Ok(new ResponseDTO("Success", $"Role '{roleName}' added successfully!"));
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO("Error", "Failed to create a role. Please, try again later."));
        }

        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new ResponseDTO("Error", "E-mail cannot be null or empty!"));
            }

            ApplicationUser? userEmail = await UserManager.FindByEmailAsync(email);

            if (userEmail == null)
            {
                return NotFound(new ResponseDTO("Error", "User e-mail not found!"));
            }

            bool roleExists = await RoleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                return NotFound(new ResponseDTO("Error", $"Role '{roleName}' not found!"));
            }

            bool isUserInRole = await UserManager.IsInRoleAsync(userEmail, roleName);

            if (isUserInRole)
            {
                return Conflict(new ResponseDTO("Error", $"User e-mail '{userEmail.Email}' is already in the '{roleName}' role!"));
            }

            IdentityResult result = await UserManager.AddToRoleAsync(userEmail, roleName);

            if (result.Succeeded)
            {
                return Ok(new ResponseDTO("Success", $"User '{userEmail.Email}' added to the '{roleName}' role!"));
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO("Error", "Failed to add user to role. Please, try again later."));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            ApplicationUser? user = await UserManager.FindByNameAsync(loginDTO.UserName);

            if (user == null || !await UserManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return Unauthorized(new ResponseDTO("Error", "Invalid login credentials. Please check your username and password and try again."));
            }

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

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            ApplicationUser? existingUser = await UserManager.FindByEmailAsync(registerDTO.Email);

            if (existingUser != null)
            {
                return Conflict(new ResponseDTO("Error", "E-mail is already in use!"));
            }

            var newUser = new ApplicationUser()
            {
                Email = registerDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDTO.UserName
            };

            IdentityResult createUserResult = await UserManager.CreateAsync(newUser, registerDTO.Password);

            if (createUserResult.Succeeded)
            {
                return Ok(new ResponseDTO("Success", "User created successfully!"));
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO("Error", "Failed to create the user. Please, try again later."));
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
                return Unauthorized(new ResponseDTO("Error", "The provided access or refresh token is invalid or expired!"));
            }

            string? userName = principal.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest(new ResponseDTO("Error", "User name cannot be null or empty!"));
            }

            ApplicationUser? user = await UserManager.FindByNameAsync(userName);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return Unauthorized(new ResponseDTO("Error", "The provided access or refresh token is invalid or expired!"));
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
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("User name cannot be null or empty!");
            }

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
