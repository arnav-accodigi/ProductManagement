using Moq;
using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;
using ProductManagement.Data.Mappers;
using ProductManagement.Tests.Constants;

public class ProductMapperMock : Mock<IProductMapper>
{
    public void SetupToDTO()
    {
        Setup(m =>
                m.ToDTO(It.Is<ProductRecord>(r => r.Name == ProductConstants.productRecord.Name))
            )
            .Returns(ProductConstants.productDto);
    }

    public void SetupToRecord()
    {
        Setup(m => m.ToRecord(It.Is<ProductDto>(r => r.Name == ProductConstants.productDto.Name)))
            .Returns(ProductConstants.productRecord);
    }
}
