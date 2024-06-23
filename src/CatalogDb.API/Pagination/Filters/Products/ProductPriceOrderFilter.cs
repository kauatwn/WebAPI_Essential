using CatalogDb.API.Entities;

namespace CatalogDb.API.Pagination.Filters.Products
{
    public sealed class ProductPriceOrderFilter : PaginationFilter<Product>
    {
        public string? Criterion { get; set; }

        public override IQueryable<Product> HandleFilter(IQueryable<Product> source)
        {
            if (!string.IsNullOrEmpty(Criterion))
            {
                IQueryable<Product> orderedByCriterion = Criterion.ToLower() switch
                {
                    "greater" => source.OrderByDescending(p => p.Price).ThenBy(p => p.Id),
                    "less" => source.OrderBy(p => p.Price).ThenBy(p => p.Id),
                    _ => source
                };

                return base.HandleFilter(orderedByCriterion);
            }

            return base.HandleFilter(source);
        }
    }
}
