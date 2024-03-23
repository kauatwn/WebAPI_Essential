using AutoMapper;
using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination;
using CatalogDb.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController(IUnitOfWork<Product> unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork<Product> _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get([FromQuery] ProductQueryParameters productQuery)
        {
            var products = _unitOfWork.ProductRepository.GetPagedProducts(productQuery);
            var metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasPreviousPage,
                products.HasNextPage,
            };

            // Cabeçalho que será enviado ao front contendo informações de paginação.
            // Número da página atual, tamanho da página, total de itens, etc.
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDto);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<ProductDTO> Get(int id)
        {
            var product = _unitOfWork.Repository.Get(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public ActionResult<ProductDTO> Post(ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = _unitOfWork.Repository.Create(product);
            _unitOfWork.Commit();
            var createdProductDto = _mapper.Map<ProductDTO>(createdProduct);
            return new CreatedAtRouteResult("ObterProduto", new { id = product.Id }, createdProductDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProductDTO> Put(int id, ProductDTO productDto)
        {
            if (id != productDto.Id || productDto == null)
            {
                return BadRequest();
            }
            var product = _mapper.Map<Product>(productDto);
            var updatedProduct = _unitOfWork.Repository.Update(product);
            _unitOfWork.Commit();
            var updatedProductDto = _mapper.Map<ProductDTO>(updatedProduct);
            return Ok(updatedProductDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProductDTO> Delete(int id)
        {
            var product = _unitOfWork.Repository.Get(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var deletedProduct = _unitOfWork.Repository.Delete(product);
            _unitOfWork.Commit();
            var deletedProductDto = _mapper.Map<ProductDTO>(deletedProduct);
            return Ok(deletedProductDto);
        }
    }
}
