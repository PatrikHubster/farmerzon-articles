using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        public UnitRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }

        protected override async Task<Unit> GetEntityAsync(Unit entity)
        {
            return await Context.Units
                .Where(u =>  u.Name == entity.Name)
                .FirstOrDefaultAsync();
        }

        public async Task<IDictionary<string, Unit>> GetEntitiesByArticleIdAsync(IEnumerable<long> ids, 
            IEnumerable<string> includes = null)
        {
            return await Context.Articles
                .Where(a => ids.Contains(a.Id))
                .IncludeMany(includes)
                .ToDictionaryAsync(key => key.Id.ToString(),
                    value => value.Unit);
        }
    }
}