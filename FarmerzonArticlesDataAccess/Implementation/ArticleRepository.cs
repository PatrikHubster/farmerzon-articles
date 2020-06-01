using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class ArticleRepository : AbstractRepository, IArticleRepository
    {
        public ArticleRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }
        
        public async Task<IList<Article>> GetEntitiesAsync(int? id, string name, string description, double? price, 
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

        public async Task<IList<Article>> GetArticleByUnitAsync(Unit unit)
        {
            var managedUnit = await Context.Units
                .Where(u => u.UnitId == unit.UnitId)
                .FirstOrDefaultAsync();
            return managedUnit == null ? new List<Article>() : managedUnit.Articles;
        }

        public async Task<IList<Article>> GetArticlesByPersonAsync(Person person)
        {
            var managedPerson = await Context.People
                .Where(p => p.PersonId == person.PersonId)
                .FirstOrDefaultAsync();
            return managedPerson == null ? new List<Article>() : managedPerson.Articles;
        }
    }
}