using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface IArticleRepository : IBasicRepository<Article>
    {
        public Task<IDictionary<string, IList<Article>>> GetEntitiesByNormalizedUserNameAsync(
            IEnumerable<string> normalizedUserNames, IEnumerable<string> includes = null);
        public Task<IDictionary<string, IList<Article>>> GetEntitiesByUnitIdAsync(IEnumerable<long> ids,
            IEnumerable<string> includes = null);
        public Task<IEnumerable<Article>> GetEntitiesByExpirationDateAsync(int amount, 
            IEnumerable<string> includes = null);
    }
}