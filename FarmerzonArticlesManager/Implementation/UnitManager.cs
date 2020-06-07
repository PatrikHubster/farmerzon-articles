using System.Collections.Generic;
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

        public async Task<IList<DTO.Unit>> GetEntitiesAsync(long? id, string name)
        {
            var units = await UnitRepository.GetEntitiesAsync(id, name);
            return Mapper.Map<IList<DTO.Unit>>(units);
        }

        public async Task<IDictionary<long, DTO.Unit>> GetUnitsByArticleIdAsync(IEnumerable<long> ids)
        {
            var articles = 
                await ArticleRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.Article.Unit)});
            var unitForArticle = new Dictionary<long, DTO.Unit>();
            var units = new Dictionary<long, DTO.Unit>();
            foreach (var article in articles)
            {
                if (unitForArticle.ContainsKey(article.ArticleId)) continue;
                
                // If you save the already converted units in an own dictionary you save the conversion if the
                // unit occurs 1000 times. If this is not necessary you have lost a single assignment.
                if (!units.ContainsKey(article.Unit.UnitId))
                {
                    units.Add(article.Unit.UnitId, Mapper.Map<DTO.Unit>(article.Unit));
                }
                unitForArticle.Add(article.ArticleId, units[article.Unit.UnitId]);
            }

            return unitForArticle;
        }
    }
}