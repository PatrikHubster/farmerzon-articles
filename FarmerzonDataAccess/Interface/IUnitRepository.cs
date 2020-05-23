using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interface
{
    public interface IUnitRepository
    {
        public Task<IList<Unit>> GetEntitiesAsync(int? id, string name);
        public Task<Unit> AddEntityAsync(Unit unit);
    }
}