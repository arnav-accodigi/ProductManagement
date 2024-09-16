using System.ComponentModel.DataAnnotations;
using Moq;
using ProductManagement.Data.Domain;
using ProductManagement.Data.Exceptions;
using ProductManagement.Data.Repositories;
using ProductManagement.Tests.Constants;

public class ProductRepositoryMock : Mock<IProductRepository>
{
    public void SetupGetAllProducts()
    {
        List<ProductRecord> products = [ProductConstants.productRecord];
        Setup(r => r.GetAll()).ReturnsAsync(products);
    }

    public void SetupCreateProduct()
    {
        Setup(r =>
                r.Create(It.Is<ProductRecord>(p => p.Name == ProductConstants.productRecord.Name))
            )
            .ReturnsAsync(ProductConstants.productRecord);
    }

    public void SetupCreateProductWithInvalidDto()
    {
        Setup(r => r.Create(It.Is<ProductRecord>(p => p.Name == "")))
            .ThrowsAsync(new ValidationException(ProductConstants.invalidRequestBodyException));
    }

    public void SetupGetProductById(Guid id)
    {
        Setup(r => r.GetById(id)).ReturnsAsync(ProductConstants.productRecord);
    }

    public void SetupDeleteProduct()
    {
        Setup(r => r.Delete(ProductConstants.productId)).Returns(Task.CompletedTask);
    }

    public void SetupDeleteProductNotFound()
    {
        Setup(r => r.Delete(ProductConstants.productId))
            .ThrowsAsync(new RecordNotFoundException(ProductConstants.notFoundMessage));
    }

    public void SetupUpdateProduct()
    {
        Setup(r =>
                r.Update(It.Is<ProductRecord>(p => p.Name == ProductConstants.productRecord.Name))
            )
            .Returns(Task.CompletedTask);
    }

    public void SetupUpdateProductNotFound()
    {
        Setup(r => r.Update(It.Is<ProductRecord>(p => p.Name == "")))
            .ThrowsAsync(new RecordNotFoundException(ProductConstants.notFoundMessage));
    }
}
