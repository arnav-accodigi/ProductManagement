using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;

namespace ProductManagement.Services.Extensions;

public static class RecordExtensions
{
    public static ProductDto ToDto(this ProductRecord productRecord) =>
        new()
        {
            Id = productRecord.Id,
            Name = productRecord.Name,
            Price = productRecord.Price,
            StockQuantity = productRecord.StockQuantity
        };
}
