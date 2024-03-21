using System.ComponentModel.DataAnnotations;

namespace CatalogDb.API.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string ImageUrl { get; set; } = string.Empty;

        public List<Product> Products { get; set; }

        public Category()
        {
            Products = [];
        }
    }
}
