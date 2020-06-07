using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface IPersonRepository
    {
        public Task<IList<Person>> GetEntitiesAsync(long? id, string userName, string normalizedUserName);
        public Task<IList<Person>> GetEntitiesByIdAsync(IEnumerable<long> ids, IList<string> includes);
    }
}