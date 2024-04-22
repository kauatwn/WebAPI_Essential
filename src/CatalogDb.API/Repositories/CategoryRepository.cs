using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb.API.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Category>> GetPagedCategoriesAsync(CategoryQueryParameters query)
        {
            IOrderedQueryable<Category> orderedCategories = GetAll().OrderBy(c => c.Id);

            PagedList<Category> pagedCategoryList = await PagedList<Category>.ToPagedList(orderedCategories, query.PageNumber, query.PageSize);

            if (pagedCategoryList.Count == 0)
            {
                throw new InvalidOperationException("List of categories not found.");
            }

            return pagedCategoryList;
        }

        public async Task<PagedList<Category>> GetCategoriesFilteredByNameAsync(CategoryNameFilter filter)
        {
            IQueryable<Category> categories = GetAll();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                categories = categories.Where(c => c.Name.Contains(filter.Name));
            }

            PagedList<Category> pagedCategoryWithProductsList = await PagedList<Category>.ToPagedList(categories, filter.PageNumber, filter.PageSize);

            return pagedCategoryWithProductsList;
        }

        public async Task<PagedList<Category>> GetCategoriesWithProductsAsync(CategoryQueryParameters query)
        {
            IQueryable<Category> categoriesWithProducts = _context.Categories.Include(c => c.Products);

            PagedList<Category> pagedCategoriesWithProducts = await PagedList<Category>.ToPagedList(categoriesWithProducts, query.PageNumber, query.PageSize);

            if (pagedCategoriesWithProducts.Count == 0)
            {
                throw new InvalidOperationException("List of categories with products not found.");
            }

            return pagedCategoriesWithProducts;
        }
    }
}
