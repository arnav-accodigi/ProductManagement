using System.ComponentModel.DataAnnotations;
using Moq;
using ProductManagement.Data.DTO;
using ProductManagement.Data.Exceptions;
using ProductManagement.Data.Validation.Product;
using ProductManagement.Services.Services;
using ProductManagement.Tests.Constants;
using ProductManagement.Tests.Mocks;
using Xunit;

namespace ProductManagement.Tests.Services;

public class ProductServiceTest : IDisposable
{
    private readonly ProductRepositoryMock productRepositoryMock;
    private readonly Mock<IProductValidator> productValidatorMock;
    private readonly IProductService productService;

    public ProductServiceTest()
    {
        productRepositoryMock = new ProductRepositoryMock();
        productValidatorMock = new Mock<IProductValidator>();

        productService = new ProductService(
            productRepositoryMock.Object,
            productValidatorMock.Object
        );
    }

    public void Dispose()
    {
        productRepositoryMock.VerifyAll();
        productValidatorMock.VerifyAll();
    }

    [Fact]
    public async Task GetAllProducts_ReturnsProductList()
    {
        productRepositoryMock.SetupGetAllProducts();

        // Act
        var productsReturned = await productService.GetAllProducts();

        // Assert
        Assert.Single(productsReturned);
        Assert.IsType<ProductDto>(productsReturned.Single());
    }

    [Fact]
    public async Task GetProductById_ReturnsProductWithId()
    {
        // Arrange
        productRepositoryMock.SetupGetProductById();

        // Act
        var product = await productService.GetProductById(ProductConstants.productId);

        // Assert
        Assert.NotNull(product);
        Assert.IsType<ProductDto>(product);
        Assert.Equal(ProductConstants.productId, product.Id);
    }

    [Fact]
    public async Task CreateProduct_ReturnsCreatedProduct()
    {
        // Arrange
        productRepositoryMock.SetupCreateProduct();

        // Act
        var createdProduct = await productService.CreateProduct(ProductConstants.productRequestDto);

        // Assert
        Assert.NotNull(createdProduct);
        Assert.IsType<ProductDto>(createdProduct);
    }

    [Fact]
    public async Task CreateProduct_ThrowsExceptionWhenInvalidInput()
    {
        // Arrange
        productValidatorMock
            .Setup(v => v.Validate(ProductConstants.invalidProductRequestDto))
            .Throws(new ValidationException(ProductConstants.invalidRequestBodyException));

        // Act
        Exception exception = await Assert.ThrowsAsync<ValidationException>(
            () => productService.CreateProduct(ProductConstants.invalidProductRequestDto)
        );

        // Assert
        Assert.Equal(ProductConstants.invalidRequestBodyException, exception.Message);
    }

    [Fact]
    public async Task DeleteProduct_DeletesProductWhenItExists()
    {
        // Arrange
        productRepositoryMock.SetupDeleteProduct();

        // Act
        await productService.DeleteProduct(ProductConstants.productId);
    }

    [Fact]
    public async Task DeleteProduct_ThrowsExceptionWhenProductNotFound()
    {
        // Arrange
        productRepositoryMock.SetupDeleteProductNotFound();

        // Act
        Exception exception = await Assert.ThrowsAsync<RecordNotFoundException>(
            () => productService.DeleteProduct(ProductConstants.notFoundProductId)
        );

        // Assert
        Assert.Equal(ProductConstants.notFoundMessage, exception.Message);
    }

    [Fact]
    public async Task UpdateProduct_UpdatesProductIfItExists()
    {
        // Arrange
        productRepositoryMock.SetupUpdateProduct();

        // Act
        var exception = await Record.ExceptionAsync(
            () =>
                productService.UpdateProduct(
                    ProductConstants.productRequestDto,
                    ProductConstants.productId
                )
        );

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public async Task UpdateProduct_ThrowsExceptionWhenProductNotFound()
    {
        // Arrange
        productRepositoryMock.SetupUpdateProductNotFound();

        // Act
        Exception exception = await Assert.ThrowsAsync<RecordNotFoundException>(
            () =>
                productService.UpdateProduct(
                    ProductConstants.productRequestDto,
                    ProductConstants.productId
                )
        );

        // Assert
        Assert.Equal(ProductConstants.notFoundMessage, exception.Message);
    }
}
