namespace CatalogDb.API.Pagination
{
    public sealed class CategoryNameFilter : QueryStringParameters
    {
        public string? Name { get; set; }
    }
}
