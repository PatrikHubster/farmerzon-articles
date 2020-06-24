using System.Threading.Tasks;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface IAbstractRepository<T>
    {
        public Task<T> AddOrUpdateEntityAsync(T entity);
    }
}