using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Interfaces;
using FarmerzonDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonDataAccess.Implementations
{
    public class CountryRepository : ICountryRepository
    {
        public async Task<IList<Country>> GetEntities(int? id, string name, string code, FarmerzonContext context)
        {
            return await context.Countries
                .Where(c => id == null || c.CountryId == id)
                .Where(c => name == null || c.Name == name )
                .Where(c => code == null || c.Code == code)
                .ToListAsync();
        }
    }
}