using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination.Filters;
using CatalogDb.API.Pagination.Filters.Categories;
using CatalogDb.API.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService CategoryService { get; }

        public CategoriesController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories([FromQuery] PaginationFilter<Category> source)
        {
            PaginationResultDTO<CategoryDTO> result = await CategoryService.GetPagedCategoriesAsync(source);

            // Cabeçalho que será enviado ao front contendo informações de paginação.
            // Número da página atual, tamanho da página, total de itens, etc.
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.Metadata));

            return Ok(result.Items);
        }

        [HttpGet("{id:int}", Name = nameof(GetCategoryById))]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            CategoryDTO categoryDto = await CategoryService.GetCategoryByIdAsync(id);

            return Ok(categoryDto);
        }

        [HttpGet("Filter/CategoryName")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesFilteredByName([FromQuery] CategoryNameFilter source)
        {
            PaginationResultDTO<CategoryDTO> result = await CategoryService.GetPagedCategoriesAsync(source);
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.Metadata));

            return Ok(result.Items);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO categoryDto)
        {
            CategoryDTO result = await CategoryService.CreateCategoryAsync(categoryDto);

            return CreatedAtRoute(nameof(GetCategoryById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Put(int id, CategoryDTO categoryDto)
        {
            CategoryDTO result = await CategoryService.UpdateCategoryAsync(id, categoryDto);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            CategoryDTO result = await CategoryService.DeleteCategoryAsync(id);

            return Ok(result);
        }
    }
}
