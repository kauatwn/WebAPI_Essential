using CatalogDb.API.Entities;

namespace CatalogDb.API.Pagination.Filters.Products
{
    public sealed class ProductPriceCriterionFilter : BaseFilter<Product>
    {
        public string? Criterion { get; set; }

        public override IQueryable<Product> HandleFilter(IQueryable<Product> filter)
        {
            if (string.IsNullOrEmpty(Criterion))
            {
                return base.HandleFilter(filter);
            }

            if (Criterion.Equals("greater", StringComparison.OrdinalIgnoreCase))
            {
                filter = filter.OrderByDescending(p => p.Price)
                    .ThenBy(p => p.Id);
            }

            if (Criterion.Equals("less", StringComparison.OrdinalIgnoreCase))
            {
                filter = filter.OrderBy(p => p.Price)
                    .ThenBy(p => p.Id);
            }

            return filter;
        }
    }
}
