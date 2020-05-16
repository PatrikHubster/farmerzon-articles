using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Interfaces;
using FarmerzonDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonDataAccess.Implementations
{
    public class UnitRepository : IUnitRepository
    {
        public async Task<IList<Unit>> GetEntities(int? id, string name, FarmerzonContext context)
        {
            return await context.Units
                .Where(unit => id == null || unit.UnitId == id)
                .Where(unit => name == null || unit.Name == name)
                .ToListAsync();
        }

        public async Task<Unit> AddEntity(Unit unit, FarmerzonContext context)
        {
            var result = await context.AddAsync(unit);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}