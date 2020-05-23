using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interface
{
    public interface ICountryRepository
    {
        public Task<IList<Country>> GetEntitiesAsync(int? id, string name, string code);
    }
}