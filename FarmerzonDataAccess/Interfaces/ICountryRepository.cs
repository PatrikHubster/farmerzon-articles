using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interfaces
{
    public interface ICountryRepository
    {
        public Task<IList<Country>> GetEntities(int? id, string name, string code, FarmerzonContext context);
    }
}