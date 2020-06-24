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
                .Where(p => id == null || p.PersonId == id)
                .Where(p => userName == null || p.UserName == userName)
                .Where(p => normalizedUserName == null || p.NormalizedUserName == normalizedUserName)
                .ToListAsync();
        }

        public async Task<IList<Person>> GetEntitiesByNormalizedUserNameAsync(IEnumerable<string> normalizedUserNames, 
            IEnumerable<string> includes)
        {
            return await Context.People
                .IncludeMany(includes)
                .Where(p => normalizedUserNames.Contains(p.NormalizedUserName))
                .ToListAsync();
        }

        public async Task<Person> GetOrAddEntityAsync(Person person)
        {
            var managedPerson = (await GetEntitiesAsync(null, person.UserName, person.NormalizedUserName))
                .FirstOrDefault();
            return managedPerson ?? await AddOrUpdateEntityAsync(person);
        }
    }
}