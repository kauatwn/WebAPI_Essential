using CatalogDb.API.Context;

namespace CatalogDb.API.Repositories
{
    public class UnityOfWork(AppDbContext context) : IUnityOfWork
    {
        private IProductRepository? _productRepository;
        private ICategoryRepository? _categoryRepository;

        private readonly AppDbContext Context = context;

        public IProductRepository ProductRepository
        {
            get
            {
                // Null coalescing.
                // Se _productRepository for null, recebe ProductRepository(Context).
                // Se não, recebe _productRepository.
                return _productRepository ??= new ProductRepository(Context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository ??= new CategoryRepository(Context);
            }
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
