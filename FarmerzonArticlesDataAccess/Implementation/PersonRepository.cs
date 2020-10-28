using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }

        protected override Task<Person> GetEntityAsync(Person entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDictionary<string, Person>> GetEntitiesByArticleIdAsync(IEnumerable<long> ids)
        {
            throw new System.NotImplementedException();
        }
    }
}