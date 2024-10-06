using MongoDB.Driver;

namespace SampleMicroService.Product.Data.Interfaces
{
    public interface IProductContext
    {
        
        public Task<List<Entities.Product>> GetAsync();

        public Task<Entities.Product?> GetAsync(string id);

        public Task CreateAsync(Entities.Product newBook);

        public Task UpdateAsync(string id, Entities.Product updatedBook);

        public Task RemoveAsync(string id);
        IMongoCollection<Entities.Product> _productCollection { get; }
    }
}
