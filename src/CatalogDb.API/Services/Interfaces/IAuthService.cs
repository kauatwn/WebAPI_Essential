using CatalogDb.API.DTOs;

namespace CatalogDb.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task CreateRoleAsync(string roleName);
        Task AddUserToRoleAsync(string email, string roleName);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto);
        Task RegisterAsync(RegisterDTO registerDto);
        Task<AuthResponseDTO> RefreshTokenAsync(TokenDTO tokenDto);
        Task RevokeAsync(string loggedInUsername);
    }
}
