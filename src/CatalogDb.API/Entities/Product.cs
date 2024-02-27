using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogDb.API.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string? Name { get; set; }

        [Required]
        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        public float Stock { get; set; }
        public DateTime RegistrationDate { get; set; }

        //// Não é necessário explicitar os atributos abaixo, pois o EF Core já realizará o mapeamento do relacionamento
        //// Define relacionamento 1:N
        //public int CategoryId { get; set; } // FK
        //public Category? Category { get; set; } // Product está mapeado para Category
    }
}
