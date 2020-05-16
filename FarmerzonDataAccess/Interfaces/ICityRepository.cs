using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interfaces
{
    public interface ICityRepository
    {
        public Task<IList<City>> FindEntities(int? id, string zipCode, string name, FarmerzonContext context);
    }
}