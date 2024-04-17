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
            PagedList<Category> categories = await _unitOfWork.CategoryRepository.GetPagedCategoriesAsync(categoryQuery);

            return GenerateResponse(categories);
        }

        [HttpGet("filtered-by-name")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesFilteredByName([FromQuery] CategoryNameFilter filter)
        {
            PagedList<Category> categories = await _unitOfWork.CategoryRepository.GetCategoriesFilteredByNameAsync(filter);

            return GenerateResponse(categories);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            Category? category = await _unitOfWork.Repository.GetAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryDTO categoryDto = _mapper.Map<CategoryDTO>(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }

            Category category = _mapper.Map<Category>(categoryDto);
            Category createdCategory = _unitOfWork.Repository.Create(category);

            await _unitOfWork.CommitAsync();

            CategoryDTO createdCategoryDto = _mapper.Map<CategoryDTO>(createdCategory);

            return new CreatedAtRouteResult("ObterCategoria", new { id = category.Id }, createdCategoryDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Put(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id || categoryDto == null)
            {
                return BadRequest();
            }

            Category category = _mapper.Map<Category>(id);
            Category updatedCategory = _unitOfWork.Repository.Update(category);

            await _unitOfWork.CommitAsync();

            CategoryDTO updatedCategoryDto = _mapper.Map<CategoryDTO>(updatedCategory);

            return Ok(updatedCategoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            Category? category = await _unitOfWork.Repository.GetAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            Category deletedCategory = _unitOfWork.Repository.Delete(category);

            await _unitOfWork.CommitAsync();

            CategoryDTO deletedCategoryDto = _mapper.Map<CategoryDTO>(deletedCategory);

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

            IEnumerable<CategoryDTO> categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return Ok(categoriesDto);
        }
    }
}
