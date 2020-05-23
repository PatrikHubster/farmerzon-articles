using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Interface;
using FarmerzonDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonDataAccess.Implementation
{
    public class UnitRepository : AbstractRepository, IUnitRepository
    {
        public UnitRepository(FarmerzonContext context) : base(context)
        {
            // nothing to do here
        }
        
        public async Task<IList<Unit>> GetEntities(int? id, string name)
        {
            return await Context.Units
                .Where(unit => id == null || unit.UnitId == id)
                .Where(unit => name == null || unit.Name == name)
                .ToListAsync();
        }

        public async Task<Unit> AddEntity(Unit unit)
        {
            var result = await Context.AddAsync(unit);
            await Context.SaveChangesAsync();
            return result.Entity;
        }
    }
}