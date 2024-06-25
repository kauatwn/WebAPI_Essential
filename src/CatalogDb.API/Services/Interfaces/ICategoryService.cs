using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination.Filters;
using CatalogDb.API.Pagination.Filters.Categories;

namespace CatalogDb.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<PaginationResultDTO<CategoryDTO>> GetPagedCategoriesAsync(PaginationFilter<Category> source);
        Task<CategoryDTO> GetCategoryByIdAsync(int id);
        Task<PaginationResultDTO<CategoryDTO>> GetCategoriesByName(CategoryNameFilter source);
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto);
        Task<CategoryDTO> UpdateCategoryAsync(int id, CategoryDTO categoryDto);
        Task<CategoryDTO> DeleteCategoryAsync(int id);
    }
}
