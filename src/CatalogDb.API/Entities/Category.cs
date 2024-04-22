using System.ComponentModel.DataAnnotations;

namespace CatalogDb.API.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string? Name { get; set; }

        [StringLength(250)]
        public string? ImageUrl { get; set; }

        public ICollection<Product> Products { get; } = [];
    }
}
