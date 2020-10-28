using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface IArticleRepository : IBasicRepository<Article>
    {
        public Task<IDictionary<string, IEnumerable<Article>>> GetEntitiesByNormalizedUserNameAsync(
            IEnumerable<string> normalizedUserNames);
        public Task<IDictionary<string, IEnumerable<Article>>> GetEntitiesByUnitIdAsync(IEnumerable<long> ids);
        public Task<IEnumerable<IDictionary<DateTime, Article>>> GetEntitiesByExpirationDateAsync(int amount);
    }
}