using CatalogDb.API.DTOs;
using CatalogDb.API.Entities;
using CatalogDb.API.Pagination.Filters;
using CatalogDb.API.Pagination.Filters.Products;
using CatalogDb.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService ProductService { get; }

        public ProductsController(IProductService productService)
        {
            ProductService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts([FromQuery] PaginationFilter<Product> source)
        {
            PaginationResultDTO<ProductDTO> result = await ProductService.GetPagedProductsAsync(source);

            // Cabeçalho que será enviado ao front contendo informações de paginação.
            // Número da página atual, tamanho da página, total de itens, etc.
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.Metadata));

            return Ok(result.Items);
        }

        [HttpGet("{id:int}", Name = nameof(GetProductById))]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            ProductDTO productDto = await ProductService.GetProductByIdAsync(id);

            return Ok(productDto);
        }

        [HttpGet("Filter/ExactPrice")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsFilteredByExactPrice([FromQuery] ProductExactPriceFilter source)
        {
            PaginationResultDTO<ProductDTO> result = await ProductService.GetProductsByExactPriceAsync(source);
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.Metadata));

            return Ok(result.Items);
        }

        [HttpGet("Filter/PriceCriterion")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsFilteredByPriceCriterion([FromQuery] ProductPriceOrderFilter source)
        {
            PaginationResultDTO<ProductDTO> result = await ProductService.GetProductsByPriceCriterionAsync(source);
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.Metadata));

            return Ok(result.Items);
        }

        [HttpGet("Filter/PriceAndAdditionalCriterion")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByAdvancedPriceFilter([FromQuery] ProductAdvancedPriceFilter source)
        {
            PaginationResultDTO<ProductDTO> result = await ProductService.GetProductsByAdvancedPriceFilterAsync(source);
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.Metadata));

            return Ok(result.Items);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post(ProductDTO productDto)
        {
            ProductDTO result = await ProductService.CreateProductAsync(productDto);

            return CreatedAtRoute(nameof(GetProductById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Put(int id, ProductDTO productDto)
        {
            ProductDTO result = await ProductService.UpdateProductAsync(id, productDto);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            ProductDTO result = await ProductService.DeleteProductAsync(id);

            return Ok(result);
        }
    }
}
