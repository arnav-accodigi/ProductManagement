using ProductManagement.Data.DTO;

namespace ProductManagement.Data.Validation.Product;

public class ProductValidator : IProductValidator
{
    public void Validate(ProductDto product)
    {
        Validator
            .Create()
            .NotNull(nameof(product), product)
            .NotEmptyString(nameof(product.Name), product.Name)
            .MinValue(nameof(product.Price), product.Price, 1m)
            .MinValue(nameof(product.StockQuantity), product.StockQuantity, 1)
            .ThrowIfInvalid();
    }
}
