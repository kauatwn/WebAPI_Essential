using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;

namespace CatalogDb.API.Repositories
{
    public class CategoryRepository(AppDbContext context) : Repository<Category>(context), ICategoryRepository
    {
        public async Task<PagedList<Category>> GetPagedCategoriesAsync(CategoryQueryParameters categoryQuery)
        {
            IOrderedQueryable<Category> orderedCategories = GetAll().OrderBy(c => c.Id);

            PagedList<Category> pagedCategoryList = await PagedList<Category>.ToPagedList(orderedCategories, categoryQuery.PageNumber, categoryQuery.PageSize);

            if (pagedCategoryList.Count == 0)
            {
                throw new InvalidOperationException("List of categories not found.");
            }

            return pagedCategoryList;
        }

        public async Task<PagedList<Category>> GetCategoriesFilteredByNameAsync(CategoryNameFilter filter)
        {
            IQueryable<Category> categories = GetAll();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                categories = categories.Where(c => c.Name.Contains(filter.Name));
            }

            PagedList<Category> filteredCategories = await PagedList<Category>.ToPagedList(categories, filter.PageNumber, filter.PageSize);

            return filteredCategories;
        }
    }
}
