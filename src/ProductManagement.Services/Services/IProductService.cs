using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;

namespace ProductManagement.Services.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAllProducts();
    Task<ProductResponseDto> GetProductById(Guid id);
    Task<ProductRecord> CreateProduct(ProductRequestDto productDto);
    Task UpdateProduct(ProductRequestDto productDto, Guid id);
    Task DeleteProduct(Guid id);
}
