using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interfaces
{
    public interface IArticleRepository
    {
        public Task<IList<Article>> GetEntities(int? id, string name, string description, double? price, 
            int? amount, double? size, DateTime? createdAt, DateTime? updatedAt);
    }
}