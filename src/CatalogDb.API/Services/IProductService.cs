using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination.Filters;
using CatalogDb.API.Pagination.Filters.Products;

namespace CatalogDb.API.Services
{
    public interface IProductService
    {
        Task<PaginationResultDTO<ProductDTO>> GetPagedProductsAsync(PaginationFilter<Product> source);
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<PaginationResultDTO<ProductDTO>> GetProductsByExactPriceAsync(ProductExactPriceFilter filter);
        Task<PaginationResultDTO<ProductDTO>> GetProductsByPriceCriterionAsync(ProductPriceOrderFilter filter);
        Task<PaginationResultDTO<ProductDTO>> GetProductsByAdvancedPriceFilterAsync(ProductAdvancedPriceFilter filter);
        Task<ProductDTO> CreateProductAsync(ProductDTO productDto);
        Task<ProductDTO> UpdateProductAsync(int id, ProductDTO productDto);
        Task<ProductDTO> DeleteProductAsync(int id);
    }
}
