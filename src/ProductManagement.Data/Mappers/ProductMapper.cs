using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;

namespace ProductManagement.Data.Mappers;

public class ProductMapper : IProductMapper
{
    public ProductDto ToDTO(ProductRecord productRecord)
    {
        return new ProductDto()
        {
            Id = productRecord.Id,
            Name = productRecord.Name,
            Price = productRecord.Price,
            StockQuantity = productRecord.StockQuantity
        };
    }

    public ProductRecord ToRecord(ProductDto productDto)
    {
        return new ProductRecord()
        {
            Name = productDto.Name,
            Price = productDto.Price,
            StockQuantity = productDto.StockQuantity
        };
    }
}
