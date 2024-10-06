using SampleMicroService.Product.Settings;
using MongoDB.Driver;

namespace SampleMicroService.Product.Data.Interfaces;

public class ProductContext : IProductContext
{
    public ProductContext( IMongoCollection<Entities.Product> productCollection) //IProductDatabaseSettings settings,
    {
        _productCollection = productCollection;
        //var client = new MongoClient(settings.ConnectionString);
        //var database = client.GetDatabase(settings.DatabaseName);
        //_productCollection = database.GetCollection<Entities.Product>(
        //    settings.CollectionName);
        ProductContextSeed.Data(_productCollection);
    }

    public async Task<List<Entities.Product>> GetAsync()
    {
        return await _productCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Entities.Product?> GetAsync(string id)
    {
        return await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Entities.Product newBook)
    {
        await _productCollection.InsertOneAsync(newBook);
    }

    public async Task UpdateAsync(string id, Entities.Product updatedBook)
    {
        await _productCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);
    }

    public async Task RemoveAsync(string id)
    {
        await _productCollection.DeleteOneAsync(x => x.Id == id);
    }

    public IMongoCollection<Entities.Product> _productCollection { get; }
}