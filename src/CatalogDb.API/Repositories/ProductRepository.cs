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
            IOrderedQueryable<Product> orderedProducts = GetAll().OrderBy(c => c.Id);

            IQueryable<Product> products = filter.HandleFilter(orderedProducts);

            var pagedProducts = await PagedList<Product>.ToPagedListAsync(products, filter.PageNumber, filter.PageSize);

            if (pagedProducts.Count == 0)
            {
                throw new InvalidOperationException("The list of products is empty");
            }

            return pagedProducts;
        }

        public async Task<PagedList<Product>> GetProductsFilteredByExactPriceAsync(ProductExactPriceFilter filter)
        {
            IOrderedQueryable<Product> orderedProducts = GetAll().OrderBy(p => p.Id);

            IQueryable<Product> filteredProducts = filter.HandleFilter(orderedProducts);

            var pagedProducts = await PagedList<Product>.ToPagedListAsync(filteredProducts, filter.PageNumber, filter.PageSize);

            if (pagedProducts.Count == 0)
            {
                throw new InvalidOperationException($"It does not exist products with the price ${filter.Price}");
            }

            return pagedProducts;
        }

        public async Task<PagedList<Product>> GetProductsFilteredByPriceCriterionAsync(ProductPriceCriterionFilter filter)
        {
            IOrderedQueryable<Product> orderedProducts = GetAll().OrderBy(p => p.Id);

            IQueryable<Product> filteredProducts = filter.HandleFilter(orderedProducts);

            var pagedProducts = await PagedList<Product>.ToPagedListAsync(filteredProducts, filter.PageNumber, filter.PageSize);

            if (pagedProducts.Count == 0)
            {
                throw new InvalidOperationException("No products were found based on the given price criterion");
            }

            return pagedProducts;
        }

        public async Task<PagedList<Product>> GetProductsFilteredByPriceWithCriterionAsync(ProductPriceWithCriterionFilter filter)
        {
            IOrderedQueryable<Product> orderedProducts = GetAll().OrderBy(p => p.Id);

            IQueryable<Product> filteredProducts = filter.HandleFilter(orderedProducts);

            var pagedProducts = await PagedList<Product>.ToPagedListAsync(filteredProducts, filter.PageNumber, filter.PageSize);

            if (pagedProducts.Count == 0)
            {
                throw new InvalidOperationException($"No products were found based on the given price (${filter.Price}) and price criterion");
            }

            return pagedProducts;
        }
    }
}
