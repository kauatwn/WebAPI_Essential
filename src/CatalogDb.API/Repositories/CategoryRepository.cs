using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;
using CatalogDb.API.Pagination.Filters;
using CatalogDb.API.Pagination.Filters.Categories;

namespace CatalogDb.API.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Category>> GetPagedCategoriesAsync(BaseFilter<Category> filter)
        {
            IOrderedQueryable<Category> orderedCategories = GetAll().OrderBy(c => c.Id);

            IQueryable<Category> categories = filter.HandleFilter(orderedCategories);

            var pagedCategories = await PagedList<Category>.ToPagedListAsync(categories, filter.PageNumber, filter.PageSize);

            if (pagedCategories.Count == 0)
            {
                throw new InvalidOperationException("The list of categories is empty");
            }

            return pagedCategories;
        }

        public async Task<PagedList<Category>> GetCategoriesFilteredByNameAsync(CategoryNameFilter filter)
        {
            IOrderedQueryable<Category> orderedCategories = GetAll().OrderBy(c => c.Id);

            IQueryable<Category> filteredCategories = filter.HandleFilter(orderedCategories);

            var pagedCategories = await PagedList<Category>.ToPagedListAsync(filteredCategories, filter.PageNumber, filter.PageSize);

            return pagedCategories;
        }
    }
}
