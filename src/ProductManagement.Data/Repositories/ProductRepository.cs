using ProductManagement.Data.Domain;
using ProductManagement.Data.Exceptions;

namespace ProductManagement.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<ProductRecord> products = [];

    public Task<IEnumerable<ProductRecord>> GetAll()
    {
        return Task.FromResult<IEnumerable<ProductRecord>>(products);
    }

    private ProductRecord? FindProduct(Guid id)
    {
        return products.Find(product => product.Id == id);
    }

    public Task<ProductRecord> GetById(Guid id)
    {
        var product = FindProduct(id) ?? throw new RecordNotFoundException("Product not found");
        return Task.FromResult(product);
    }

    public Task<ProductRecord> Create(ProductRecord productRecord)
    {
        products.Add(productRecord);
        return Task.FromResult(productRecord);
    }

    public Task Update(ProductRecord productRecord)
    {
        var product =
            FindProduct(productRecord.Id) ?? throw new RecordNotFoundException("Product not found");

        product.Id = productRecord.Id;
        product.Name = productRecord.Name;
        product.Price = productRecord.Price;
        product.StockQuantity = productRecord.StockQuantity;

        return Task.CompletedTask;
    }

    public Task Delete(Guid id)
    {
        if (FindProduct(id) == null)
            throw new RecordNotFoundException("Product not found");
        products.RemoveAll(product => product.Id == id);
        return Task.CompletedTask;
    }
}
