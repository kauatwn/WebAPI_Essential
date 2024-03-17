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
            return _context.Categories.AsNoTracking().ToList();
        }

        public Category GetCategory(int id)
        {
           return _context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == id);
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
