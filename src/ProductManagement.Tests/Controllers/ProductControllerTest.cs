using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Controllers;
using ProductManagement.Data.DTO;
using ProductManagement.Data.Exceptions;
using ProductManagement.Tests.Constants;
using ProductManagement.Tests.Mocks;
using Xunit;

public class ProductControllerTest : IDisposable
{
    public ProductController productController;
    public ProductServiceMock productServiceMock;

    public ProductControllerTest()
    {
        this.productServiceMock = new ProductServiceMock();
        this.productController = new ProductController(productServiceMock.Object);
    }

    public void Dispose()
    {
        productServiceMock.VerifyAll();
    }

    [Fact]
    public async Task GetAllProducts_ReturnsAllProducts()
    {
        // Arrange
        productServiceMock.SetupGetAllProducts();

        // Act
        var products = await productController.GetAllProducts() as OkObjectResult;

        // Assert
        Assert.NotNull(products);
        Assert.Equal(StatusCodes.Status200OK, products.StatusCode);
        Assert.NotNull(products.Value);
        Assert.Single((IEnumerable<ProductDto>)products.Value);
    }

    [Fact]
    public async Task CreateProduct_CreatesNewProduct()
    {
        // Arrange
        productServiceMock.SetupCreateProduct();

        // Act
        var product =
            await productController.CreateProduct(ProductConstants.productRequestDto)
            as CreatedAtActionResult;

        // Assert
        Assert.NotNull(product);
        Assert.Equal(StatusCodes.Status201Created, product.StatusCode);
        Assert.NotNull(product.Value);
        Assert.IsType<ProductDto>(product.Value);
        Assert.Equal(ProductConstants.productResponseDto, product.Value);
    }

    [Fact]
    public async Task CreateProduct_ThrowsExceptionWhenInvalidInput()
    {
        // Arrange
        productServiceMock.SetupCreateProductWithInvalidDto();

        // Act
        Exception exception = await Assert.ThrowsAsync<ValidationException>(
            () => productController.CreateProduct(ProductConstants.invalidProductRequestDto)
        );

        // Assert
        Assert.IsType<ValidationException>(exception);
        Assert.Equal(ProductConstants.invalidRequestBodyException, exception.Message);
    }

    [Fact]
    public async Task GetProduct_ReturnsProductWithId()
    {
        // Arrange
        productServiceMock.SetupGetProductById();

        // Act
        var product =
            await productController.GetProduct(ProductConstants.productId) as OkObjectResult;

        // Assert
        Assert.NotNull(product);
        Assert.Equal(StatusCodes.Status200OK, product.StatusCode);
        Assert.NotNull(product.Value);
        Assert.IsType<ProductDto>(product.Value);
        Assert.Equal(ProductConstants.productResponseDto, product.Value);
    }

    [Fact]
    public async Task GetProduct_ThrowsExceptionWhenNotFound()
    {
        // Arrange
        productServiceMock.SetupGetProductByIdNotFound();

        // Act
        Exception exception = await Assert.ThrowsAsync<RecordNotFoundException>(
            () => productController.GetProduct(ProductConstants.notFoundProductId)
        );

        // Assert
        Assert.IsType<RecordNotFoundException>(exception);
        Assert.Equal(ProductConstants.notFoundMessage, exception.Message);
    }

    [Fact]
    public async Task DeleteProduct_DeletesProductWithId()
    {
        // Arrange
        productServiceMock.SetupDeleteProduct();

        // Act
        await productController.DeleteProduct(ProductConstants.productId);
    }

    [Fact]
    public async Task DeleteProduct_ThrowsExceptionWhenNotFound()
    {
        // Arrange
        productServiceMock.SetupDeleteProductNotFound();

        // Act
        Exception exception = await Assert.ThrowsAsync<RecordNotFoundException>(
            () => productController.DeleteProduct(ProductConstants.notFoundProductId)
        );

        // Assert
        Assert.IsType<RecordNotFoundException>(exception);
        Assert.Equal(ProductConstants.notFoundMessage, exception.Message);
    }

    [Fact]
    public async Task UpdateProduct_UpdatesProductWithId()
    {
        // Arrange
        productServiceMock.SetupUpdateProduct();

        // Act
        await productController.UpdateProduct(
            ProductConstants.productResponseDto,
            ProductConstants.productId
        );
    }

    [Fact]
    public async Task UpdateProduct_ThrowsExceptionWhenNotFound()
    {
        // Arrange
        productServiceMock.SetupUpdateProductNotFound();

        // Act
        Exception exception = await Assert.ThrowsAsync<RecordNotFoundException>(
            () =>
                productController.UpdateProduct(
                    ProductConstants.productRequestDto,
                    ProductConstants.notFoundProductId
                )
        );

        // Assert
        Assert.IsType<RecordNotFoundException>(exception);
        Assert.Equal(ProductConstants.notFoundMessage, exception.Message);
    }

    [Fact]
    public async Task UpdateProduct_ThrowsExceptionWhenInvalidInput()
    {
        // Arrange
        productServiceMock.SetupUpdateProductInvalidInput();

        // Act
        Exception exception = await Assert.ThrowsAsync<ValidationException>(
            () =>
                productController.UpdateProduct(
                    ProductConstants.invalidProductRequestDto,
                    ProductConstants.productId
                )
        );

        // Assert
        Assert.IsType<ValidationException>(exception);
        Assert.Equal(ProductConstants.invalidRequestBodyException, exception.Message);
    }
}
