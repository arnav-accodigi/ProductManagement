using ProductManagement.Data.Domain;
using ProductManagement.Data.Exceptions;

namespace ProductManagement.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<ProductRecord> products = [];

    public async Task<IEnumerable<ProductRecord>> GetAll()
    {
        return await Task.FromResult(products);
    }

    private ProductRecord? FindProduct(Guid id)
    {
        return products.Find(product => product.Id == id);
    }

    public async Task<ProductRecord> GetById(Guid id)
    {
        var product = FindProduct(id) ?? throw new RecordNotFoundException("Product not found");
        return await Task.FromResult(product);
    }

    public async Task<ProductRecord> Create(ProductRecord productRecord)
    {
        await Task.Run(() => products.Add(productRecord));
        return productRecord;
    }

    public async Task Update(ProductRecord productRecord)
    {
        await Task.Run(() =>
        {
            var product =
                FindProduct(productRecord.Id)
                ?? throw new RecordNotFoundException("Product not found");

            product = productRecord;
        });
    }

    public async Task Delete(Guid id)
    {
        await Task.Run(() =>
        {
            var removedItems = products.RemoveAll(product => product.Id == id);
            if (removedItems == 0)
                throw new RecordNotFoundException("Product not found");
        });
    }
}
