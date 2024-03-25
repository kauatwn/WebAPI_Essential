using System.Linq.Expressions;

namespace CatalogDb.API.Repositories
{
    public interface IRepository<T>
    {
        public IQueryable<T> GetAll();
        public Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        public T Create(T entity);
        public T Update(T entity);
        public T Delete(T entity);
    }
}
