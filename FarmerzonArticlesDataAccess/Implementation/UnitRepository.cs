using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class UnitRepository : AbstractRepository, IUnitRepository
    {
        public UnitRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }
        
        public async Task<IList<Unit>> GetEntitiesAsync(long? id, string name)
        {
            return await Context.Units
                .Where(u => id == null || u.UnitId == id)
                .Where(u => name == null || u.Name == name)
                .ToListAsync();
        }

        public async Task<IList<Unit>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes)
        {
            return await Context.Units
                .IncludeMany(includes)
                .Where(u => ids.Contains(u.UnitId))
                .ToListAsync();
        }
    }
}