using CatalogDb.API.Entities;
using CatalogDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController(IUnityOfWork unityOfWork) : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork = unityOfWork;

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _unityOfWork.CategoryRepository.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Category> Get(int id)
        {
            var categoria = _unityOfWork.CategoryRepository.GetCategory(id);
            return Ok(categoria);

        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            var createdCategory = _unityOfWork.CategoryRepository.Create(category);
            return new CreatedAtRouteResult("ObterCategoria", new { id = category.Id }, createdCategory);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("Invalid data.");
            }

            _unityOfWork.CategoryRepository.Update(category);
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            _unityOfWork.CategoryRepository.GetCategory(id);
            var deletedCategory = _unityOfWork.CategoryRepository.Delete(id);
            return Ok(deletedCategory);
        }
    }
}
