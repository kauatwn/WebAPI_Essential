using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                var products = _context.Products.AsNoTracking().ToList();

                if (products == null)
                {
                    return NotFound("Products not found.");
                }
                return products;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                var products = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);

                if (products == null)
                {
                    return NotFound("Product not found.");
                }
                return products;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }

        [HttpPost]
        public ActionResult<Product> Post(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }

                _context.Products.Add(product);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterProduto", new { id = product.Id }, product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<Product> Put(int id, Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    return BadRequest();
                }

                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(product);
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
                var product = _context.Products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }
    }
}
