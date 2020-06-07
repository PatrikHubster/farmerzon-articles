using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class UnitRepository : AbstractRepository<Unit>, IUnitRepository
    {
        public UnitRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }
        
        public async Task<IList<Unit>> GetEntitiesAsync(long? id, string name)
        {
            return await Context.Units
                .Where(unit => id == null || unit.UnitId == id)
                .Where(unit => name == null || unit.Name == name)
                .ToListAsync();
        }

        public async Task<IList<Unit>> GetEntitiesByIdAsync(IEnumerable<long> ids, IList<string> includes)
        {
            var query = Context.Units.Where(u => ids.Contains(u.UnitId));
            query = AddIncludesToQuery(query, includes);
            return await query.ToListAsync();
        }

        public async Task<Unit> AddEntityAsync(Unit unit)
        {
            var result = await Context.AddAsync(unit);
            await Context.SaveChangesAsync();
            return result.Entity;
        }
    }
}