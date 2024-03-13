using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                // Consultas utilizando o EF é feita através do cache. É realizado o tracking das entidades para acompanhar os estados.
                // O método AsNoTracking não deixa armazenado entidades no cache e busca diretamente no BD, melhorando a performance.
                // Utilize AsNoTracking somente para consultas de leitura.
                var categories = _context.Categories.AsNoTracking().ToList();

                if (categories == null)
                {
                    return NotFound("Categories not found.");
                }
                return categories;
            }
            // Exception genérica para simular um erro específico que será capturado.
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesWithProducts()
        {
            try
            {
                return _context.Categories.Include(p => p.Products).AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Category> Get(int id)
        {
            try
            {
                var categoria = _context.Categories.FirstOrDefault(p => p.Id == id);

                if (categoria == null)
                {
                    return NotFound("Category not found.");
                }
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            try
            {
                if (category is null)
                    return BadRequest();

                _context.Categories.Add(category);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria", new { id = category.Id }, category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoria = _context.Categories.FirstOrDefault(p => p.Id == id);

                if (categoria == null)
                {
                    return NotFound("Category not found.");
                }
                _context.Categories.Remove(categoria);
                _context.SaveChanges();
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while processing your request.");
            }
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
