using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Extensions;
using CatalogDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> Get()
        {
            var categories = _unitOfWork.CategoryRepository.GetCategories();
            var categoriesDto = categories.ToCategoriesDTOList();
            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoryDTO> Get(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetCategory(id);
            var categoryDto = category.ToCategoryDTO();
            return Ok(categoryDto);
        }

        [HttpPost]
        public ActionResult<CategoryDTO> Post(CategoryDTO categoryDto)
        {
            var category = categoryDto.ToCategory();
            var createdCategory = _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Commit();
            var createdCategoryDto = createdCategory.ToCategoryDTO();
            return new CreatedAtRouteResult("ObterCategoria", new { id = category.Id }, createdCategoryDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoryDTO> Put(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest("Invalid data.");
            }

            var category = categoryDto.ToCategory();
            var updatedCategory = _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();
            var updatedCategoryDto = updatedCategory.ToCategoryDTO();
            return Ok(updatedCategoryDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoryDTO> Delete(int id)
        {
            _unitOfWork.CategoryRepository.GetCategory(id);
            var deletedCategory = _unitOfWork.CategoryRepository.Delete(id);
            _unitOfWork.Commit();
            var deletedCategoryDto = deletedCategory.ToCategoryDTO();
            return Ok(deletedCategoryDto);
        }
    }
}
