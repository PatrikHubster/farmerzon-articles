using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class PersonRepository : AbstractRepository, IPersonRepository
    {
        public PersonRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }

        public async Task<IList<Person>> GetEntitiesAsync(long? id, string userName, string normalizedUserName)
        {
            return await Context.People
                .Where(p => id == null || p.PersonId == id)
                .Where(p => userName == null || p.UserName == userName)
                .Where(p => normalizedUserName == null || p.NormalizedUserName == normalizedUserName)
                .ToListAsync();
        }

        public async Task<IList<Person>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes)
        {
            return await Context.People
                .IncludeMany(includes)
                .Where(p => ids.Contains(p.PersonId))
                .ToListAsync();
        }

        public async Task<IList<Person>> GetEntitiesByNormalizedUserNameAsync(IEnumerable<string> normalizedUserNames, IEnumerable<string> includes)
        {
            return await Context.People
                .IncludeMany(includes)
                .Where(p => normalizedUserNames.Contains(p.NormalizedUserName))
                .ToListAsync();
        }
    }
}