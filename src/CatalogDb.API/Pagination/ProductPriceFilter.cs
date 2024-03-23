namespace CatalogDb.API.Pagination
{
    public sealed class ProductPriceFilter : QueryStringParameters
    {
        public decimal? Price { get; set; }
        public string? PriceCriterion { get; set; }
    }
}
