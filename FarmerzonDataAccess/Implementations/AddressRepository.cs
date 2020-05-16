using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Interfaces;
using FarmerzonDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonDataAccess.Implementations
{
    public class AddressRepository : IAddressRepository
    {
        public async Task<IList<Address>> GetEntities(int? id, string doorNumber, string street, 
            FarmerzonContext context)
        {
            return await context.Addresses
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