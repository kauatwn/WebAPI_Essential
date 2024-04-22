using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;

namespace CatalogDb.API.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<PagedList<Category>> GetPagedCategoriesAsync(CategoryQueryParameters query);
        Task<PagedList<Category>> GetCategoriesFilteredByNameAsync(CategoryNameFilter filter);
        Task<PagedList<Category>> GetCategoriesWithProductsAsync(CategoryQueryParameters query);
    }
}
