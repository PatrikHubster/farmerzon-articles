using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IPersonManager
    {
        public Task<IList<DTO.Person>> GetEntitiesAsync(long? id, string userName, string normalizedUserName);
        public Task<IDictionary<long, DTO.Person>> GetPeopleByArticleIdAsync(IEnumerable<long> ids);
    }
}