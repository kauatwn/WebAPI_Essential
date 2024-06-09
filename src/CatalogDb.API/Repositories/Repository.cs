using CatalogDb.API.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatalogDb.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext Context { get; }

        public Repository(AppDbContext context)
        {
            Context = context;
        }

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>().AsNoTracking();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public T Create(T entity)
        {
            Context.Set<T>().Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            Context.Set<T>().Update(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            return entity;
        }
    }
}
