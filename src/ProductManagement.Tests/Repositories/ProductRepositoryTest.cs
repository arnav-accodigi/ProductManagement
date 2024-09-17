using ProductManagement.Data.Domain;
using ProductManagement.Data.Exceptions;
using ProductManagement.Data.Repositories;
using ProductManagement.Tests.Constants;
using Xunit;

namespace ProductManagement.Tests.Repositories;

public class ProductRepositoryTests
{
    private readonly ProductRepository productRepository;

    public ProductRepositoryTests()
    {
        productRepository = new ProductRepository();
    }

    [Fact]
    public async Task GetAllProducts_ReturnsProducts_Empty()
    {
        // Arrange/Act
        var products = await productRepository.GetAll();

        // Assert
        Assert.Empty(products);
    }

    [Fact]
    public async Task Create_AddsProduct()
    {
        // Arrange
        var product = ProductConstants.productRecord;

        // Act
        var result = await productRepository.Create(product);
        var products = await productRepository.GetAll();

        // Assert
        Assert.Equal(product, result);
        Assert.Single(products);
        Assert.Contains(product, products);
    }

    [Fact]
    public async Task GetById_ReturnsProduct()
    {
        // Arrange
        var product = ProductConstants.productRecord;

        // Act
        await productRepository.Create(product);
        var result = await productRepository.GetById(product.Id);

        // Assert
        Assert.Equal(product, result);
    }

    [Fact]
    public async Task GetById_ThrowsExceptionWhenNotFound()
    {
        // Arrange
        var notFoundId = ProductConstants.notFoundProductId;

        // Act/Assert
        var exception = await Assert.ThrowsAsync<RecordNotFoundException>(
            () => productRepository.GetById(notFoundId)
        );
        Assert.Equal("Product not found", exception.Message);
    }

    [Fact]
    public async Task Update_UpdatesProduct()
    {
        // Arrange
        var product = ProductConstants.productRecord;
        await productRepository.Create(product);

        var updatedProduct = new ProductRecord
        {
            Id = product.Id,
            Name = "Updated Product",
            Price = ProductConstants.productRecord.Price,
            StockQuantity = ProductConstants.productRecord.StockQuantity
        };

        // Act
        await productRepository.Update(updatedProduct);

        // Assert
        var result = await productRepository.GetById(product.Id);
        Assert.Equal("Updated Product", result.Name);
    }

    [Fact]
    public async Task Update_ThrowsExceptionWhenNotFound()
    {
        // Arrange
        var notFoundProduct = new ProductRecord { Id = ProductConstants.notFoundProductId, };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RecordNotFoundException>(
            () => productRepository.Update(notFoundProduct)
        );
        Assert.Equal("Product not found", exception.Message);
    }

    [Fact]
    public async Task Delete_ShouldRemoveProduct()
    {
        // Arrange
        var product = ProductConstants.productRecord;
        await productRepository.Create(product);

        // Act
        await productRepository.Delete(product.Id);

        // Assert
        var exception = await Assert.ThrowsAsync<RecordNotFoundException>(
            () => productRepository.GetById(product.Id)
        );
        Assert.Equal("Product not found", exception.Message);
    }

    [Fact]
    public async Task Delete_ThrowsExceptionWhenNotFound()
    {
        // Arrange
        var notFoundId = ProductConstants.notFoundProductId;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RecordNotFoundException>(
            () => productRepository.Delete(notFoundId)
        );
        Assert.Equal("Product not found", exception.Message);
    }
}
