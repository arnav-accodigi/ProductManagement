using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Controllers;
using ProductManagement.Data.DTO;
using ProductManagement.Data.Exceptions;
using ProductManagement.Tests.Constants;
using ProductManagement.Tests.Mocks;
using Xunit;

namespace ProductManagement.Tests.Controllers;

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
    public async Task CreateProduct_ReturnsErrorResponseWithInvalidInput()
    {
        // Arrange
        productServiceMock.SetupCreateProductWithInvalidDto();

        // Act
        var response =
            await productController.CreateProduct(ProductConstants.invalidProductRequestDto)
            as ObjectResult;

        // Assert
        Assert.NotNull(response?.Value);
        var errorMessage = response.Value as ErrorResponse;
        Assert.NotNull(errorMessage?.Error);
        Assert.Equal("Invalid request body", errorMessage?.Error);
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
    public async Task GetProduct_ReturnsErrorResponseWhenNotFound()
    {
        // Arrange
        productServiceMock.SetupGetProductByIdNotFound();

        // Act
        var response =
            await productController.GetProduct(ProductConstants.notFoundProductId) as ObjectResult;

        // Assert
        Assert.NotNull(response?.Value);
        var errorMessage = response.Value as ErrorResponse;
        Assert.NotNull(errorMessage?.Error);
        Assert.Equal("Product not found", errorMessage?.Error);
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
    public async Task DeleteProduct_ReturnsErrorResponseWhenNotFound()
    {
        // Arrange
        productServiceMock.SetupDeleteProductNotFound();

        // Act
        var response =
            await productController.DeleteProduct(ProductConstants.notFoundProductId)
            as ObjectResult;

        // Assert
        Assert.NotNull(response?.Value);
        var errorMessage = response.Value as ErrorResponse;
        Assert.NotNull(errorMessage?.Error);
        Assert.Equal("Product not found", errorMessage?.Error);
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
    public async Task UpdateProduct_ReturnsErrorResponseWhenNotFound()
    {
        // Arrange
        productServiceMock.SetupUpdateProductNotFound();

        // Act
        var response =
            await productController.UpdateProduct(
                ProductConstants.productRequestDto,
                ProductConstants.notFoundProductId
            ) as ObjectResult;

        // Assert
        Assert.NotNull(response?.Value);
        var errorMessage = response.Value as ErrorResponse;
        Assert.NotNull(errorMessage?.Error);
        Assert.Equal("Product not found", errorMessage?.Error);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsErrorResponseWhenInvalidInput()
    {
        // Arrange
        productServiceMock.SetupUpdateProductInvalidInput();

        // Act
        var response =
            await productController.UpdateProduct(
                ProductConstants.invalidProductRequestDto,
                ProductConstants.productId
            ) as ObjectResult;

        // Assert
        Assert.NotNull(response?.Value);
        var errorMessage = response.Value as ErrorResponse;
        Assert.NotNull(errorMessage?.Error);
        Assert.Equal("Invalid request body", errorMessage?.Error);
    }
}
