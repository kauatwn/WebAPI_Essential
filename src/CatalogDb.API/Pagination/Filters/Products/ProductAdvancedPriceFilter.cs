using CatalogDb.API.Entities;

namespace CatalogDb.API.Pagination.Filters.Products
{
    public sealed class ProductAdvancedPriceFilter : PaginationFilter<Product>
    {
        public decimal? Price { get; set; }
        public string? Criterion { get; set; }

        public override IQueryable<Product> HandleFilter(IQueryable<Product> source)
        {
            if (Price.HasValue && string.IsNullOrEmpty(Criterion))
            {
                IQueryable<Product> sortedByExactPrice = source.Where(p => p.Price == Price.Value)
                    .OrderBy(p => p.Id);

                return base.HandleFilter(sortedByExactPrice);
            }

            if (Price.HasValue && !string.IsNullOrEmpty(Criterion))
            {
                IQueryable<Product> orderedByCriterionAccordingToPrice = Criterion.ToLower() switch
                {
                    "greater" => source.Where(p => p.Price > Price.Value).OrderByDescending(p => p.Price).ThenBy(p => p.Id),
                    "less" => source.Where(p => p.Price < Price.Value).OrderBy(p => p.Price).ThenBy(p => p.Id),
                    _ => source
                };

                return base.HandleFilter(orderedByCriterionAccordingToPrice);
            }

            if (!Price.HasValue && !string.IsNullOrEmpty(Criterion))
            {
                IQueryable<Product> orderedByCriterion = Criterion.ToLower() switch
                {
                    "greater" => source.OrderByDescending(p => p.Price).ThenBy(p => p.Id),
                    "less" => source = source.OrderBy(p => p.Price).ThenBy(p => p.Id),
                    _ => source
                };

                return base.HandleFilter(orderedByCriterion);
            }

            return base.HandleFilter(source);
        }
    }
}
