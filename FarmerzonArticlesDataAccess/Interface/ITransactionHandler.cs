using System.Threading.Tasks;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface ITransactionHandler
    {
        public Task BeginTransactionAsync();
        public Task CommitTransactionAsync();
        public Task RollbackTransactionAsync();
        public Task DisposeTransactionAsync();
    }
}