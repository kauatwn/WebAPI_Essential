using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;
using CatalogDb.API.Pagination.Filters;
using CatalogDb.API.Pagination.Filters.Products;

namespace CatalogDb.API.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedList<Product>> GetPagedProductsAsync(BaseFilter<Product> filter);
        Task<PagedList<Product>> GetProductsFilteredByExactPrice(ProductExactPriceFilter filter);
        Task<PagedList<Product>> GetProductsFilteredByPriceCriterion(ProductPriceCriterionFilter filter);
    }
}
