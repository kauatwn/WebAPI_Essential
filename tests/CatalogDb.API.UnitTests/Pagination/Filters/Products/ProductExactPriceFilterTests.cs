using CatalogDb.API.Entities;
using CatalogDb.API.Pagination.Filters.Products;
using FluentAssertions;

namespace CatalogDb.API.UnitTests.Pagination.Filters.Products
{
    public class ProductExactPriceFilterTests
    {
        [Fact]
        public void HandleFilter_WhenPriceMatches_ReturnsFilteredProducts()
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
            filteredProducts.Should()
                            .NotBeEmpty();

            filteredProducts.Should()
                            .OnlyContain(p => p.Price == filter.Price.Value);

            filteredProducts.Should()
                            .HaveCount(products.Count(p => p.Price == filter.Price.Value));
        }

        [Fact]
        public void HandleFilter_WhenPriceNoMatches_ReturnsEmptyQuery()
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
            filteredProducts.Should()
                            .BeEmpty();
        }

        [Fact]
        public void HandleFilter_WhenPriceIsNotSpecified_ReturnsAllProducts()
        {
            // Arrange
            IQueryable<Product> products = new List<Product>
            {
                new() { Id = 1, Price = 10.0m },
                new() { Id = 2, Price = 15.0m },
                new() { Id = 3, Price = 10.0m },
                new() { Id = 4, Price = 20.0m }
            }.AsQueryable();

            var filter = new ProductExactPriceFilter { Price = null };

            // Act
            IQueryable<Product> filteredProducts = filter.HandleFilter(products);

            // Assert
            filteredProducts.Should()
                            .NotBeEmpty();

            filteredProducts.Should()
                            .Equal(products);
        }

        [Fact]
        public void HandleFilter_WhenListIsEmpty_ReturnsEmptyQuery()
        {
            // Arrange
            IQueryable<Product> products = new List<Product>().AsQueryable();

            var filter = new ProductExactPriceFilter { Price = 10.0m };

            // Act
            IQueryable<Product> filteredProducts = filter.HandleFilter(products);

            filter.Price = null;
            IQueryable<Product> filteredProductsWithoutSpecifiedPrice = filter.HandleFilter(products);

            // Assert
            filteredProducts.Should()
                            .BeEmpty();

            filteredProductsWithoutSpecifiedPrice.Should()
                                                 .BeEmpty();
        }
    }
}
