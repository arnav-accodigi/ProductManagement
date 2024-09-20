using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;

namespace ProductManagement.Tests.Constants;

public static class ProductConstants
{
    public static Guid productId = new("e1a2cf5f-be7d-4591-a650-f3b0d26cc932");
    public static Guid notFoundProductId = new("14da6f7a-590c-48bf-9774-9f5678476dbb");
    public static string notFoundMessage = "Product not found";
    public static string invalidRequestBodyException = "Invalid request body";
    public static string productName = "Test product";
    public static decimal productPrice = 10m;
    public static int productStockQuantity = 100;
    public static ProductDto productResponseDto =
        new()
        {
            Id = productId,
            Name = productName,
            Price = productPrice,
            StockQuantity = productStockQuantity
        };

    public static ProductDto invalidProductRequestDto = new() { Name = "" };

    public static ProductDto productRequestDto =
        new()
        {
            Name = productName,
            Price = productPrice,
            StockQuantity = productStockQuantity
        };

    public static ProductRecord productRecord =
        new()
        {
            Id = productId,
            Name = productName,
            Price = productPrice,
            StockQuantity = productStockQuantity
        };
}
