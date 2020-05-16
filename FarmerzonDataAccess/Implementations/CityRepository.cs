using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Interfaces;
using FarmerzonDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonDataAccess.Implementations
{
    public class CityRepository : ICityRepository
    {
        public async Task<IList<City>> FindEntities(int? id, string zipCode, string name, FarmerzonContext context)
        {
            return await context.Cities
                .Where(c => id == null || c.CityId == id)
                .Where(c => zipCode == null || c.ZipCode == zipCode)
                .Where(c => name == null || c.Name == name)
                .ToListAsync();
        }
    }
}