using CatalogDb.API.Entities;

namespace CatalogDb.API.Repositories
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetProducts();
        public Product GetProduct(int id);
        public Product Create(Product product);
        public Product Update(Product product);
        public Product Delete(int id);
    }
}
