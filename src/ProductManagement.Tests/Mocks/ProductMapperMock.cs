using Moq;
using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;
using ProductManagement.Data.Mappers;
using ProductManagement.Tests.Constants;

namespace ProductManagement.Tests.Mocks;

public class ProductMapperMock : Mock<IProductMapper>
{
    public ProductMapperMock()
        : base(MockBehavior.Strict) { }

    public void SetupToDTO()
    {
        Setup(m =>
                m.ToDTO(It.Is<ProductRecord>(r => r.Name == ProductConstants.productRecord.Name))
            )
            .Returns(ProductConstants.productResponseDto);
    }

    public void SetupToRecord()
    {
        Setup(m =>
                m.ToRecord(
                    It.Is<ProductDto>(r => r.Name == ProductConstants.productResponseDto.Name)
                )
            )
            .Returns(ProductConstants.productRecord);
    }
}
