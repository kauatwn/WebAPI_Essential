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
    public class ProductsController : ControllerBase
    {
        private IUnitOfWork<Product> UnitOfWork { get; }
        private IMapper Mapper { get; }

        public ProductsController(IUnitOfWork<Product> unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get([FromQuery] ProductQueryParameters productQuery)
        {
            PagedList<Product> products = await UnitOfWork.ProductRepository.GetPagedProductsAsync(productQuery);

            return GenerateResponse(products);
        }

        [HttpGet("filtered-by-price")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsFilteredByPrice([FromQuery] ProductPriceFilter filter)
        {
            PagedList<Product> products = await UnitOfWork.ProductRepository.GetProductsFilteredByPriceAsync(filter);

            return GenerateResponse(products);
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            Product? product = await UnitOfWork.Repository.GetAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ProductDTO productDto = Mapper.Map<ProductDTO>(product);

            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post(ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            Product product = Mapper.Map<Product>(productDto);
            Product createdProduct = UnitOfWork.Repository.Create(product);

            await UnitOfWork.CommitAsync();

            ProductDTO createdProductDto = Mapper.Map<ProductDTO>(createdProduct);

            return new CreatedAtRouteResult("ObterProduto", new { id = product.Id }, createdProductDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Put(int id, ProductDTO productDto)
        {
            if (id != productDto.Id || productDto == null)
            {
                return BadRequest();
            }

            Product product = Mapper.Map<Product>(productDto);
            Product updatedProduct = UnitOfWork.Repository.Update(product);

            await UnitOfWork.CommitAsync();

            ProductDTO updatedProductDto = Mapper.Map<ProductDTO>(updatedProduct);

            return Ok(updatedProductDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            Product? product = await UnitOfWork.Repository.GetAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            Product deletedProduct = UnitOfWork.Repository.Delete(product);

            await UnitOfWork.CommitAsync();

            ProductDTO deletedProductDto = Mapper.Map<ProductDTO>(deletedProduct);

            return Ok(deletedProductDto);
        }

        private ActionResult<IEnumerable<ProductDTO>> GenerateResponse(PagedList<Product> products)
        {
            object metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasPreviousPage,
                products.HasNextPage
            };

            // Cabeçalho que será enviado ao front contendo informações de paginação.
            // Número da página atual, tamanho da página, total de itens, etc.
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            IEnumerable<ProductDTO> productsDto = Mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDto);
        }
    }
}
