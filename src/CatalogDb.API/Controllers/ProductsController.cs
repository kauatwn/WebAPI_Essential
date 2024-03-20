using CatalogDb.API.Entities;
using CatalogDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _unitOfWork.ProductRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Product> Get(int id)
        {
            var product = _unitOfWork.ProductRepository.GetProduct(id);
            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            var createdProduct = _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.Commit();
            return new CreatedAtRouteResult("ObterProduto", new { id = product.Id }, createdProduct);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Ivalid data.");
            }
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            _unitOfWork.ProductRepository.GetProduct(id);
            var deletedProduct = _unitOfWork.ProductRepository.Delete(id);
            _unitOfWork.Commit();
            return Ok(deletedProduct);
        }
    }
}