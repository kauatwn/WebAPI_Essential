using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;

namespace CatalogDb.API.Repositories
{
    public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
    {
        public PagedList<Product> GetPagedProducts(ProductQueryParameters productQuery)
        {
            var products = GetAll().OrderBy(p => p.Id).AsQueryable();
            var pagedProductList = PagedList<Product>.ToPagedList(products, productQuery.PageNumber, productQuery.PageSize);
            if (pagedProductList.Count == 0)
            {
                throw new Exception("List of products not found.");
            }
            return pagedProductList;
        }
    }
}
