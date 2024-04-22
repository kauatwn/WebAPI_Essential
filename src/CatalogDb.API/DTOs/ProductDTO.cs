using System.ComponentModel.DataAnnotations;

namespace CatalogDb.API.DTOs
{
    public record ProductDTO(
        int Id,
        [Required][StringLength(80)] string Name,
        [Required][StringLength(200)] string Description,
        [Required][Range(0, double.MaxValue)] decimal Price,
        [StringLength(250)] string? ImageUrl,
        [Required][Range(1, int.MaxValue)] int CategoryId);
}
