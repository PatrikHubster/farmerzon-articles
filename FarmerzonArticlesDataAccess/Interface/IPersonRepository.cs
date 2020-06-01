using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface IPersonRepository
    {
        public Task<IList<Person>> GetEntitiesAsync(int? id, string userName, string normalizedUserName);
        public Task<Person> GetPersonByArticleAsync(Article article);
    }
}