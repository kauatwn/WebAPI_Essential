using CatalogDb.API.Context;

namespace CatalogDb.API.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private IRepository<T>? _repository;
        private ICategoryRepository? _categoryRepository;
        private IProductRepository? _productRepository;

        private AppDbContext Context { get; }

        public UnitOfWork(AppDbContext context)
        {
            Context = context;
        }

        public IRepository<T> Repository
        {
            get
            {
                return _repository ??= new Repository<T>(Context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository ??= new CategoryRepository(Context);
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepository ??= new ProductRepository(Context);
            }
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
