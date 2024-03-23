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
    public class ProductsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get([FromQuery] ProductQueryParams productQuery)
        {
            if (productQuery.PageNumber == 0)
            {
                productQuery.PageNumber = 1;
            }

            if (productQuery.PageSize == 0)
            {
                productQuery.PageSize = 10;
            }

            var products = _unitOfWork.ProductRepository.GetProducts(productQuery);
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
            var product = _unitOfWork.ProductRepository.GetProduct(id);
            var productDto = _mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public ActionResult<ProductDTO> Post(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.Commit();
            var createdProductDto = _mapper.Map<ProductDTO>(createdProduct);
            return new CreatedAtRouteResult("ObterProduto", new { id = product.Id }, createdProductDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProductDTO> Put(int id, ProductDTO productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest("Ivalid data.");
            }

            var product = _mapper.Map<Product>(productDto);
            var updatedProduct = _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();
            var updatedProductDto = _mapper.Map<ProductDTO>(updatedProduct);
            return Ok(updatedProductDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProductDTO> Delete(int id)
        {
            _unitOfWork.ProductRepository.GetProduct(id);
            var deletedProduct = _unitOfWork.ProductRepository.Delete(id);
            _unitOfWork.Commit();
            var deletedProductDto = _mapper.Map<ProductDTO>(deletedProduct);
            return Ok(deletedProductDto);
        }
    }
}
