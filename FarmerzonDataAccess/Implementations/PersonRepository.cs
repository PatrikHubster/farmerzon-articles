using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccess.Interfaces;
using FarmerzonDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonDataAccess.Implementations
{
    public class PersonRepository : IPersonRepository
    {
        public async Task<IList<Person>> GetEntities(int? id, string userName, 
            string normalizedUserName, FarmerzonContext context)
        {
            return await context.People
                .Where(person => id == null || person.PersonId == id)
                .Where(p => userName == null || p.UserName == userName)
                .Where(p => normalizedUserName == null || p.NormalizedUserName == normalizedUserName)
                .ToListAsync();
        }
    }
}