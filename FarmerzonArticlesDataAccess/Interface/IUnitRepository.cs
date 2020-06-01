using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Interface
{
    public interface IUnitRepository
    {
        public Task<IList<Unit>> GetEntitiesAsync(int? id, string name);
        public Task<Unit> AddEntityAsync(Unit unit);
        public Task<Unit> GetUnitByArticleAsync(Article article);
    }
}