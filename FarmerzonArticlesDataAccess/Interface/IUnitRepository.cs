using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface IUnitRepository : IAbstractRepository<Unit>
    {
        public Task<IList<Unit>> GetEntitiesAsync(long? id, string name);
        public Task<IList<Unit>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes);
        public Task<Unit> GetOrAddEntityAsync(Unit unit);
    }
}