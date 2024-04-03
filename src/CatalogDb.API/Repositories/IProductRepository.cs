using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;

namespace CatalogDb.API.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedList<Product>> GetPagedProductsAsync(ProductQueryParameters productQueryParameters);
        Task<PagedList<Product>> GetProductsFilteredByPriceAsync(ProductPriceFilter productPriceFilter);
    }
}
