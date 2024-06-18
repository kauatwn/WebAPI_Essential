using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;
using CatalogDb.API.Pagination.Filters;
using CatalogDb.API.Pagination.Filters.Products;

namespace CatalogDb.API.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedList<Product>> GetPagedProductsAsync(BaseFilter<Product> filter);
        Task<PagedList<Product>> GetProductsFilteredByExactPriceAsync(ProductExactPriceFilter filter);
        Task<PagedList<Product>> GetProductsFilteredByPriceCriterionAsync(ProductPriceOrderFilter filter);
        Task<PagedList<Product>> GetProductsFilteredByPriceWithCriterionAsync(ProductAdvancedPriceFilter filter);
    }
}
