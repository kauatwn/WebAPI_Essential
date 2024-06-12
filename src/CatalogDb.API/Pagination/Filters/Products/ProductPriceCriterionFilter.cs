using CatalogDb.API.Entities;

namespace CatalogDb.API.Pagination.Filters.Products
{
    public sealed class ProductPriceCriterionFilter : BaseFilter<Product>
    {
        public string? PriceCriterion { get; set; }

        public override IQueryable<Product> HandleFilter(IQueryable<Product> filter)
        {
            if (string.IsNullOrEmpty(PriceCriterion))
            {
                return filter;
            }

            if (PriceCriterion.Equals("greater", StringComparison.OrdinalIgnoreCase))
            {
                filter = filter.OrderByDescending(p => p.Price)
                    .ThenBy(p => p.Id);
            }

            if (PriceCriterion.Equals("less", StringComparison.OrdinalIgnoreCase))
            {
                filter = filter.OrderBy(p => p.Price)
                    .ThenBy(p => p.Id);
            }

            return filter;
        }
    }
}
