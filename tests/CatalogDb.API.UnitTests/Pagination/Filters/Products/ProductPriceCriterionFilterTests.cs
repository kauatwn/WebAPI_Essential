using CatalogDb.API.Entities;
using CatalogDb.API.Pagination.Filters.Products;
using FluentAssertions;

namespace CatalogDb.API.UnitTests.Pagination.Filters.Products
{
    public class ProductPriceCriterionFilterTests
    {
        [Fact]
        public void HandleFilter_WithCriterion_ReturnsFilteredQuery()
        {
            // Arrange
            IQueryable<Product> products = new List<Product>
            {
                new() { Id = 1, Price = 10.0m },
                new() { Id = 2, Price = 15.0m },
                new() { Id = 3, Price = 5.0m },
                new() { Id = 4, Price = 20.0m }
            }.AsQueryable();

            var filterGreater = new ProductPriceCriterionFilter { Criterion = "greater" };
            var filterLess = new ProductPriceCriterionFilter { Criterion = "less" };

            // Act
            IQueryable<Product> filteredQueryGreater = filterGreater.HandleFilter(products);
            IQueryable<Product> filteredQueryLess = filterLess.HandleFilter(products);

            // Assert
            filteredQueryGreater.Should().NotBeEmpty();
            filteredQueryGreater.Should().BeInDescendingOrder(p => p.Price);
            filteredQueryGreater.Should().HaveCount(products.Count());

            filteredQueryLess.Should().NotBeEmpty();
            filteredQueryLess.Should().BeInAscendingOrder(p => p.Price);
            filteredQueryLess.Should().HaveCount(products.Count());
        }

        [Fact]
        public void HandleFilter_WithoutCriterion_ReturnsUnfilteredQuery()
        {
            // Arrange
            IQueryable<Product> products = new List<Product>
            {
                new() { Id = 1, Price = 10.0m },
                new() { Id = 2, Price = 15.0m },
                new() { Id = 3, Price = 5.0m },
                new() { Id = 4, Price = 20.0m }
            }.AsQueryable();

            var filter = new ProductPriceCriterionFilter { Criterion = null };

            // Act
            IQueryable<Product> filteredQuery = filter.HandleFilter(products);

            // Assert
            filteredQuery.Should().Equal(products);
        }

        [Fact]
        public void HandleFilter_WithEmptyList_ReturnsEmptyQuery()
        {
            // Arrange
            IQueryable<Product> products = new List<Product>().AsQueryable();

            var filterGreater = new ProductPriceCriterionFilter { Criterion = "greater" };
            var filterLess = new ProductPriceCriterionFilter { Criterion = "less" };
            var filterNull = new ProductPriceCriterionFilter { Criterion = null };

            // Act
            IQueryable<Product> filteredQueryGreater = filterGreater.HandleFilter(products);
            IQueryable<Product> filteredQueryLess = filterLess.HandleFilter(products);
            IQueryable<Product> filteredQueryNull = filterNull.HandleFilter(products);

            // Assert
            filteredQueryGreater.Should().BeEmpty();
            filteredQueryLess.Should().BeEmpty();
            filteredQueryNull.Should().BeEmpty();
        }
    }
}
