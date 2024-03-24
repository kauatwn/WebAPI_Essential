using AutoMapper;
using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;
using CatalogDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController(IUnitOfWork<Category> unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork<Category> _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get([FromQuery] CategoryQueryParameters categoryQuery)
        {
            var categories = await _unitOfWork.CategoryRepository.GetPagedCategoriesAsync(categoryQuery);
            return GenerateResponse(categories);
        }

        [HttpGet("filtered-by-name")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesFilteredByName([FromQuery] CategoryNameFilter filter)
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesFilteredByNameAsync(filter);
            return GenerateResponse(categories);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _unitOfWork.Repository.GetAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDto = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(categoryDto);
            var createdCategory = _unitOfWork.Repository.Create(category);
            await _unitOfWork.CommitAsync();
            var createdCategoryDto = _mapper.Map<CategoryDTO>(createdCategory);
            return new CreatedAtRouteResult("ObterCategoria", new { id = category.Id }, createdCategoryDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Put(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id || categoryDto == null)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(id);
            var updatedCategory = _unitOfWork.Repository.Update(category);
            await _unitOfWork.CommitAsync();
            var updatedCategoryDto = _mapper.Map<CategoryDTO>(updatedCategory);
            return Ok(updatedCategoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await _unitOfWork.Repository.GetAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            var deletedCategory = _unitOfWork.Repository.Delete(category);
            await _unitOfWork.CommitAsync();
            var deletedCategoryDto = _mapper.Map<CategoryDTO>(deletedCategory);
            return Ok(deletedCategoryDto);
        }

        private ActionResult<IEnumerable<CategoryDTO>> GenerateResponse(PagedList<Category> categories)
        {
            var metadata = new
            {
                categories.TotalCount,
                categories.PageSize,
                categories.CurrentPage,
                categories.TotalPages,
                categories.HasPreviousPage,
                categories.HasNextPage,
            };
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(categoriesDto);
        }
    }
}
