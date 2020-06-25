using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IArticleManager
    {
        public Task<IList<DTO.ArticleResponse>> GetEntitiesAsync(long? id, string name, string description, double? price, 
            int? amount, double? size, DateTime? createdAt, DateTime? updatedAt, DateTime? expirationDate);
        public Task<IDictionary<string, IList<DTO.ArticleResponse>>> GetArticlesByNormalizedUserNameAsync(
            IEnumerable<string> normalizedUserNames);
        public Task<IDictionary<string, IList<DTO.ArticleResponse>>> GetArticlesByUnitIdAsync(IEnumerable<long> ids);
        public Task<DTO.ArticleResponse> AddArticle(DTO.ArticleInput articleInput, string normalizedUserName,
            string userName);
        public Task<IList<DTO.ArticleResponse>> GetArticlesByExpirationDate(int amount);
    }
}