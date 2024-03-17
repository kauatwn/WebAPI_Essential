using CatalogDb.API.Entities;

namespace CatalogDb.API.Repositories
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetCategories();
        public Category GetCategory(int id);
        public Category Create(Category category);
        public Category Update(Category category);
        public Category Delete(int id);
    }
}
