using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;
using CatalogDb.API.Pagination.Filters;
using CatalogDb.API.Pagination.Filters.Categories;

namespace CatalogDb.API.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<PagedList<Category>> GetPagedCategoriesAsync(BaseFilter<Category> filter);
        Task<PagedList<Category>> GetCategoriesFilteredByNameAsync(CategoryNameFilter filter);
    }
}
