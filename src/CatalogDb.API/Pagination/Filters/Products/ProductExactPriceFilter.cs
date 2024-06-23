using CatalogDb.API.Entities;

namespace CatalogDb.API.Pagination.Filters.Products
{
    public sealed class ProductExactPriceFilter : PaginationFilter<Product>
    {
        public decimal? Price { get; set; }

        public override IQueryable<Product> HandleFilter(IQueryable<Product> source)
        {
            if (Price.HasValue)
            {
                IQueryable<Product> sortedByExactPrice = source.Where(p => p.Price == Price.Value)
                    .OrderBy(p => p.Id);

                return base.HandleFilter(sortedByExactPrice);
            }

            return base.HandleFilter(source);
        }
    }
}
