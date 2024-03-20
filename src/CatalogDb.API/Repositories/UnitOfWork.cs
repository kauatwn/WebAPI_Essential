using CatalogDb.API.Context;

namespace CatalogDb.API.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private IProductRepository? _productRepository;
        private ICategoryRepository? _categoryRepository;

        private readonly AppDbContext _context = context;

        public IProductRepository ProductRepository
        {
            get
            {
                // Null coalescing.
                // Se _productRepository for null, recebe ProductRepository(Context).
                // Se não, recebe _productRepository.
                return _productRepository ??= new ProductRepository(_context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository ??= new CategoryRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
