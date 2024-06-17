using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;
using CatalogDb.API.Pagination.Filters.Products;

namespace CatalogDb.API.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedList<Product>> GetPagedProductsAsync(int pageNumber, int pageSize);
        Task<PagedList<Product>> GetProductsFilteredByExactPriceAsync(ProductExactPriceFilter filter);
        Task<PagedList<Product>> GetProductsFilteredByPriceCriterionAsync(ProductPriceCriterionFilter filter);
        Task<PagedList<Product>> GetProductsFilteredByPriceWithCriterionAsync(ProductPriceWithCriterionFilter filter);
    }
}
