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
    }
}
