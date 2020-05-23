using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Interface;
using FarmerzonDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonDataAccess.Implementation
{
    public class CountryRepository : AbstractRepository, ICountryRepository
    {
        public CountryRepository(FarmerzonContext context) : base(context)
        {
            // nothing to do here
        }
        
        public async Task<IList<Country>> GetEntities(int? id, string name, string code)
        {
            return await Context.Countries
                .Where(c => id == null || c.CountryId == id)
                .Where(c => name == null || c.Name == name )
                .Where(c => code == null || c.Code == code)
                .ToListAsync();
        }
    }
}