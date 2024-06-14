using AutoMapper;
using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;
using CatalogDb.API.Pagination.Filters;
using CatalogDb.API.Pagination.Filters.Categories;
using CatalogDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IUnitOfWork<Category> UnitOfWork { get; }
        private IMapper Mapper { get; }

        public CategoriesController(IUnitOfWork<Category> unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoryById([FromQuery] BaseFilter<Category> query)
        {
            PagedList<Category> categories = await UnitOfWork.CategoryRepository.GetPagedCategoriesAsync(query);

            return GenerateResponse(categories);
        }

        [HttpGet("{id:int}", Name = nameof(GetCategoryById))]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            Category? category = await UnitOfWork.Repository.GetByIdAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryDTO categoryDto = Mapper.Map<CategoryDTO>(category);

            return Ok(categoryDto);
        }

        [HttpGet("Filter/CategoryNameFilter")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesFilteredByName([FromQuery] CategoryNameFilter filter)
        {
            PagedList<Category> categories = await UnitOfWork.CategoryRepository.GetCategoriesFilteredByNameAsync(filter);

            return GenerateResponse(categories);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }

            Category category = Mapper.Map<Category>(categoryDto);
            Category createdCategory = UnitOfWork.Repository.Create(category);

            await UnitOfWork.CommitAsync();

            CategoryDTO createdCategoryDto = Mapper.Map<CategoryDTO>(createdCategory);

            return CreatedAtRoute(nameof(GetCategoryById), new { id = category.Id }, createdCategoryDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Put(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id || categoryDto == null)
            {
                return BadRequest();
            }

            Category category = Mapper.Map<Category>(id);
            Category updatedCategory = UnitOfWork.Repository.Update(category);

            await UnitOfWork.CommitAsync();

            CategoryDTO updatedCategoryDto = Mapper.Map<CategoryDTO>(updatedCategory);

            return Ok(updatedCategoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            Category? category = await UnitOfWork.Repository.GetByIdAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            Category deletedCategory = UnitOfWork.Repository.Delete(category);

            await UnitOfWork.CommitAsync();

            CategoryDTO deletedCategoryDto = Mapper.Map<CategoryDTO>(deletedCategory);

            return Ok(deletedCategoryDto);
        }

        private ActionResult<IEnumerable<CategoryDTO>> GenerateResponse(PagedList<Category> categories)
        {
            object metadata = new
            {
                categories.TotalCount,
                categories.PageSize,
                categories.CurrentPage,
                categories.TotalPages,
                categories.HasPreviousPage,
                categories.HasNextPage
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            IEnumerable<CategoryDTO> categoriesDto = Mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return Ok(categoriesDto);
        }
    }
}
