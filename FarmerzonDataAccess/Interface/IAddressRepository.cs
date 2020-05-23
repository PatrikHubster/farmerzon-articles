using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interface
{
    public interface IAddressRepository
    {
        public Task<IList<Address>> GetEntitiesAsync(int? id, string doorNumber, string street);
    }
}