using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface IPersonRepository : IAbstractRepository<Person>
    {
        public Task<IList<Person>> GetEntitiesAsync(long? id, string userName, string normalizedUserName);
        public Task<IList<Person>> GetEntitiesByNormalizedUserNameAsync(IEnumerable<string> normalizedUserNames,
            IEnumerable<string> includes);
        public Task<Person> GetOrAddEntityAsync(Person person);
    }
}