using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class UnitRepository : AbstractRepository, IUnitRepository
    {
        public UnitRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }
        
        public async Task<IList<Unit>> GetEntitiesAsync(int? id, string name)
        {
            return await Context.Units
                .Where(unit => id == null || unit.UnitId == id)
                .Where(unit => name == null || unit.Name == name)
                .ToListAsync();
        }

        public async Task<Unit> AddEntityAsync(Unit unit)
        {
            var result = await Context.AddAsync(unit);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Unit> GetUnitByArticleAsync(Article article)
        {
            var managedArticle = await Context.Articles
                .Include(a => a.Unit)
                .Where(a => a.ArticleId == article.ArticleId)
                .FirstOrDefaultAsync();
            return managedArticle?.Unit;
        }
    }
}