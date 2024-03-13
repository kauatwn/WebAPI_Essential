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
            // Consultas utilizando o EF é feita através do cache. É realizado o tracking das entidades para acompanhar os estados.
            // O método AsNoTracking não deixa armazenado entidades no cache e busca diretamente no BD, melhorando a performance.
            // Utilize AsNoTracking somente para consultas de leitura.
            var products = _context.Products.AsNoTracking().ToList();

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

        [HttpDelete("{id:int}")]
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

/* Boas práticas
 * 
 * Para otimizar o desempenho, é importante seguir algumas práticas recomendadas ao escrever consultas em um aplicativo usando o Entity Framework.
 * 
 * 1. Evitar retornar todos os registros em uma consulta, especialmente quando lidamos com grandes conjuntos de dados.
 * 
 *    Ao fazer isso, podemos sobrecarregar a rede e consumir recursos desnecessários.
 *    Em vez disso, é aconselhável limitar o número de entidades retornadas, utilizando, por exemplo, o método Take(). Exemplo:
 *    
 *    _context.Products.Take(10).AsNoTracking().ToList();
 *    
 *    Isso limita a consulta a apenas 10 registros e desativa o rastreamento das entidades, melhorando o desempenho.
 * 
 * 
 * 2. Não retornar objetos relacionados sem aplicar um filtro também.
 * 
 *    Se incluirmos objetos relacionados em uma consulta sem um filtro adequado, podemos acabar recuperando um grande número de dados desnecessários.
 *    É melhor aplicar um filtro antes de incluir objetos relacionados para reduzir a quantidade de dados recuperados. Exemplo:
 *    
 *    _context.Categories.Where(c => c.CategoryId <= 5).Include(c => c.Products).AsNoTracking().ToList();
 *    
 *    Aqui, estamos filtrando as categorias com um ID menor ou igual a 5 e, em seguida, incluindo apenas os produtos relacionados a essas categorias filtradas.
 *    Isso ajuda a evitar a sobrecarga de dados desnecessários e melhora o desempenho da consulta.
 */
