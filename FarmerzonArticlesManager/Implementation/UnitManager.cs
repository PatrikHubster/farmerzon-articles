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
        private IUnitRepository UnitRepository { get; set; }

        public UnitManager(IMapper mapper, IUnitRepository unitRepository) : base(mapper)
        {
            UnitRepository = unitRepository;
        }
        
        public async Task<IList<DTO.Unit>> GetEntitiesAsync(int? id, string name)
        {
            var units = await UnitRepository.GetEntitiesAsync(id, name);
            return Mapper.Map<IList<DTO.Unit>>(units);
        }

        public async Task<DTO.Unit> GetUnitByArticleAsync(DTO.Article article)
        {
            var convertedArticle = Mapper.Map<DAO.Article>(article);
            var unit = await UnitRepository.GetUnitByArticleAsync(convertedArticle);
            return Mapper.Map<DTO.Unit>(unit);
        }
    }
}