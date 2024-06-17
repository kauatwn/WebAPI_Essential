using CatalogDb.API.Entities;

namespace CatalogDb.API.Pagination.Filters.Categories
{
    public sealed class CategoryNameFilter : BaseFilter<Category>
    {
        public string? Name { get; set; }

        public override IQueryable<Category> HandleFilter(IQueryable<Category> filter)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return base.HandleFilter(filter);
            }

            return filter.Where(c => c.Name.Contains(Name));
        }
    }
}
