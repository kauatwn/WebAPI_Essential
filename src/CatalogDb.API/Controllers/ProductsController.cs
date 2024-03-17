using CatalogDb.API.Entities;
using CatalogDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository repository) : ControllerBase
    {
        private readonly IProductRepository _repository = repository;

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _repository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Product> Get(int id)
        {
            var product = _repository.GetProduct(id);
            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            var createdProduct = _repository.Create(product);
            return new CreatedAtRouteResult("ObterProduto", new {id = product.Id}, createdProduct);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Ivalid data.");
            }
            _repository.Update(product);
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            _repository.GetProduct(id);
            var deletedProduct = _repository.Delete(id);
            return Ok(deletedProduct);
        }
    }
}