using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;
using ProductManagement.Data.Mappers;
using ProductManagement.Data.Repositories;

namespace ProductManagement.Services.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;

    public ProductService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        var products = await productRepository.GetAll();
        return products.Select(ProductMapper.ToProductDTO);
    }

    public async Task<ProductDto> GetProductById(Guid id)
    {
        var product = await productRepository.GetById(id);
        return ProductMapper.ToProductDTO(product);
    }

    public Task CreateProduct(ProductDto productDto)
    {
        var productRecord = ProductMapper.ToProductRecord(productDto);
        productRecord.Id = Guid.NewGuid();
        return productRepository.Create(productRecord);
    }

    public Task UpdateProduct(ProductDto productDto, Guid id)
    {
        var productRecord = ProductMapper.ToProductRecord(productDto);
        productRecord.Id = id;
        return productRepository.Update(productRecord);
    }

    public Task DeleteProduct(Guid id)
    {
        return productRepository.Delete(id);
    }
}
