using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;

namespace CatalogDb.API.Repositories
{
    public interface ICategoryRepository
    {
        public PagedList<Category> GetPagedCategories(CategoryQueryParameters categoryQueryParameters);
    }
}
