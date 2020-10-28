using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface IPersonRepository : IBasicRepository<Person>
    {
        public Task<IDictionary<string, Person>> GetEntitiesByArticleIdAsync(IEnumerable<long> ids);
    }
}