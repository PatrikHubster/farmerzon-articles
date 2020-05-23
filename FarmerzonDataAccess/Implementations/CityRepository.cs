using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Interfaces;
using FarmerzonDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonDataAccess.Implementations
{
    public class CityRepository : AbstractRepository, ICityRepository
    {
        public CityRepository(FarmerzonContext context) : base(context)
        {
            // nothing to do here
        }
        
        public async Task<IList<City>> FindEntities(int? id, string zipCode, string name)
        {
            return await Context.Cities
                .Where(c => id == null || c.CityId == id)
                .Where(c => zipCode == null || c.ZipCode == zipCode)
                .Where(c => name == null || c.Name == name)
                .ToListAsync();
        }
    }
}