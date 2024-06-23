using AutoMapper;
using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Exceptions;
using CatalogDb.API.Pagination.Filters;
using CatalogDb.API.Pagination.Filters.Products;
using CatalogDb.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb.API.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork<Product> UnitOfWork { get; }
        private IMapper Mapper { get; }

        public ProductService(IUnitOfWork<Product> unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<PaginationResultDTO<ProductDTO>> GetPagedProductsAsync(PaginationFilter<Product> source)
        {
            IQueryable<Product> products = UnitOfWork.Repository.GetAll().OrderBy(p => p.Id);
            IQueryable<Product> pagedProducts = source.HandleFilter(products);

            int totalCount = await products.CountAsync();
            var metadataDto = new PaginationMetadataDTO(totalCount, source.PageNumber, source.PageSize);

            if (source.PageNumber > metadataDto.TotalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(source.PageNumber));
            }

            IEnumerable<ProductDTO> pagedProductsDto = Mapper.Map<IEnumerable<ProductDTO>>(pagedProducts);

            return new PaginationResultDTO<ProductDTO>(pagedProductsDto, metadataDto);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            Product? product = await UnitOfWork.Repository.GetByIdAsync(p => p.Id == id)
                ?? throw new ResourceNotFoundException($"No product found with the ID {id}");

            ProductDTO productDto = Mapper.Map<ProductDTO>(product);

            return productDto;
        }

        public async Task<PaginationResultDTO<ProductDTO>> GetProductsByExactPriceAsync(ProductExactPriceFilter source)
        {
            IQueryable<Product> products = UnitOfWork.Repository.GetAll().OrderBy(p => p.Id);
            IQueryable<Product> pagedProducts = source.HandleFilter(products);

            int totalCount = await products.CountAsync();
            var metadataDto = new PaginationMetadataDTO(totalCount, source.PageNumber, source.PageSize);

            if (source.PageNumber > metadataDto.TotalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(source.PageNumber));
            }

            IEnumerable<ProductDTO> pagedProductsDto = Mapper.Map<IEnumerable<ProductDTO>>(pagedProducts);

            return new PaginationResultDTO<ProductDTO>(pagedProductsDto, metadataDto);
        }

        public async Task<PaginationResultDTO<ProductDTO>> GetProductsByPriceCriterionAsync(ProductPriceOrderFilter source)
        {
            IQueryable<Product> products = UnitOfWork.Repository.GetAll().OrderBy(p => p.Id);
            IQueryable<Product> pagedProducts = source.HandleFilter(products);

            int totalCount = await products.CountAsync();
            var metadataDto = new PaginationMetadataDTO(totalCount, source.PageNumber, source.PageSize);

            if (source.PageNumber > metadataDto.TotalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(source.PageNumber));
            }

            IEnumerable<ProductDTO> pagedProductsDto = Mapper.Map<IEnumerable<ProductDTO>>(pagedProducts);

            return new PaginationResultDTO<ProductDTO>(pagedProductsDto, metadataDto);
        }

        public async Task<PaginationResultDTO<ProductDTO>> GetProductsByAdvancedPriceFilterAsync(ProductAdvancedPriceFilter source)
        {
            IQueryable<Product> products = UnitOfWork.Repository.GetAll().OrderBy(p => p.Id);
            IQueryable<Product> pagedProducts = source.HandleFilter(products);

            int totalCount = await products.CountAsync();
            var metadataDto = new PaginationMetadataDTO(totalCount, source.PageNumber, source.PageSize);

            if (source.PageNumber > metadataDto.TotalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(source.PageNumber));
            }

            IEnumerable<ProductDTO> pagedProductsDto = Mapper.Map<IEnumerable<ProductDTO>>(pagedProducts);

            return new PaginationResultDTO<ProductDTO>(pagedProductsDto, metadataDto);
        }

        public async Task<ProductDTO> CreateProductAsync(ProductDTO productDto)
        {
            Product product = Mapper.Map<Product>(productDto);

            Product createdProduct = UnitOfWork.Repository.Create(product);
            await UnitOfWork.CommitAsync();

            ProductDTO createdProductDto = Mapper.Map<ProductDTO>(createdProduct);

            return createdProductDto;
        }

        public async Task<ProductDTO> UpdateProductAsync(int id, ProductDTO productDto)
        {
            if (id != productDto.Id)
            {
                throw new ArgumentException(nameof(id));
            }

            Product product = Mapper.Map<Product>(productDto);

            Product updatedProduct = UnitOfWork.Repository.Update(product);
            await UnitOfWork.CommitAsync();

            ProductDTO updatedProductDto = Mapper.Map<ProductDTO>(updatedProduct);

            return updatedProductDto;
        }

        public async Task<ProductDTO> DeleteProductAsync(int id)
        {
            Product? product = await UnitOfWork.Repository.GetByIdAsync(p => p.Id == id)
                ?? throw new ResourceNotFoundException($"No product found with the ID {id}");

            Product deletedProduct = UnitOfWork.Repository.Delete(product);
            await UnitOfWork.CommitAsync();

            ProductDTO deletedProductDto = Mapper.Map<ProductDTO>(deletedProduct);

            return deletedProductDto;
        }
    }
}
