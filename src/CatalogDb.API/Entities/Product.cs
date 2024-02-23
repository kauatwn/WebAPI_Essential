namespace CatalogDb.API.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public float Stock { get; set; }
        public DateTime RegistrationDate { get; set; }

        //// Não é necessário explicitar os atributos abaixo, pois o EF Core já realizará o mapeamento do relacionamento
        //// Define relacionamento 1:N
        //public int CategoryId { get; set; } // FK
        //public Category? Category { get; set; } // Product está mapeado para Category
    }
}
