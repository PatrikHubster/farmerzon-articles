using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IUnitManager
    {
        public Task<IList<DTO.UnitResponse>> GetEntitiesAsync(long? id, string name);
        public Task<IDictionary<string, DTO.UnitResponse>> GetUnitsByArticleIdAsync(IEnumerable<long> ids);
    }
}