namespace CatalogDb.API.DTOs
{
    public record AuthResponseDTO(string Token, string RefreshToken, DateTime Expiry);
}
