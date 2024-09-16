using ProductManagement.Data.DTO;

namespace ProductManagement.Data.Validation.Product;

public interface IProductValidator
{
    void Validate(ProductDto product);
}
