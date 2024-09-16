using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;

namespace ProductManagement.Data.Mappers;

public static class ProductMapper
{
    public static ProductDto ToProductDTO(ProductRecord productRecord)
    {
        return new ProductDto()
        {
            Id = productRecord.Id,
            Name = productRecord.Name,
            Price = productRecord.Price,
            StockQuantity = productRecord.StockQuantity
        };
    }

    public static ProductRecord ToProductRecord(ProductDto productDto, Guid id)
    {
        return new ProductRecord()
        {
            Id = id,
            Name = productDto.Name,
            Price = productDto.Price,
            StockQuantity = productDto.StockQuantity
        };
    }
}
