using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesManager.Interface;

using DAO = FarmerzonArticlesDataAccessModel;
using DTO = FarmerzonArticlesDataTransferModel;

namespace FarmerzonArticlesManager.Implementation
{
    public class ArticleManager : AbstractManager, IArticleManager
    {
        private IArticleRepository ArticleRepository { get; set; }

        public ArticleManager(IMapper mapper, IArticleRepository articleRepository) : base(mapper)
        {
            ArticleRepository = articleRepository;
        }
        
        public async Task<IList<DTO.Article>> GetEntitiesAsync(int? id, string name, string description, double? price, 
            int? amount, double? size, DateTime? createdAt, DateTime? updatedAt)
        {
            var articles = await ArticleRepository.GetEntitiesAsync(id, name, description, price,
                amount, size, createdAt, updatedAt);
            return Mapper.Map<IList<DTO.Article>>(articles);
        }

        public async Task<IList<DTO.Article>> GetArticlesByUnitAsync(DTO.Unit unit)
        {
            var convertedUnit = Mapper.Map<DAO.Unit>(unit);
            var articles = await ArticleRepository.GetArticleByUnitAsync(convertedUnit);
            return Mapper.Map<IList<DTO.Article>>(articles);
        }

        public async Task<IList<DTO.Article>> GetArticlesByPersonAsync(DTO.Person person)
        {
            var convertedPerson = Mapper.Map<DAO.Person>(person);
            var articles = await ArticleRepository.GetArticlesByPersonAsync(convertedPerson);
            return Mapper.Map<IList<DTO.Article>>(articles);
        }
    }
}