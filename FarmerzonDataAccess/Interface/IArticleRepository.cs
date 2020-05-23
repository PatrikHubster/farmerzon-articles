using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interface
{
    public interface IArticleRepository
    {
        public Task<IList<Article>> GetEntitiesAsync(int? id, string name, string description, double? price, 
            int? amount, double? size, DateTime? createdAt, DateTime? updatedAt);
    }
}