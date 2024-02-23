namespace CatalogDb.API.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }

        // Define relacionamento N:1
        public List<Product>? Products { get; set; } // Category pode ter uma coleção de Product

        public Category()
        {
            Products = [];
        }
    }
}
