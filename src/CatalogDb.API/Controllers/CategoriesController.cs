using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                var categories = _context.Categories.AsNoTracking().ToList();

                if (categories == null)
                {
                    return NotFound("Categories not found.");
                }
                return categories;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesWithProducts()
        {
            try
            {
                var categoriesWithProducts = _context.Categories.AsNoTracking().Include(p => p.Products).ToList();

                if (categoriesWithProducts == null)
                {
                    return NotFound("Categories with products not found.");
                }
                return categoriesWithProducts;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Category> Get(int id)
        {
            try
            {
                var categoria = _context.Categories.AsNoTracking().FirstOrDefault(p => p.Id == id);

                if (categoria == null)
                {
                    return NotFound("Category not found.");
                }
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }

                _context.Categories.Add(category);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterCategoria", new { id = category.Id }, category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            try
            {
                if (id != category.Id)
                {
                    return BadRequest();
                }
                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }
    }
}
