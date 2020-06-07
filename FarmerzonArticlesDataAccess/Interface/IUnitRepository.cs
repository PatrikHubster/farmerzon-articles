using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface IUnitRepository
    {
        public Task<IList<Unit>> GetEntitiesAsync(long? id, string name);
        public Task<IList<Unit>> GetEntitiesByIdAsync(IEnumerable<long> ids, IList<string> includes);
        
        public Task<Unit> AddEntityAsync(Unit unit);
    }
}