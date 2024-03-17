using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb.API.Repositories
{
    public class CategoryRepository(AppDbContext context) : ICategoryRepository
    {
        private readonly AppDbContext _context = context;

        public IEnumerable<Category> GetCategories()
        {
            var categories = _context.Categories.AsNoTracking().ToList();

            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }
            return categories;
        }

        public Category GetCategory(int id)
        {
           var category = _context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            return category;
        }
        public Category Create(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Update(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return category;
        }

        public Category Delete(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return category;
        }
    }
}
