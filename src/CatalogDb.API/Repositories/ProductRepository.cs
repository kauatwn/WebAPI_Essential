using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb.API.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        private readonly AppDbContext _context = context;
        
        public IEnumerable<Product> GetProducts()
        {
            var products = _context.Products.AsNoTracking().ToList();
         
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }
            return products;
        }

        public Product GetProduct(int id)
        {
            var product = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return product;
        }

        public Product Create(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Add(product);
            return product;
        }
        
        public Product Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Entry(product).State = EntityState.Modified;
            return product;
        }

        public Product Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Remove(product);
            return product;
        }
    }
}
