using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _context.Products.ToList();

            if (products == null)
            {
                return NotFound("Products not found.");
            }
            return products;
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Product> Get(int id)
        {
            var products = _context.Products.FirstOrDefault(p => p.Id == id);
            if (products == null)
            {
                return NotFound("Product not found.");
            }
            return products;
        }

        [HttpPost]
        public ActionResult<Product> Post(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            _context.Products.Add(product); // Inclui product no contexto do EF Core (Memória)
            _context.SaveChanges(); // Salva no BD
            return new CreatedAtRouteResult("ObterProduto", new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Product> Put(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
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
    }
}
