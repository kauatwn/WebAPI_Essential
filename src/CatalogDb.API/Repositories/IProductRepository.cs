using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;

namespace CatalogDb.API.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<PagedList<Product>> GetPagedProductsAsync(ProductQueryParameters productQueryParameters);
        public Task<PagedList<Product>> GetProductsFilteredByPriceAsync(ProductPriceFilter productPriceFilter);
    }
}
