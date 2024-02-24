using CatalogDb.API.Context;
using CatalogDb.API.Entities;
using Microsoft.AspNetCore.Mvc;

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
    }
}
