using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IPersonManager
    {
        public Task<IEnumerable<DTO.PersonOutput>> GetEntitiesAsync(long? id, string userName, string normalizedUserName);
        public Task<IDictionary<string, DTO.PersonOutput>> GetEntitiesByArticleIdAsync(IEnumerable<long> ids);
    }
}