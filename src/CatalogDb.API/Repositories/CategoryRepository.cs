using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;

namespace CatalogDb.API.Repositories
{
    public class CategoryRepository(AppDbContext context) : Repository<Category>(context), ICategoryRepository
    {
        public PagedList<Category> GetPagedCategories(CategoryQueryParameters categoryQuery)
        {
            var categories = GetAll().OrderBy(p => p.Id).AsQueryable();
            var pagedCategoryList = PagedList<Category>.ToPagedList(categories, categoryQuery.PageNumber, categoryQuery.PageSize);
            if (pagedCategoryList.Count == 0)
            {
                throw new Exception("List of categories not found.");
            }
            return pagedCategoryList;
        }

        public PagedList<Category> GetCategoriesFilteredByName(CategoryNameFilter filter)
        {
            var categories = GetAll();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                categories = categories.Where(c => c.Name.Contains(filter.Name));
            }
            var filteredCategories = PagedList<Category>.ToPagedList(categories, filter.PageNumber, filter.PageSize);
            return filteredCategories;
        }
    }
}
