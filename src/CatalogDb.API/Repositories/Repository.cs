using CatalogDb.API.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatalogDb.API.Repositories
{
    public class Repository<T>(AppDbContext context) : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context = context;

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }
    }
}
