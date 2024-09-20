using ProductManagement.Data.DTO;
using ProductManagement.Data.Repositories;
using ProductManagement.Data.Validation.Product;
using ProductManagement.Services.Extensions;

namespace ProductManagement.Services.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IProductValidator productValidator;

    public ProductService(IProductRepository productRepository, IProductValidator productValidator)
    {
        this.productRepository = productRepository;
        this.productValidator = productValidator;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        var products = await productRepository.GetAll();
        return products.Select(product => product.ToDto());
    }

    public async Task<ProductDto> GetProductById(Guid id)
    {
        var product = await productRepository.GetById(id);
        return product.ToDto();
    }

    public async Task<ProductDto> CreateProduct(ProductDto productDto)
    {
        productValidator.Validate(productDto);
        var productRecord = productDto.ToRecord();
        productRecord.Id = Guid.NewGuid();
        var createdProduct = await productRepository.Create(productRecord);
        return createdProduct.ToDto();
    }

    public Task UpdateProduct(ProductDto productDto, Guid id)
    {
        productValidator.Validate(productDto);
        var productRecord = productDto.ToRecord();
        productRecord.Id = id;
        return productRepository.Update(productRecord);
    }

    public Task DeleteProduct(Guid id)
    {
        return productRepository.Delete(id);
    }
}
