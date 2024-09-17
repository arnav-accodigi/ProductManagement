using System.ComponentModel.DataAnnotations;
using ProductManagement.Data.DTO;
using ProductManagement.Tests.Constants;
using Xunit;
using Validator = ProductManagement.Data.Validation.Validator;

namespace ProductManagement.Tests.Validators;

public class ValidatorTest
{
    public Validator validator;

    public ValidatorTest()
    {
        validator = Validator.Create();
    }

    [Fact]
    public void NotNull_ThrowsExceptionWhenValueIsNull()
    {
        // Arrange
        ProductDto? product = null;

        // Act
        validator.NotNull<ProductDto>(nameof(product), product);

        // Assert
        var exception = Assert.Throws<ValidationException>(validator.ThrowIfInvalid);
        Assert.Equal("product can not be null", exception.Message);
    }

    [Fact]
    public void NotNull_ValidatesWhenValueIsNotNull()
    {
        // Arrange
        ProductDto product = new() { Name = "Test" };

        // Act
        validator.NotNull(nameof(product.Name), product.Name);

        // Assert
        var exception = Record.Exception(validator.ThrowIfInvalid);
        Assert.Null(exception);
    }

    [Fact]
    public void MinValue_ThrowsExceptionWhenValueIsLessThanMinValue()
    {
        // Arrange
        ProductDto product = new() { Price = 100 };

        // Act
        validator.MinValue(nameof(product.Price), product.Price, 1000);

        // Assert
        var exception = Assert.Throws<ValidationException>(validator.ThrowIfInvalid);
        Assert.Equal("Price must be greater than or equal to 1000", exception.Message);
    }

    [Fact]
    public void MinValue_ValidatesWhenValueIsEqualToMinValue()
    {
        // Arrange
        ProductDto product = new() { Price = 100 };

        // Act
        validator.MinValue(nameof(product.Price), product.Price, 100);

        // Assert
        var exception = Record.Exception(validator.ThrowIfInvalid);
        Assert.Null(exception);
    }

    [Fact]
    public void NotEmptyString_ThrowsExceptionWhenStringIsEmpty()
    {
        // Arrange
        ProductDto product = ProductConstants.invalidProductRequestDto;

        // Act
        validator.NotEmptyString(nameof(product.Name), product.Name);

        // Assert
        var exception = Assert.Throws<ValidationException>(validator.ThrowIfInvalid);
        Assert.Equal("Name cannot be empty", exception.Message);
    }

    [Fact]
    public void ValidObject_ValidatesObject()
    {
        // Arrange
        ProductDto product = ProductConstants.productRequestDto;

        // Act
        validator
            .NotNull<ProductDto>(nameof(product), product)
            .MinValue(nameof(product.Price), product.Price, 1m)
            .MinValue(nameof(product.StockQuantity), product.StockQuantity, 10);

        // Assert
        var exception = Record.Exception(validator.ThrowIfInvalid);
        Assert.Null(exception);
    }
}
