using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Interfaces;
using FarmerzonDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonDataAccess.Implementations
{
    public class StateRepository : IStateRepository
    {
        public async Task<IList<State>> GetEntities(int? id, string name, FarmerzonContext context)
        {
            return await context.States
                .Where(s => id == null || s.StateId == id)
                .Where(s => name == null || s.Name == name)
                .ToListAsync();
        }
    }
}