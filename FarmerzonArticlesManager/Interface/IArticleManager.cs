using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IArticleManager
    {
        public Task<DTO.ArticleOutput> InsertEntityAsync(DTO.ArticleInput entity, string userName,
            string normalizedUserName);
        public Task<IEnumerable<DTO.ArticleOutput>> GetEntitiesAsync(long? id = null, string name = null, 
            string description = null, double? price = null, int? amount = null, double? size = null, 
            DateTime? createdAt = null, DateTime? updatedAt = null, DateTime? expirationDate = null);
        public Task<IDictionary<string, IEnumerable<DTO.ArticleOutput>>> GetEntitiesByNormalizedUserNameAsync(
            IEnumerable<string> normalizedUserNames);
        public Task<IDictionary<string, IEnumerable<DTO.ArticleOutput>>> GetEntitiesByUnitIdAsync(IEnumerable<long> ids);
        public Task<IEnumerable<DTO.ArticleOutput>> GetEntitiesByExpirationDateAsync(int amount);
        public Task<DTO.ArticleOutput> UpdateEntityAsync(long id, DTO.ArticleInput entity, string userName, 
            string normalizedUserName);
        public Task<DTO.ArticleOutput> RemoveEntityByIdAsync(long id, string userName, string normalizedUserName);
        public Task<IEnumerable<DTO.ArticleOutput>> GetEntitiesByIdAsync(IEnumerable<long> ids);
        public Task<DTO.ArticleOutput> GetEntityByIdAsync(long id);
    }
}