using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interface
{
    public interface IUnitRepository
    {
        public Task<IList<Unit>> GetEntities(int? id, string name);
        public Task<Unit> AddEntity(Unit unit);
    }
}