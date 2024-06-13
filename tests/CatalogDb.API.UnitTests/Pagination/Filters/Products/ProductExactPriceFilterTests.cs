using CatalogDb.API.Entities;
using CatalogDb.API.Pagination.Filters.Products;
using FluentAssertions;

namespace CatalogDb.API.UnitTests.Pagination.Filters.Products
{
    public class ProductExactPriceFilterTests
    {
        [Fact]
        public void HandleFilter_ByExactPrice_ReturnsQueryFilteredProductsWhenMatching()
        {
            // Arrange
            IQueryable<Product> products = new List<Product>
            {
                new() { Id = 1, Price = 10.0m },
                new() { Id = 2, Price = 15.0m },
                new() { Id = 3, Price = 10.0m },
                new() { Id = 4, Price = 20.0m }
            }.AsQueryable();

            var filter = new ProductExactPriceFilter { Price = 10.0m };

            // Act
            IQueryable<Product> filteredProducts = filter.HandleFilter(products);

            // Assert
            filteredProducts.Should().HaveCount(2);
            filteredProducts.Should().OnlyContain(p => p.Price == filter.Price.Value);
        }

        [Fact]
        public void HandleFilter_ByExactPrice_ReturnsEmptyQueryWhenNoMatching()
        {
            // Arrange
            IQueryable<Product> products = new List<Product>
            {
                new() { Id = 1, Price = 10.0m },
                new() { Id = 2, Price = 15.0m },
                new() { Id = 3, Price = 10.0m },
                new() { Id = 4, Price = 20.0m }
            }.AsQueryable();

            var filter = new ProductExactPriceFilter { Price = 5.0m };

            // Act
            IQueryable<Product> filteredProducts = filter.HandleFilter(products);

            // Assert
            filteredProducts.Should().BeEmpty();
        }
    }
}
