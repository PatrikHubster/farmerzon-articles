using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IArticleManager
    {
        public Task<IList<DTO.Article>> GetEntitiesAsync(long? id, string name, string description, double? price, 
            int? amount, double? size, DateTime? createdAt, DateTime? updatedAt);
        public Task<IDictionary<string, IList<DTO.Article>>> GetArticlesByNormalizedUserNameAsync(
            IEnumerable<string> normalizedUserNames);
        public Task<IDictionary<string, IList<DTO.Article>>> GetArticlesByUnitIdAsync(IEnumerable<long> ids);
    }
}