using System.Linq.Expressions;

namespace SampleMicroService.Product.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Entities.Product>> GetProducts();
        Task<Entities.Product> GetProductById(string id);
        Task<IEnumerable<Entities.Product>> GetProductByPredicate(Expression<Func<Entities.Product,bool>> predicate);
        Task Create(Entities.Product entity);
        Task<bool> Update(Entities.Product entity);
        Task<bool> Delete(string id);
        

    }
}
