using AutoMapper;
using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Exceptions;
using CatalogDb.API.Pagination.Filters;
using CatalogDb.API.Pagination.Filters.Categories;
using CatalogDb.API.Repositories.Interfaces;
using CatalogDb.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb.API.Services
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork<Category> UnitOfWork { get; }
        private IMapper Mapper { get; }

        public CategoryService(IUnitOfWork<Category> unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<PaginationResultDTO<CategoryDTO>> GetPagedCategoriesAsync(PaginationFilter<Category> source)
        {
            IQueryable<Category> categories = UnitOfWork.Repository.GetAll().OrderBy(c => c.Id);
            IQueryable<Category> pagedCategories = source.HandleFilter(categories);

            int totalCount = await categories.CountAsync();
            var metadataDto = new PaginationMetadataDTO(totalCount, source.PageNumber, source.PageSize);

            if (source.PageNumber > metadataDto.TotalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(source.PageNumber));
            }

            IEnumerable<CategoryDTO> pagedCategoriesDto = Mapper.Map<IEnumerable<CategoryDTO>>(pagedCategories);

            return new PaginationResultDTO<CategoryDTO>(pagedCategoriesDto, metadataDto);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            Category? category = await UnitOfWork.Repository.GetByIdAsync(c => c.Id == id)
                ?? throw new ResourceNotFoundException($"No category found with the ID {id}");

            CategoryDTO categoryDto = Mapper.Map<CategoryDTO>(category);

            return categoryDto;
        }

        public async Task<PaginationResultDTO<CategoryDTO>> GetCategoriesByName(CategoryNameFilter source)
        {
            IQueryable<Category> categories = UnitOfWork.Repository.GetAll().OrderBy(c => c.Id);
            IQueryable<Category> pagedCategories = source.HandleFilter(categories);

            int totalCount = await categories.CountAsync();
            var metadataDto = new PaginationMetadataDTO(totalCount, source.PageNumber, source.PageSize);

            if (source.PageNumber > metadataDto.TotalPages)
            {
                throw new ArgumentOutOfRangeException(nameof(source.PageNumber));
            }

            IEnumerable<CategoryDTO> pagedCategoriesDto = Mapper.Map<IEnumerable<CategoryDTO>>(pagedCategories);

            return new PaginationResultDTO<CategoryDTO>(pagedCategoriesDto, metadataDto);
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto)
        {
            Category category = Mapper.Map<Category>(categoryDto);

            Category createdCategory = UnitOfWork.Repository.Create(category);
            await UnitOfWork.CommitAsync();

            CategoryDTO createdCategoryDto = Mapper.Map<CategoryDTO>(createdCategory);

            return createdCategoryDto;
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
            {
                throw new ArgumentException(nameof(id));
            }

            Category category = Mapper.Map<Category>(categoryDto);

            Category updatedCategory = UnitOfWork.Repository.Update(category);
            await UnitOfWork.CommitAsync();

            CategoryDTO updatedCategoryDto = Mapper.Map<CategoryDTO>(updatedCategory);

            return updatedCategoryDto;
        }

        public async Task<CategoryDTO> DeleteCategoryAsync(int id)
        {
            Category? category = await UnitOfWork.Repository.GetByIdAsync(c => c.Id == id)
                ?? throw new ResourceNotFoundException($"No category found with the ID {id}");

            Category deledtedCategory = UnitOfWork.Repository.Delete(category);
            await UnitOfWork.CommitAsync();

            CategoryDTO deledtedCategoryDto = Mapper.Map<CategoryDTO>(deledtedCategory);

            return deledtedCategoryDto;
        }
    }
}
