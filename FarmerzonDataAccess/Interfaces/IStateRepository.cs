using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interfaces
{
    public interface IStateRepository
    {
        public Task<IList<State>> GetEntities(int? id, string name, FarmerzonContext context);
    }
}