using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;

namespace CatalogDb.API.Repositories
{
    public interface IProductRepository
    {
        public PagedList<Product> GetPagedProducts(ProductQueryParameters productQueryParameters);
    }
}
