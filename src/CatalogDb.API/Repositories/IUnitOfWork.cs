namespace CatalogDb.API.Repositories
{
    public interface IUnitOfWork<T> where T : class
    {
        IRepository<T> Repository { get; }
        Task CommitAsync();
    }
}
