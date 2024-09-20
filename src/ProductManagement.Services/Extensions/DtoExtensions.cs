using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;

namespace ProductManagement.Services.Extensions;

public static class DtoExtensions
{
    public static ProductRecord ToRecord(this ProductDto productDto) =>
        new()
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Price = productDto.Price,
            StockQuantity = productDto.StockQuantity
        };
}
