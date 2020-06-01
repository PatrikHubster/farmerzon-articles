using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IArticleManager
    {
        public Task<IList<DTO.Article>> GetEntitiesAsync(int? id, string name, string description, double? price, 
            int? amount, double? size, DateTime? createdAt, DateTime? updatedAt);
        public Task<IList<DTO.Article>> GetArticlesByUnitAsync(DTO.Unit unit);
        public Task<IList<DTO.Article>> GetArticlesByPersonAsync(DTO.Person person);
    }
}