using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interfaces
{
    public interface IUnitRepository
    {
        public Task<IList<Unit>> GetEntities(int? id, string name, FarmerzonContext context);
        public Task<Unit> AddEntity(Unit unit, FarmerzonContext context);
    }
}