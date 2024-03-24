using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;

namespace CatalogDb.API.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<PagedList<Category>> GetPagedCategoriesAsync(CategoryQueryParameters categoryQueryParameters);
        public Task<PagedList<Category>> GetCategoriesFilteredByNameAsync(CategoryNameFilter categoryNameFilter);
    }
}
