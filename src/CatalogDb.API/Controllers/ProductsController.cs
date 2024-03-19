using CatalogDb.API.Entities;
using CatalogDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController(IUnityOfWork unityOfWork) : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork = unityOfWork;

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _unityOfWork.ProductRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Product> Get(int id)
        {
            var product = _unityOfWork.ProductRepository.GetProduct(id);
            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            var createdProduct = _unityOfWork.ProductRepository.Create(product);
            _unityOfWork.Commit();
            return new CreatedAtRouteResult("ObterProduto", new {id = product.Id}, createdProduct);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Ivalid data.");
            }
            _unityOfWork.ProductRepository.Update(product);
            _unityOfWork.Commit();
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            _unityOfWork.ProductRepository.GetProduct(id);
            var deletedProduct = _unityOfWork.ProductRepository.Delete(id);
            _unityOfWork.Commit();
            return Ok(deletedProduct);
        }
    }
}