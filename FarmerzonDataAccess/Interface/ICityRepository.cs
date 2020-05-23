using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interface
{
    public interface ICityRepository
    {
        public Task<IList<City>> FindEntitiesAsync(int? id, string zipCode, string name);
    }
}