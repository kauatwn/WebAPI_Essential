using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Exceptions;
using CatalogDb.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CatalogDb.API.Services
{
    public class AuthService : IAuthService
    {
        private ITokenService TokenService { get; }
        private UserManager<ApplicationUser> UserManager { get; }
        private RoleManager<IdentityRole> RoleManager { get; }
        private IConfiguration Config { get; }

        public AuthService(ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            TokenService = tokenService;
            UserManager = userManager;
            RoleManager = roleManager;
            Config = config;
        }

        public async Task CreateRoleAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            bool roleExists = await RoleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                throw new RoleAlreadyExistsException($"Role '{roleName}' already exists");
            }

            IdentityResult roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
            if (!roleResult.Succeeded)
            {
                throw new RoleCreationFailedException("Failed to create a role. Please, try again later");
            }
        }

        public async Task AddUserToRoleAsync(string email, string roleName)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            ApplicationUser? userEmail = await UserManager.FindByEmailAsync(email);
            if (userEmail == null)
            {
                throw new ResourceNotFoundException(nameof(userEmail));
            }

            bool roleExists = await RoleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                throw new ResourceNotFoundException($"Role '{roleName}' not found");
            }

            bool isUserInRole = await UserManager.IsInRoleAsync(userEmail, roleName);
            if (isUserInRole)
            {
                throw new UserAlreadyInRoleException($"User e-mail '{userEmail.Email}' is already in the '{roleName}' role");
            }

            IdentityResult roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
            if (!roleResult.Succeeded)
            {
                throw new RoleCreationFailedException("Failed to create a role. Please, try again later");
            }
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto)
        {
            ApplicationUser? user = await UserManager.FindByNameAsync(loginDto.UserName);

            if (user == null || !await UserManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new AuthenticationException("Invalid login credentials. Please check your username and password and try again");
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

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            DateTime tokenExpiry = token.ValidTo;

            return new AuthResponseDTO(accessToken, refreshToken, tokenExpiry);
        }

        public async Task RegisterAsync(RegisterDTO registerDto)
        {
            ApplicationUser? userEmail = await UserManager.FindByEmailAsync(registerDto.Email);

            if (userEmail != null)
            {
                throw new EmailAlreadyInUseException("E-mail is already in use");
            }

            var newUser = new ApplicationUser()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.UserName
            };

            IdentityResult createUserResult = await UserManager.CreateAsync(newUser, registerDto.Password);
            if (!createUserResult.Succeeded)
            {
                throw new UserCreationFailedException("Failed to create a user. Please, try again later");
            }
        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(TokenDTO tokenDto)
        {
            ArgumentNullException.ThrowIfNull(nameof(tokenDto));

            string accessToken = tokenDto.AccessToken;
            string refreshToken = tokenDto.RefreshToken;

            ClaimsPrincipal principal = TokenService.GetClaimsPrincipalFromExpiredToken(accessToken, Config)
                ?? throw new SecurityTokenException("Failed to get valid claims principal from the access token");

            string? loggedInUsername = principal.Identity?.Name;
            if (string.IsNullOrEmpty(loggedInUsername))
            {
                throw new InvalidOperationException("Failed to retrieve the username from the claims principal");
            }

            ApplicationUser? user = await UserManager.FindByNameAsync(loggedInUsername)
                ?? throw new ResourceNotFoundException("User not found");

            if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new UnauthorizedAccessException("Refresh token validation failed");
            }

            JwtSecurityToken newAccessToken = TokenService.GenerateAccessToken(principal.Claims.ToList(), Config);
            refreshToken = TokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            await UserManager.UpdateAsync(user);

            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
            DateTime tokenExpiry = newAccessToken.ValidTo;

            return new AuthResponseDTO(accessToken, refreshToken, tokenExpiry);
        }

        public async Task RevokeAsync(string loggedInUsername)
        {
            ArgumentNullException.ThrowIfNull(nameof(loggedInUsername));

            ApplicationUser? user = await UserManager.FindByNameAsync(loggedInUsername)
                ?? throw new ResourceNotFoundException("User not found");

            user.RefreshToken = null;

            await UserManager.UpdateAsync(user);
        }
    }
}
