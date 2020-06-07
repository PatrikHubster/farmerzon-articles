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
            int? amount, double? size, DateTime? createdAt, DateTime? updatedAt)
        {
            return await Context.Articles
                .Where(a => id == null || a.ArticleId == id)
                .Where(a => name == null || a.Name == name)
                .Where(a => description == null || a.Description == description)
                .Where(a => price == null || a.Price == price)
                .Where(a => size == null || a.Size == size)
                .Where(a => createdAt == null || a.CreatedAt == createdAt)
                .Where(a => updatedAt == null || a.UpdatedAt == updatedAt)
                .ToListAsync();
        }

        public async Task<IList<Article>> GetEntitiesByIdAsync(IEnumerable<long> ids, IList<string> includes)
        {
            var query = Context.Articles.Where(a => ids.Contains(a.ArticleId));
            query = AddIncludesToQuery(query, includes);
            return await query.ToListAsync();
        }
    }
}