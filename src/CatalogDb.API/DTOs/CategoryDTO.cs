using System.ComponentModel.DataAnnotations;

namespace CatalogDb.API.DTOs
{
    public record CategoryDTO(
        int Id,
        [Required][StringLength(80)] string Name,
        [StringLength(250)] string? ImageUrl);
}
