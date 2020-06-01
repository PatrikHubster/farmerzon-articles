using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface IArticleRepository
    {
        public Task<IList<Article>> GetEntitiesAsync(int? id, string name, string description, double? price, 
            int? amount, double? size, DateTime? createdAt, DateTime? updatedAt);
        public Task<IList<Article>> GetArticleByUnitAsync(Unit unit);
        public Task<IList<Article>> GetArticlesByPersonAsync(Person person);
    }
}