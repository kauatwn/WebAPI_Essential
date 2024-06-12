using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogDb.API.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [StringLength(250)]
        public string? ImageUrl { get; set; }

        public float Stock { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(Category.Id))]
        public int CategoryId { get; set; }
    }
}
