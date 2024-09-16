using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;

namespace ProductManagement.Tests.Constants;

public static class ProductConstants
{
    public static Guid productId = new Guid("e1a2cf5f-be7d-4591-a650-f3b0d26cc932");
    public static string notFoundMessage = "Product not found";
    public static string invalidRequestBodyException = "Invalid request body";
    public static ProductDto productDto =
        new()
        {
            Id = productId,
            Name = "Test product",
            Price = 10m,
            StockQuantity = 100
        };

    public static ProductRecord productRecord =
        new()
        {
            Id = productId,
            Name = "Test product",
            Price = 10m,
            StockQuantity = 100
        };
}
