using CatalogDb.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb.API.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
    }
}
