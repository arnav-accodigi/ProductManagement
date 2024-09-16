// using System.ComponentModel.DataAnnotations;
using ProductManagement.Data.DTO;
// using ProductManagement.Data.Exceptions;
using ProductManagement.Data.Mappers;
using ProductManagement.Data.Repositories;
using ProductManagement.Data.Validation.Product;

namespace ProductManagement.Services.Services;

// public class ResponseDto
// {
//     public string Message { get; set; } = "";
//     public int StatusCode { get; set; }
//     public string Error { get; set; } = "";
//     public object Data { get; set; }
// }

// public class ResponseCreator<T>
// {
//     public static async Task<ResponseDto> ToResponse(Func<Task<T>> asyncFunction)
//     {
//         try
//         {
//             var response = await asyncFunction();
//             return new ResponseDto { StatusCode = 200, Data = response };
//         }
//         catch (Exception ex)
//         {
//             var statusCode = ex switch
//             {
//                 RecordNotFoundException => 404,
//                 ValidationException => 400,
//                 _ => 500
//             };
//             return new ResponseDto { Error = ex.Message, StatusCode = statusCode, };
//         }
//     }
// }

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
        return products.Select(ProductMapper.ToProductDTO);
    }

    public async Task<ProductDto> GetProductById(Guid id)
    {
        var product = await productRepository.GetById(id);
        return ProductMapper.ToProductDTO(product);
    }

    public async Task<ProductDto> CreateProduct(ProductDto productDto)
    {
        productValidator.Validate(productDto);
        var productRecord = ProductMapper.ToProductRecord(productDto, Guid.NewGuid());
        var createdProduct = await productRepository.Create(productRecord);
        return ProductMapper.ToProductDTO(createdProduct);
    }

    public Task UpdateProduct(ProductDto productDto, Guid id)
    {
        productValidator.Validate(productDto);
        var productRecord = ProductMapper.ToProductRecord(productDto, id);
        return productRepository.Update(productRecord);
    }

    public Task DeleteProduct(Guid id)
    {
        return productRepository.Delete(id);
    }

    // public Task<ResponseDto> GetProductById(Guid id)
    // {
    //     return ResponseCreator<ProductDto>.ToResponse(() => GetProductByIdTask(id));
    // }
}
