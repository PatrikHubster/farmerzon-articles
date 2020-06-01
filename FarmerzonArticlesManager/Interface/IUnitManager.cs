using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IUnitManager
    {
        public Task<IList<DTO.Unit>> GetEntitiesAsync(int? id, string name);
        public Task<DTO.Unit> GetUnitByArticleAsync(DTO.Article article);
    }
}