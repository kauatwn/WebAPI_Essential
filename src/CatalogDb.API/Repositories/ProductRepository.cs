using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb.API.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        private readonly AppDbContext _context = context;
        
        public PagedList<Product> GetProducts(ProductQueryParams productQuery)
        {
            var products = _context.Products.AsNoTracking().OrderBy(p => p.Id);
            var orderedProduct = PagedList<Product>.ToPagedList(products, productQuery.PageNumber, productQuery.PageSize);
            if (orderedProduct.Count == 0)
            {
                throw new Exception("List of products not found.");
            }
            return orderedProduct;
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
