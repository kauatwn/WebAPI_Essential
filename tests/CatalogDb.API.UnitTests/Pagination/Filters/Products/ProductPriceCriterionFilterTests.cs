using CatalogDb.API.Entities;
using CatalogDb.API.Pagination.Filters.Products;
using FluentAssertions;

namespace CatalogDb.API.UnitTests.Pagination.Filters.Products
{
    public class ProductPriceCriterionFilterTests
    {
        [Fact]
        public void HandleFilter_WhenPriceCriterionIsSpecified_ReturnsFilteredProducts()
        {
            // Arrange
            IQueryable<Product> products = new List<Product>
            {
                new() { Id = 1, Price = 10.0m },
                new() { Id = 2, Price = 15.0m },
                new() { Id = 3, Price = 5.0m },
                new() { Id = 4, Price = 20.0m }
            }.AsQueryable();

            // Act
            var filter = new ProductPriceCriterionFilter { PriceCriterion = "greater" };
            IQueryable<Product> filteredProductsGreater = filter.HandleFilter(products);

            filter.PriceCriterion = "less";
            IQueryable<Product> filteredProductsLess = filter.HandleFilter(products);

            // Assert
            filteredProductsGreater.Should()
                                   .BeInDescendingOrder(p => p.Price);

            filteredProductsLess.Should()
                                .BeInAscendingOrder(p => p.Price);
        }

        [Fact]
        public void HandleFilter_WhenPriceCriterionIsNotSpecified_ReturnsAllProducts()
        {
            // Arrange
            IQueryable<Product> products = new List<Product>
            {
                new() { Id = 1, Price = 10.0m },
                new() { Id = 2, Price = 15.0m },
                new() { Id = 3, Price = 5.0m },
                new() { Id = 4, Price = 20.0m }
            }.AsQueryable();

            var filter = new ProductPriceCriterionFilter { PriceCriterion = null };

            // Act
            IQueryable<Product> filteredProducts = filter.HandleFilter(products);

            // Assert
            filteredProducts.Should()
                            .Equal(products);
        }

        [Fact]
        public void HandleFilter_WhenListIsEmpty_ReturnsEmptyQuery()
        {
            // Arrange
            IQueryable<Product> products = new List<Product>().AsQueryable();

            var filter = new ProductPriceCriterionFilter { PriceCriterion = null };

            // Act
            IQueryable<Product> filteredProductsWithoutSpecifiedCriterion = filter.HandleFilter(products);

            filter.PriceCriterion = "greater";
            IQueryable<Product> filteredProductsGreater = filter.HandleFilter(products);

            filter.PriceCriterion = "less";
            IQueryable<Product> filteredProductsLess = filter.HandleFilter(products);

            // Assert
            filteredProductsWithoutSpecifiedCriterion.Should()
                                                     .BeEmpty();

            filteredProductsGreater.Should()
                                   .BeEmpty();

            filteredProductsLess.Should()
                                .BeEmpty();
        }
    }
}
