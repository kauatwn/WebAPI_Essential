using CatalogDb.API.Entities;

namespace CatalogDb.API.Pagination.Filters.Products
{
    public sealed class ProductPriceAndPriceCriterionFilter : BaseFilter<Product>
    {
        public decimal? Price { get; set; }
        public string? PriceCriterion { get; set; }

        public override IQueryable<Product> HandleFilter(IQueryable<Product> filter)
        {
            if (!Price.HasValue || string.IsNullOrEmpty(PriceCriterion))
            {
                return filter;
            }

            if (PriceCriterion.Equals("greater", StringComparison.OrdinalIgnoreCase))
            {
                filter = filter.Where(p => p.Price >= Price.Value)
                               .OrderByDescending(p => p.Price)
                               .ThenBy(p => p.Id);
            }

            if (PriceCriterion.Equals("less", StringComparison.OrdinalIgnoreCase))
            {
                filter = filter.Where(p => p.Price <= Price.Value)
                               .OrderBy(p => p.Price)
                               .ThenBy(p => p.Id);
            }

            return filter;
        }
    }
}
