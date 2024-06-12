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
            IOrderedQueryable<Category> orderedCategories = GetAll()
                .OrderBy(c => c.Id);

            PagedList<Category> pagedCategoryList = await PagedList<Category>.ToPagedList(orderedCategories, filter.PageNumber, filter.PageSize);

            if (pagedCategoryList.Count == 0)
            {
                throw new InvalidOperationException("The list of products is empty");
            }

            return pagedCategoryList;
        }

        public async Task<PagedList<Category>> GetCategoriesFilteredByNameAsync(CategoryNameFilter filter)
        {
            IQueryable<Category> categories = GetAll();

            IQueryable<Category> filteredCategories = filter.HandleFilter(categories);

            var pagedCategory = await PagedList<Category>.ToPagedList(filteredCategories, filter.PageNumber, filter.PageSize);

            return pagedCategory;
        }
    }
}
