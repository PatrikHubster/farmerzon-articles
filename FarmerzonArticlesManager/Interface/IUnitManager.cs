using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IUnitManager : IBasicManager<DTO.UnitOutput, DTO.UnitInput>
    {
        public Task<IEnumerable<DTO.UnitOutput>> GetEntitiesAsync(long? id = null, string name = null);
        public Task<IDictionary<string, DTO.UnitOutput>> GetEntitiesByArticleIdAsync(IEnumerable<long> ids);
    }
}