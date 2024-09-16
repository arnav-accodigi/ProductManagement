using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;

namespace ProductManagement.Data.Mappers;

public interface IProductMapper
{
    ProductDto ToDTO(ProductRecord productRecord);
    ProductRecord ToRecord(ProductDto productDto);
}
