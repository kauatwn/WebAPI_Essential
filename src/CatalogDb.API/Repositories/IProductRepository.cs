using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;

namespace CatalogDb.API.Repositories
{
    public interface IProductRepository
    {
        public PagedList<Product> GetProducts(ProductQueryParams productQuery);
        public Product GetProduct(int id);
        public Product Create(Product product);
        public Product Update(Product product);
        public Product Delete(int id);
    }
}
