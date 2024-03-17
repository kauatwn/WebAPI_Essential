using CatalogDb.API.Entities;
using CatalogDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryRepository repository) : ControllerBase
    {
        private readonly ICategoryRepository _repository = repository;

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _repository.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Category> Get(int id)
        {
            var categoria = _repository.GetCategory(id);
            return Ok(categoria);

        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            var createdCategory = _repository.Create(category);
            return new CreatedAtRouteResult("ObterCategoria", new { id = category.Id }, createdCategory);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("Invalid data.");
            }

            _repository.Update(category);
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            _repository.GetCategory(id);
            var deletedCategory = _repository.Delete(id);
            return Ok(deletedCategory);
        }
    }
}
