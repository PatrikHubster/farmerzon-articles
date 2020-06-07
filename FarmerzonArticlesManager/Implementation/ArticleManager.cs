using System;
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
    public class ArticleManager : AbstractManager, IArticleManager
    {
        private IArticleRepository ArticleRepository { get; set; }
        private IPersonRepository PersonRepository { get; set; }
        private IUnitRepository UnitRepository { get; set; }

        public ArticleManager(IMapper mapper, IArticleRepository articleRepository, 
            IPersonRepository personRepository, IUnitRepository unitRepository) : base(mapper)
        {
            ArticleRepository = articleRepository;
            PersonRepository = personRepository;
            UnitRepository = unitRepository;
        }
        
        public async Task<IList<DTO.Article>> GetEntitiesAsync(long? id, string name, string description, double? price, 
            int? amount, double? size, DateTime? createdAt, DateTime? updatedAt)
        {
            var articles = await ArticleRepository.GetEntitiesAsync(id, name, description, price,
                amount, size, createdAt, updatedAt);
            return Mapper.Map<IList<DTO.Article>>(articles);
        }

        public async Task<ILookup<long, DTO.Article>> GetArticlesByPersonIdAsync(IEnumerable<long> ids)
        {
            var people = 
                await PersonRepository.GetEntitiesByIdAsync(ids, 
                    new List<string> {nameof(DAO.Person.Articles)});
            return people
                .SelectMany(p => p.Articles.Select(a => new {Key = p.PersonId, Value = Mapper.Map<DTO.Article>(a)}))
                .ToLookup(pair => pair.Key, pair => pair.Value);
        }

        public async Task<ILookup<long, DTO.Article>> GetArticlesByUnitIdAsync(IEnumerable<long> ids)
        {
            var units = 
                await UnitRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.Unit.Articles)});
            return units
                .SelectMany(u => u.Articles.Select(a => new {Key = u.UnitId, Value = Mapper.Map<DTO.Article>(a)}))
                .ToLookup(pair => pair.Key, pair => pair.Value);
        }
    }
}