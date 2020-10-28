using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class ArticleRepository : GenericRepository<Article>, IArticleRepository
    {
        public ArticleRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }

        protected override Task<Article> GetEntityAsync(Article entity)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, IEnumerable<Article>>> GetEntitiesByNormalizedUserNameAsync(IEnumerable<string> normalizedUserNames)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, IEnumerable<Article>>> GetEntitiesByUnitIdAsync(IEnumerable<long> ids)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IDictionary<DateTime, Article>>> GetEntitiesByExpirationDateAsync(int amount)
        {
            throw new NotImplementedException();
        }
    }
}