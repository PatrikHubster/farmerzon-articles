using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;
using Microsoft.EntityFrameworkCore;

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
            return Task.FromResult<Article>(null);
        }

        public async Task<IDictionary<string, IList<Article>>> GetEntitiesByNormalizedUserNameAsync(
            IEnumerable<string> normalizedUserNames, IEnumerable<string> includes = null)
        {
            return await Context.People
                .Where(p => normalizedUserNames.Contains(p.NormalizedUserName))
                .IncludeMany(includes, "Articles")
                .ToDictionaryAsync(key => key.NormalizedUserName,
                    value => value.Articles);
        }

        public async Task<IDictionary<string, IList<Article>>> GetEntitiesByUnitIdAsync(IEnumerable<long> ids, 
            IEnumerable<string> includes)
        {
            return await Context.Units
                .Where(u => ids.Contains(u.Id))
                .IncludeMany(includes, "Articles")
                .ToDictionaryAsync(key => key.Id.ToString(),
                    value => value.Articles);
        }

        public async Task<IEnumerable<Article>> GetEntitiesByExpirationDateAsync(int amount, 
            IEnumerable<string> includes)
        {
            return await Context.Articles
                .IncludeMany(includes)
                .OrderBy(a => a.ExpirationDate)
                .Take(amount)
                .ToListAsync();
        }
    }
}