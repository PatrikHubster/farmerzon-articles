using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IUnitManager
    {
        public Task<IList<DTO.Unit>> GetEntitiesAsync(long? id, string name);
        public Task<IDictionary<string, DTO.Unit>> GetUnitsByArticleIdAsync(IEnumerable<long> ids);
    }
}