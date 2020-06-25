using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class ArticleRepository : AbstractRepository<Article>, IArticleRepository
    {
        public ArticleRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }

        public async Task<IList<Article>> GetEntitiesAsync(long? id, string name, string description, double? price, 
            int? amount, double? size, DateTime? createdAt, DateTime? updatedAt, DateTime? expirationDate)
        {
            return await Context.Articles
                .Where(a => id == null || a.ArticleId == id)
                .Where(a => name == null || a.Name == name)
                .Where(a => description == null || a.Description == description)
                .Where(a => price == null || a.Price == price)
                .Where(a => size == null || a.Size == size)
                .Where(a => createdAt == null || a.CreatedAt == createdAt)
                .Where(a => updatedAt == null || a.UpdatedAt == updatedAt)
                .Where(a => expirationDate == null || a.ExpirationDate == expirationDate)
                .ToListAsync();
        }

        public async Task<IList<Article>> GetEntitiesByIdAsync(IEnumerable<long> ids, IEnumerable<string> includes)
        {
            return await Context.Articles
                .IncludeMany(includes)
                .Where(a => ids.Contains(a.ArticleId))
                .ToListAsync();
        }

        public new Task<Article> AddOrUpdateEntityAsync(Article entity)
        {
            if (entity.Person != null && entity.Person.PersonId != 0)
            {
                Context.Attach(entity.Person);
            }

            if (entity.Unit != null && entity.Unit.UnitId != 0)
            {
                Context.Attach(entity.Unit);
            }

            return base.AddOrUpdateEntityAsync(entity);
        }

        public async Task<IList<Article>> GetArticlesByExpirationDate(int amount)
        {
            return await Context.Articles
                .Include(a => a.Unit)
                .OrderBy(a => a.ExpirationDate)
                .Take(amount)
                .ToListAsync();
        }
    }
}