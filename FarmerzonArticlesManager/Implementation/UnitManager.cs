using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesManager.Interface;

using DAO = FarmerzonArticlesDataAccessModel;
using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Implementation
{
    public class UnitManager : AbstractManager, IUnitManager
    {
        private IArticleRepository ArticleRepository { get; set; }
        private IUnitRepository UnitRepository { get; set; }

        public UnitManager(IMapper mapper, IArticleRepository articleRepository,
            IUnitRepository unitRepository) : base(mapper)
        {
            ArticleRepository = articleRepository;
            UnitRepository = unitRepository;
        }

        public async Task<IList<DTO.UnitResponse>> GetEntitiesAsync(long? id, string name)
        {
            var units = await UnitRepository.GetEntitiesAsync(id, name);
            return Mapper.Map<IList<DTO.UnitResponse>>(units);
        }

        public async Task<IDictionary<string, DTO.UnitResponse>> GetUnitsByArticleIdAsync(IEnumerable<long> ids)
        {
            var articles = 
                await ArticleRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.Article.Unit)});
            return articles.ToDictionary(key => key.ArticleId.ToString(), 
                value => Mapper.Map<DTO.UnitResponse>(value.Unit));
        }
    }
}