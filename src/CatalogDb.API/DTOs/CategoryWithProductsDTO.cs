using CatalogDb.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace CatalogDb.API.DTOs
{
    public record CategoryWithProductsDTO(
        int Id,
        [Required][StringLength(80)] string Name,
        [StringLength(250)] string? ImageUrl,
        ICollection<Product> Products);
}
