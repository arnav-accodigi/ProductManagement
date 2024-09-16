using System.ComponentModel.DataAnnotations;
using Moq;
using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;
using ProductManagement.Data.Exceptions;
using ProductManagement.Data.Validation.Product;
using ProductManagement.Services.Services;
using ProductManagement.Tests.Constants;
using Xunit;

namespace ProductManagement.Tests.Services;

public class ProductServiceTest : IDisposable
{
    private readonly ProductRepositoryMock productRepositoryMock;
    private readonly Mock<IProductValidator> productValidatorMock;
    private readonly ProductMapperMock productMapperMock;

    private readonly IProductService productService;

    public ProductServiceTest()
    {
        productRepositoryMock = new ProductRepositoryMock();
        productValidatorMock = new Mock<IProductValidator>();
        productMapperMock = new ProductMapperMock();

        productService = new ProductService(
            productRepositoryMock.Object,
            productValidatorMock.Object,
            productMapperMock.Object
        );
    }

    public void Dispose()
    {
        productRepositoryMock.VerifyAll();
        productValidatorMock.VerifyAll();
        productMapperMock.VerifyAll();
    }

    [Fact]
    public async Task GetAllProducts_ReturnsProductList()
    {
        productRepositoryMock.SetupGetAllProducts();
        productMapperMock.SetupToDTO();

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
        productRepositoryMock.SetupGetProductById(ProductConstants.productId);
        productMapperMock.SetupToDTO();

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
        productMapperMock.SetupToDTO();
        productMapperMock.SetupToRecord();

        // Act
        var createdProduct = await productService.CreateProduct(ProductConstants.productDto);

        // Assert
        Assert.NotNull(createdProduct);
        Assert.IsType<ProductDto>(createdProduct);
    }

    [Fact]
    public async Task CreateProduct_ThrowsExceptionWhenInvalidInput()
    {
        // Arrange
        productRepositoryMock.SetupCreateProductWithInvalidDto();
        productMapperMock.SetupToDTO();
        productMapperMock.SetupToRecord();

        // Act
        Exception exception = await Assert.ThrowsAsync<ValidationException>(
            () => productService.CreateProduct(new ProductDto { Name = "" })
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

        // Verify
        productRepositoryMock.Verify(r => r.Delete(ProductConstants.productId), Times.Once);
    }

    [Fact]
    public async Task DeleteProduct_ThrowsExceptionWhenProductNotFound()
    {
        // Arrange
        productRepositoryMock.SetupDeleteProductNotFound();

        // Act
        Exception exception = await Assert.ThrowsAsync<RecordNotFoundException>(
            () => productService.DeleteProduct(ProductConstants.productId)
        );

        // Assert
        Assert.Equal(ProductConstants.notFoundMessage, exception.Message);
    }

    [Fact]
    public async Task UpdateProduct_UpdatesProductIfItExists()
    {
        // Arrange
        productRepositoryMock.SetupUpdateProduct();
        productMapperMock.SetupToDTO();
        productMapperMock.SetupToRecord();

        // Act
        await productService.UpdateProduct(ProductConstants.productDto, ProductConstants.productId);

        // Assert
        productRepositoryMock.Verify(
            r => r.Update(It.Is<ProductRecord>(p => p.Name == ProductConstants.productRecord.Name)),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateProduct_ThrowsExceptionWhenProductNotFound()
    {
        // Arrange
        productRepositoryMock.SetupUpdateProductNotFound();
        productMapperMock.SetupToDTO();
        productMapperMock.SetupToRecord();

        // Act
        Exception exception = await Assert.ThrowsAsync<RecordNotFoundException>(
            () =>
                productService.UpdateProduct(
                    new ProductDto
                    {
                        Name = "",
                        Price = ProductConstants.productDto.Price,
                        StockQuantity = ProductConstants.productDto.StockQuantity
                    },
                    ProductConstants.productId
                )
        );

        // Assert
        Assert.Equal(ProductConstants.notFoundMessage, exception.Message);
    }
}
