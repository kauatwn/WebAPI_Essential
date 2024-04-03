using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;

namespace CatalogDb.API.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<PagedList<Category>> GetPagedCategoriesAsync(CategoryQueryParameters categoryQueryParameters);
        Task<PagedList<Category>> GetCategoriesFilteredByNameAsync(CategoryNameFilter categoryNameFilter);
    }
}
