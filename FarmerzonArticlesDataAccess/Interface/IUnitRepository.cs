using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface IUnitRepository : IBasicRepository<Unit>
    {
        public Task<IDictionary<string, Unit>> GetEntitiesByArticleIdAsync(IEnumerable<long> ids,
            IEnumerable<string> includes = null);
    }
}