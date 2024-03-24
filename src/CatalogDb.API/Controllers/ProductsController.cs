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
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get([FromQuery] ProductQueryParameters productQuery)
        {
            var products = await _unitOfWork.ProductRepository.GetPagedProductsAsync(productQuery);
            return GenerateResponse(products);
        }

        [HttpGet("filtered-by-price")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsFilteredByPrice([FromQuery] ProductPriceFilter filter)
        {
            var products = await _unitOfWork.ProductRepository.GetProductsFilteredByPriceAsync(filter);
            return GenerateResponse(products);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _unitOfWork.Repository.GetAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post(ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = _unitOfWork.Repository.Create(product);
            await _unitOfWork.CommitAsync();
            var createdProductDto = _mapper.Map<ProductDTO>(createdProduct);
            return new CreatedAtRouteResult("ObterProduto", new { id = product.Id }, createdProductDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Put(int id, ProductDTO productDto)
        {
            if (id != productDto.Id || productDto == null)
            {
                return BadRequest();
            }
            var product = _mapper.Map<Product>(productDto);
            var updatedProduct = _unitOfWork.Repository.Update(product);
            await _unitOfWork.CommitAsync();
            var updatedProductDto = _mapper.Map<ProductDTO>(updatedProduct);
            return Ok(updatedProductDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await _unitOfWork.Repository.GetAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var deletedProduct = _unitOfWork.Repository.Delete(product);
            await _unitOfWork.CommitAsync();
            var deletedProductDto = _mapper.Map<ProductDTO>(deletedProduct);
            return Ok(deletedProductDto);
        }

        private ActionResult<IEnumerable<ProductDTO>> GenerateResponse(PagedList<Product> products)
        {
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
    }
}
