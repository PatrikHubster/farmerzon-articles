using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IPersonManager
    {
        public Task<IList<DTO.PersonResponse>> GetEntitiesAsync(long? id, string userName, string normalizedUserName);
        public Task<IDictionary<string, DTO.PersonResponse>> GetPeopleByArticleIdAsync(IEnumerable<long> ids);
    }
}