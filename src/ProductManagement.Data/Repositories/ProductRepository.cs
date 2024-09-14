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

    public async Task<ProductRecord> GetById(Guid id)
    {
        var product =
            products.Find(product => product.Id == id)
            ?? throw new NotFoundException("Product not found");
        return await Task.FromResult(product);
    }

    public async Task Create(ProductRecord productRecord)
    {
        // TODO: Add validation for valid product
        await Task.Run(() => products.Add(productRecord));
    }

    public async Task Update(ProductRecord productRecord)
    {
        await Task.Run(() =>
        {
            int productIndex = products.FindIndex(product => product.Id == productRecord.Id);

            if (productIndex == -1)
                throw new NotFoundException("Product not found");

            products[productIndex] = productRecord;
        });
    }

    public async Task Delete(Guid id)
    {
        await Task.Run(() =>
        {
            var removedItems = products.RemoveAll(product => product.Id == id);
            if (removedItems == 0)
                throw new NotFoundException("Product not found");
        });
    }
}
