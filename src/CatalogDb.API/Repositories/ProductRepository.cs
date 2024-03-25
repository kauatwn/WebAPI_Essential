using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;

namespace CatalogDb.API.Repositories
{
    public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
    {
        public async Task<PagedList<Product>> GetPagedProductsAsync(ProductQueryParameters productQuery)
        {
            var orderedProducts = GetAll().OrderBy(p => p.Id);
            var pagedProducts = await PagedList<Product>.ToPagedList(orderedProducts, productQuery.PageNumber, productQuery.PageSize);
            if (pagedProducts.Count == 0)
            {
                throw new Exception("List of products not found.");
            }
            return pagedProducts;
        }

        public async Task<PagedList<Product>> GetProductsFilteredByPriceAsync(ProductPriceFilter filter)
        {
            var products = GetAll();
            if (filter.Price.HasValue && !string.IsNullOrEmpty(filter.PriceCriterion))
            {
                if (filter.PriceCriterion.Equals("greater", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(p => p.Price > filter.Price.Value).OrderBy(p => p.Price);
                }
                else if (filter.PriceCriterion.Equals("less", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(p => p.Price < filter.Price.Value).OrderBy(p => p.Price);
                }
                else if (filter.PriceCriterion.Equals("equal", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(p => p.Price == filter.Price.Value).OrderBy(p => p.Price);
                }
            }
            var filteredProducts = await PagedList<Product>.ToPagedList(products.AsQueryable(), filter.PageNumber, filter.PageSize);
            return filteredProducts;
        }
    }
}
