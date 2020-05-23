using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interface
{
    public interface IStateRepository
    {
        public Task<IList<State>> GetEntitiesAsync(int? id, string name);
    }
}