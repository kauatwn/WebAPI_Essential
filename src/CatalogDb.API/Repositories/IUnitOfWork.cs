namespace CatalogDb.API.Repositories
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public void Commit();
    }
}
