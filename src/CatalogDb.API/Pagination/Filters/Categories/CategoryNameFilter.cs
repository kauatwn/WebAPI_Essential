using CatalogDb.API.Entities;

namespace CatalogDb.API.Pagination.Filters.Categories
{
    public sealed class CategoryNameFilter : PaginationFilter<Category>
    {
        public string? Name { get; set; }

        public override IQueryable<Category> HandleFilter(IQueryable<Category> source)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                IQueryable<Category> sortedByName = source.Where(c => c.Name.Contains(Name))
                    .OrderBy(c => c.Id);

                return base.HandleFilter(sortedByName);
            }

            return base.HandleFilter(source);
        }
    }
}
