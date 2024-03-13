using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _context.Categories.ToList();

            if (categories == null)
            {
                return NotFound("Categories not found.");
            }
            return categories;
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetProductsByCategory()
        {
            return _context.Categories.Include(p => p.Products).ToList();
        }


        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Category> Get(int id)
        {
            var categoria = _context.Categories.FirstOrDefault(p => p.Id == id);

            if (categoria == null)
            {
                return NotFound("Category not found.");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            if (category is null)
                return BadRequest();

            _context.Categories.Add(category);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = category.Id }, category);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categories.FirstOrDefault(p => p.Id == id);

            if (categoria == null)
            {
                return NotFound("Category not found.");
            }
            _context.Categories.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
    }
}
