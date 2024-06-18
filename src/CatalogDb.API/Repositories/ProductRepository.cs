using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;
using CatalogDb.API.Pagination.Filters;
using CatalogDb.API.Pagination.Filters.Products;

namespace CatalogDb.API.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Product>> GetPagedProductsAsync(BaseFilter<Product> filter)
        {
            return await HandleFilterAndPaginate(filter);
        }

        public async Task<PagedList<Product>> GetProductsFilteredByExactPriceAsync(ProductExactPriceFilter filter)
        {
            return await HandleFilterAndPaginate(filter);
        }

        public async Task<PagedList<Product>> GetProductsFilteredByPriceCriterionAsync(ProductPriceOrderFilter filter)
        {
            return await HandleFilterAndPaginate(filter);
        }

        public async Task<PagedList<Product>> GetProductsFilteredByPriceWithCriterionAsync(ProductAdvancedPriceFilter filter)
        {
            return await HandleFilterAndPaginate(filter);
        }

        private async Task<PagedList<Product>> HandleFilterAndPaginate(BaseFilter<Product> filter)
        {
            IOrderedQueryable<Product> orderedProducts = GetAll().OrderBy(c => c.Id);

            if (!orderedProducts.Any())
            {
                throw new InvalidOperationException("The list of products is empty");
            }

            IQueryable<Product> query = filter.HandleFilter(orderedProducts);

            var pagedProducts = await PagedList<Product>.ToPagedListAsync(query, filter.PageNumber, filter.PageSize);

            return pagedProducts;
        }
    }
}
