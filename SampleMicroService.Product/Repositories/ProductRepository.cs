using System.Linq.Expressions;
using SampleMicroService.Product.Data.Interfaces;
using SampleMicroService.Product.Repositories.Interfaces;
using MongoDB.Driver;

namespace SampleMicroService.Product.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly IProductContext _context;

        public ProductRepository(IProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entities.Product>> GetProducts()
        {
            return await _context._productCollection.Find(x=>true).ToListAsync();
        }

        public async Task<Entities.Product> GetProductById(string id)
        {
            var val=await _context._productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return val;

        }

        public async Task<IEnumerable<Entities.Product>> GetProductByPredicate(Expression<Func<Entities.Product, bool>> predicate)
        {
            var val= await _context._productCollection.FindAsync(predicate);
            return await val.ToListAsync();
        }

        public async Task Create(Entities.Product entity)
        {
            await _context._productCollection.InsertOneAsync(entity);
        }

        public async Task<bool> Update(Entities.Product entity)
        {
            var updateResult=await _context._productCollection.ReplaceOneAsync(x=>x.Id==entity.Id,entity);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount>0;

        }

        public async Task<bool> Delete(string id)
        {
            var filter=Builders<Entities.Product>.Filter.Eq(m=>m.Id,id);
            DeleteResult deleteResult=await _context._productCollection.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }
    }
}
