namespace CatalogDb.API.DTOs
{
    public record ProductDTO(int Id, string? Name, string? Description, decimal Price, string? ImageUrl, int CategoryId);
}
