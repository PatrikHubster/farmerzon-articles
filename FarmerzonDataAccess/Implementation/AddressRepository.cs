using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Interface;
using FarmerzonDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonDataAccess.Implementation
{
    public class AddressRepository : AbstractRepository, IAddressRepository
    {
        public AddressRepository(FarmerzonContext context) : base(context)
        {
            // nothing to do here
        }

        public async Task<IList<Address>> GetEntitiesAsync(int? id, string doorNumber, string street)
        {
            return await Context.Addresses
                .Include(a => a.City)
                .Include(a => a.State)
                .Include(a => a.Country)
                .Where(a => id == null || a.AddressId == id)
                .Where(a => doorNumber == null || a.DoorNumber == doorNumber)
                .Where(a => street == null || a.Street == street)
                .ToListAsync();
        }
    }
}