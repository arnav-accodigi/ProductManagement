using ProductManagement.Data.DTO;
using ProductManagement.Data.Mappers;
using ProductManagement.Data.Repositories;
using ProductManagement.Data.Validation.Product;

namespace ProductManagement.Services.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IProductValidator productValidator;
    private readonly IProductMapper productMapper;

    public ProductService(
        IProductRepository productRepository,
        IProductValidator productValidator,
        IProductMapper productMapper
    )
    {
        this.productRepository = productRepository;
        this.productValidator = productValidator;
        this.productMapper = productMapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        var products = await productRepository.GetAll();
        return products.Select(productMapper.ToDTO);
    }

    public async Task<ProductDto> GetProductById(Guid id)
    {
        var product = await productRepository.GetById(id);
        return productMapper.ToDTO(product);
    }

    public async Task<ProductDto> CreateProduct(ProductDto productDto)
    {
        productValidator.Validate(productDto);
        var productRecord = productMapper.ToRecord(productDto);
        productRecord.Id = Guid.NewGuid();
        var createdProduct = await productRepository.Create(productRecord);
        return productMapper.ToDTO(createdProduct);
    }

    public Task UpdateProduct(ProductDto productDto, Guid id)
    {
        productValidator.Validate(productDto);
        var productRecord = productMapper.ToRecord(productDto);
        productRecord.Id = id;
        return productRepository.Update(productRecord);
    }

    public Task DeleteProduct(Guid id)
    {
        return productRepository.Delete(id);
    }
}
