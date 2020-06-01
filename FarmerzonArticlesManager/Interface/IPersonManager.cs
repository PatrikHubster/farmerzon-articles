using System.Collections.Generic;
using System.Threading.Tasks;

using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Interface
{
    public interface IPersonManager
    {
        public Task<IList<DTO.Person>> GetEntitiesAsync(int? id, string userName, string normalizedUserName);
        public Task<DTO.Person> GetPersonByArticleAsync(DTO.Article article);
    }
}