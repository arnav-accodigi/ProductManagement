using System.ComponentModel.DataAnnotations;
using Moq;
using ProductManagement.Data.Domain;
using ProductManagement.Data.Exceptions;
using ProductManagement.Data.Repositories;
using ProductManagement.Tests.Constants;

namespace ProductManagement.Tests.Mocks;

public class ProductRepositoryMock : Mock<IProductRepository>
{
    public ProductRepositoryMock()
        : base(MockBehavior.Strict) { }

    private bool Matches(ProductRecord productRecord)
    {
        return productRecord.Name == ProductConstants.productName
            && productRecord.Price == ProductConstants.productPrice
            && productRecord.StockQuantity == ProductConstants.productStockQuantity;
    }

    public void SetupGetAllProducts()
    {
        Setup(r => r.GetAll()).ReturnsAsync([ProductConstants.productRecord]);
    }

    public void SetupCreateProduct()
    {
        Setup(r => r.Create(It.Is<ProductRecord>(p => Matches(p))))
            .ReturnsAsync(ProductConstants.productRecord);
    }

    public void SetupCreateProductWithInvalidDto()
    {
        Setup(r => r.Create(It.Is<ProductRecord>(p => p.Name == "")))
            .ThrowsAsync(new ValidationException(ProductConstants.invalidRequestBodyException));
    }

    public void SetupGetProductById()
    {
        Setup(r => r.GetById(ProductConstants.productId))
            .ReturnsAsync(ProductConstants.productRecord);
    }

    public void SetupDeleteProduct()
    {
        Setup(r => r.Delete(ProductConstants.productId)).Returns(Task.CompletedTask);
    }

    public void SetupDeleteProductNotFound()
    {
        Setup(r => r.Delete(ProductConstants.notFoundProductId))
            .ThrowsAsync(new RecordNotFoundException(ProductConstants.notFoundMessage));
    }

    public void SetupUpdateProduct()
    {
        Setup(r => r.Update(It.Is<ProductRecord>(p => Matches(p)))).Returns(Task.CompletedTask);
    }

    public void SetupUpdateProductNotFound()
    {
        Setup(r => r.Update(It.Is<ProductRecord>(p => Matches(p))))
            .ThrowsAsync(new RecordNotFoundException(ProductConstants.notFoundMessage));
    }

    public void SetupUpdateProductInvalidInput()
    {
        Setup(r => r.Update(It.Is<ProductRecord>(p => p.Name == "")))
            .ThrowsAsync(new ValidationException(ProductConstants.invalidRequestBodyException));
    }
}
