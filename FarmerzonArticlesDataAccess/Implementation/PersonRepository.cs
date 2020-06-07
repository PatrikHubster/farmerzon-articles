using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class PersonRepository : AbstractRepository<Person>, IPersonRepository
    {
        public PersonRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }
        
        public async Task<IList<Person>> GetEntitiesAsync(long? id, string userName, string normalizedUserName)
        {
            return await Context.People
                .Where(person => id == null || person.PersonId == id)
                .Where(p => userName == null || p.UserName == userName)
                .Where(p => normalizedUserName == null || p.NormalizedUserName == normalizedUserName)
                .ToListAsync();
        }

        public async Task<IList<Person>> GetEntitiesByIdAsync(IEnumerable<long> ids, IList<string> includes)
        {
            var query = Context.People.Where(p => ids.Contains(p.PersonId));
            query = AddIncludesToQuery(query, includes);
            return await query.ToListAsync();
        }
    }
}