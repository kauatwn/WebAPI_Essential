using CatalogDb.API.Entities;
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
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _unitOfWork.CategoryRepository.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Category> Get(int id)
        {
            var categoria = _unitOfWork.CategoryRepository.GetCategory(id);
            return Ok(categoria);

        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            var createdCategory = _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Commit();
            return new CreatedAtRouteResult("ObterCategoria", new { id = category.Id }, createdCategory);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("Invalid data.");
            }

            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            _unitOfWork.CategoryRepository.GetCategory(id);
            var deletedCategory = _unitOfWork.CategoryRepository.Delete(id);
            _unitOfWork.Commit();
            return Ok(deletedCategory);
        }
    }
}
