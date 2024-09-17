using System.ComponentModel.DataAnnotations;
using Moq;
using ProductManagement.Data.DTO;
using ProductManagement.Data.Exceptions;
using ProductManagement.Services.Services;
using ProductManagement.Tests.Constants;

namespace ProductManagement.Tests.Mocks;

public class ProductServiceMock : Mock<IProductService>
{
    public ProductServiceMock()
        : base(MockBehavior.Strict) { }

    public void SetupGetAllProducts()
    {
        Setup(s => s.GetAllProducts()).ReturnsAsync([ProductConstants.productResponseDto]);
    }

    public void SetupCreateProduct()
    {
        Setup(s =>
                s.CreateProduct(
                    It.Is<ProductDto>(p =>
                        p.Name == ProductConstants.productRequestDto.Name
                        && p.Price == ProductConstants.productRequestDto.Price
                        && p.StockQuantity == ProductConstants.productRequestDto.StockQuantity
                    )
                )
            )
            .ReturnsAsync(ProductConstants.productResponseDto);
    }

    public void SetupCreateProductWithInvalidDto()
    {
        Setup(s => s.CreateProduct(It.Is<ProductDto>(p => p.Name == "")))
            .ThrowsAsync(new ValidationException(ProductConstants.invalidRequestBodyException));
    }

    public void SetupGetProductById()
    {
        Setup(r => r.GetProductById(ProductConstants.productId))
            .ReturnsAsync(ProductConstants.productResponseDto);
    }

    public void SetupGetProductByIdNotFound()
    {
        Setup(s => s.GetProductById(ProductConstants.notFoundProductId))
            .ThrowsAsync(new RecordNotFoundException(ProductConstants.notFoundMessage));
    }

    public void SetupDeleteProduct()
    {
        Setup(r => r.DeleteProduct(ProductConstants.productId)).Returns(Task.CompletedTask);
    }

    public void SetupDeleteProductNotFound()
    {
        Setup(r => r.DeleteProduct(ProductConstants.notFoundProductId))
            .ThrowsAsync(new RecordNotFoundException(ProductConstants.notFoundMessage));
    }

    public void SetupUpdateProduct()
    {
        Setup(r =>
                r.UpdateProduct(
                    It.Is<ProductDto>(p =>
                        p.Name == ProductConstants.productRequestDto.Name
                        && p.Price == ProductConstants.productRequestDto.Price
                        && p.StockQuantity == ProductConstants.productRequestDto.StockQuantity
                    ),
                    ProductConstants.productId
                )
            )
            .Returns(Task.CompletedTask);
    }

    public void SetupUpdateProductNotFound()
    {
        Setup(r =>
                r.UpdateProduct(
                    It.Is<ProductDto>(p =>
                        p.Name == ProductConstants.productRequestDto.Name
                        && p.Price == ProductConstants.productRequestDto.Price
                        && p.StockQuantity == ProductConstants.productRequestDto.StockQuantity
                    ),
                    ProductConstants.notFoundProductId
                )
            )
            .ThrowsAsync(new RecordNotFoundException(ProductConstants.notFoundMessage));
    }

    public void SetupUpdateProductInvalidInput()
    {
        Setup(r =>
                r.UpdateProduct(It.Is<ProductDto>(p => p.Name == ""), ProductConstants.productId)
            )
            .ThrowsAsync(new ValidationException(ProductConstants.invalidRequestBodyException));
    }
}
