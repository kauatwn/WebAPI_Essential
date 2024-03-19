using CatalogDb.API.Context;

namespace CatalogDb.API.Repositories
{
    public interface IUnityOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public void Commit();
    }
}
