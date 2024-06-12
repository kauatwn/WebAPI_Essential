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
            IOrderedQueryable<Product> orderedProducts = GetAll()
                .OrderBy(p => p.Id);

            var pagedProducts = await PagedList<Product>.ToPagedList(orderedProducts, filter.PageNumber, filter.PageSize);

            if (pagedProducts.Count == 0)
            {
                throw new InvalidOperationException("The list of products is empty");
            }

            return pagedProducts;
        }

        public async Task<PagedList<Product>> GetProductsFilteredByExactPrice(ProductExactPriceFilter filter)
        {
            IQueryable<Product> products = GetAll();

            IQueryable<Product> filteredProducts = filter.HandleFilter(products);

            var pagedProducts = await PagedList<Product>.ToPagedList(filteredProducts, filter.PageNumber, filter.PageSize);

            if (pagedProducts.Count == 0)
            {
                throw new InvalidOperationException($"It does not exist products with the price ${filter.Price}");
            }

            return pagedProducts;
        }
    }
}
