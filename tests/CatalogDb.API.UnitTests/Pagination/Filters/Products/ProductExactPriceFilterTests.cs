using CatalogDb.API.Entities;
using CatalogDb.API.Pagination.Filters.Products;
using FluentAssertions;

namespace CatalogDb.API.UnitTests.Pagination.Filters.Products
{
    public class ProductExactPriceFilterTests
    {
        [Fact]
        public void HandleFilter_PriceMatches_ReturnsFilteredQuery()
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
            IQueryable<Product> filteredQuery = filter.HandleFilter(products);

            // Assert
            filteredQuery.Should().NotBeEmpty();
            filteredQuery.Should().OnlyContain(p => p.Price == filter.Price.Value);
            filteredQuery.Should().HaveCount(products.Count(p => p.Price == filter.Price.Value));
        }

        [Fact]
        public void HandleFilter_PriceNoMatches_ReturnsEmptyQuery()
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
            IQueryable<Product> filteredQuery = filter.HandleFilter(products);

            // Assert
            filteredQuery.Should().BeEmpty();
        }

        [Fact]
        public void HandleFilter_WithoutPrice_ReturnsUnfilteredQuery()
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
            IQueryable<Product> query = filter.HandleFilter(products);

            // Assert
            query.Should().NotBeEmpty();
            query.Should().Equal(products);
        }

        [Fact]
        public void HandleFilter_EmptyList_ReturnsEmptyQuery()
        {
            // Arrange
            IQueryable<Product> products = new List<Product>().AsQueryable();

            var filterWithPrice = new ProductExactPriceFilter { Price = 10.0m };
            var filterWithoutPrice = new ProductExactPriceFilter { Price = null };

            // Act
            IQueryable<Product> queryWithPrice = filterWithPrice.HandleFilter(products);

            IQueryable<Product> queryWithoutPrice = filterWithoutPrice.HandleFilter(products);

            // Assert
            queryWithPrice.Should().BeEmpty();
            queryWithoutPrice.Should().BeEmpty();
        }
    }
}
