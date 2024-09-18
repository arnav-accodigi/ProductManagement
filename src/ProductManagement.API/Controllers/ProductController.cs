using Microsoft.AspNetCore.Mvc;
using ProductManagement.Data.DTO;
using ProductManagement.Services.Services;

namespace ProductManagement.API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController : BaseController
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        return await HandleAsync(async () =>
        {
            var products = await productService.GetAllProducts();
            return Ok(products);
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        return await HandleAsync(async () =>
        {
            var product = await productService.GetProductById(id);
            return Ok(product);
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
    {
        return await HandleAsync(async () =>
        {
            var product = await productService.CreateProduct(productDto);
            return CreatedAtAction(nameof(GetProduct), new { Id = product.Id }, product);
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto, Guid id)
    {
        return await HandleAsync(async () =>
        {
            await productService.UpdateProduct(productDto, id);
            return Ok();
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        return await HandleAsync(async () =>
        {
            await productService.DeleteProduct(id);
            return Ok();
        });
    }
}
