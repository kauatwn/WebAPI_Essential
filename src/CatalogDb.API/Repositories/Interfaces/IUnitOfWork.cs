namespace CatalogDb.API.Repositories.Interfaces
{
    public interface IUnitOfWork<T> where T : class
    {
        IRepository<T> Repository { get; }
        Task CommitAsync();
    }
}
