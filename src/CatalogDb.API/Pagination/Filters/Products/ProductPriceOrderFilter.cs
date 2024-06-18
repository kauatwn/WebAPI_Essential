using CatalogDb.API.Entities;

namespace CatalogDb.API.Pagination.Filters.Products
{
    public sealed class ProductPriceOrderFilter : BaseFilter<Product>
    {
        public string? Criterion { get; set; }

        public override IQueryable<Product> HandleFilter(IQueryable<Product> filter)
        {
            if (!string.IsNullOrEmpty(Criterion))
            {
                IQueryable<Product> orderedByCriterion = Criterion.ToLower() switch
                {
                    "greater" => filter.OrderByDescending(p => p.Price).ThenBy(p => p.Id),
                    "less" => filter.OrderBy(p => p.Price).ThenBy(p => p.Id),
                    _ => filter
                };

                return orderedByCriterion;
            }

            return base.HandleFilter(filter);
        }
    }
}
