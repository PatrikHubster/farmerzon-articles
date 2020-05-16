using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interfaces
{
    public interface IAddressRepository
    {
        public Task<IList<Address>> GetEntities(int? id, string doorNumber, string street, FarmerzonContext context);
    }
}