using System.Linq.Expressions;

namespace CatalogDb.API.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
