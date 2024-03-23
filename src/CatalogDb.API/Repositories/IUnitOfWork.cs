namespace CatalogDb.API.Repositories
{
    public interface IUnitOfWork<T>
    {
        IRepository<T> Repository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        void Commit();
    }
}
