using CatalogDb.API.Context;

namespace CatalogDb.API.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private IRepository<T>? _repository;

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
