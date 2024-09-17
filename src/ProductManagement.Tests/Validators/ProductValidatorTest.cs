using System.ComponentModel.DataAnnotations;
using ProductManagement.Data.DTO;
using ProductManagement.Data.Validation.Product;
using Xunit;

namespace ProductManagement.Tests.Validators;

public class ProductValidatorTest
{
    public IProductValidator productValidator;

    public ProductValidatorTest()
    {
        productValidator = new ProductValidator();
    }

    [Theory]
    [InlineData("Bread", 10, 200)]
    [InlineData("Butter", 20, 100)]
    [InlineData("Jam", 30, 150)]
    public void Validates_ProductDtoIsValid(string name, decimal price, int stockQuantity)
    {
        // Arrange
        var product = new ProductDto
        {
            Name = name,
            Price = price,
            StockQuantity = stockQuantity
        };

        // Act
        var exception = Record.Exception(() => productValidator.Validate(product));

        // Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("Bread", 0, 200)]
    [InlineData("Butter", 0, 100)]
    [InlineData("Jam", 0, 150)]
    public void ThrowsException_ProductDtoHasLessThanMinimumPrice(
        string name,
        decimal price,
        int stockQuantity
    )
    {
        // Arrange
        var product = new ProductDto
        {
            Name = name,
            Price = price,
            StockQuantity = stockQuantity
        };

        // Act
        var exception = Assert.Throws<ValidationException>(
            () => productValidator.Validate(product)
        );

        // Assert
        Assert.Equal("Price must be greater than or equal to 1", exception.Message);
    }

    [Theory]
    [InlineData("           ", 10, 0)]
    [InlineData("", 20, 0)]
    [InlineData(null, 30, 0)]
    public void ThrowsException_ProductDtoHasBlankNameAndLessThanMinimumStockQuantity(
        string name,
        decimal price,
        int stockQuantity
    )
    {
        // Arrange
        var product = new ProductDto
        {
            Name = name,
            Price = price,
            StockQuantity = stockQuantity
        };

        // Act
        var exception = Assert.Throws<ValidationException>(
            () => productValidator.Validate(product)
        );

        // Assert
        Assert.Equal(
            "Name cannot be empty, StockQuantity must be greater than or equal to 1",
            exception.Message
        );
    }
}
