using CatalogDb.API.Entities;

namespace CatalogDb.API.Pagination.Filters.Products
{
    public sealed class ProductExactPriceFilter : BaseFilter<Product>
    {
        public decimal? Price { get; set; }

        public override IQueryable<Product> HandleFilter(IQueryable<Product> filter)
        {
            if (Price.HasValue)
            {
                IQueryable<Product> sortedByExactPrice = filter.Where(p => p.Price == Price.Value)
                    .OrderBy(p => p.Id);

                return sortedByExactPrice;
            }

            return base.HandleFilter(filter);
        }
    }
}
