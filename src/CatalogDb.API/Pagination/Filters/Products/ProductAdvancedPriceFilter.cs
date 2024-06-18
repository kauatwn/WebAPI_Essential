using CatalogDb.API.Entities;

namespace CatalogDb.API.Pagination.Filters.Products
{
    public sealed class ProductAdvancedPriceFilter : BaseFilter<Product>
    {
        public decimal? Price { get; set; }
        public string? Criterion { get; set; }

        public override IQueryable<Product> HandleFilter(IQueryable<Product> filter)
        {
            if (Price.HasValue && string.IsNullOrEmpty(Criterion))
            {
                IQueryable<Product> sortedByExactPrice = filter.Where(p => p.Price == Price.Value)
                    .OrderBy(p => p.Id);

                return sortedByExactPrice;
            }

            if (Price.HasValue && !string.IsNullOrEmpty(Criterion))
            {
                IQueryable<Product> orderedByCriterionAccordingToPrice = Criterion.ToLower() switch
                {
                    "greater" => filter.Where(p => p.Price > Price.Value).OrderByDescending(p => p.Price).ThenBy(p => p.Id),
                    "less" => filter.Where(p => p.Price < Price.Value).OrderBy(p => p.Price).ThenBy(p => p.Id),
                    _ => filter
                };

                return orderedByCriterionAccordingToPrice;
            }

            if (!Price.HasValue && !string.IsNullOrEmpty(Criterion))
            {
                IQueryable<Product> orderedByCriterion = Criterion.ToLower() switch
                {
                    "greater" => filter.OrderByDescending(p => p.Price).ThenBy(p => p.Id),
                    "less" => filter = filter.OrderBy(p => p.Price).ThenBy(p => p.Id),
                    _ => filter
                };

                return orderedByCriterion;
            }

            return base.HandleFilter(filter);
        }
    }
}
