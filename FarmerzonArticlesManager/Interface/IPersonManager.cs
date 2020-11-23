using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IPersonManager
    {
        public Task<IDictionary<string, DTO.PersonOutput>> GetEntitiesByArticleIdAsync(IEnumerable<long> ids);
    }
}