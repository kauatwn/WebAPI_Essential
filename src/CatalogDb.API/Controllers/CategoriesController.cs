using AutoMapper;
using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> Get()
        {
            var categories = _unitOfWork.CategoryRepository.GetCategories();
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoryDTO> Get(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetCategory(id);
            var categoryDto = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDto);
        }

        [HttpPost]
        public ActionResult<CategoryDTO> Post(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var createdCategory = _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Commit();
            var createdCategoryDto = _mapper.Map<CategoryDTO>(createdCategory);
            return new CreatedAtRouteResult("ObterCategoria", new { id = category.Id }, createdCategoryDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoryDTO> Put(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest("Invalid data.");
            }

            var category = _mapper.Map<Category>(id);
            var updatedCategory = _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();
            var updatedCategoryDto = _mapper.Map<CategoryDTO>(updatedCategory);
            return Ok(updatedCategoryDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoryDTO> Delete(int id)
        {
            _unitOfWork.CategoryRepository.GetCategory(id);
            var deletedCategory = _unitOfWork.CategoryRepository.Delete(id);
            _unitOfWork.Commit();
            var deletedCategoryDto = _mapper.Map<CategoryDTO>(deletedCategory);
            return Ok(deletedCategoryDto);
        }
    }
}
