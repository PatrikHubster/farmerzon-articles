using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }

        protected override async Task<Person> GetEntityAsync(Person entity)
        {
            return await Context.People
                .Where(p =>  p.NormalizedUserName == entity.NormalizedUserName)
                .FirstOrDefaultAsync();
        }

        public async Task<IDictionary<string, Person>> GetEntitiesByArticleIdAsync(IEnumerable<long> ids, 
            IEnumerable<string> includes)
        {
            return await Context.Articles
                .Where(a => ids.Contains(a.Id))
                .IncludeMany(includes, "Person")
                .ToDictionaryAsync(key => key.Id.ToString(),
                    value => value.Person);
        }
    }
}