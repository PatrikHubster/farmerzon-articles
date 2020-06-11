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

        public async Task<IDictionary<string, IList<DTO.Article>>> GetArticlesByPersonIdAsync(IEnumerable<long> ids)
        {
            var people =
                await PersonRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.Person.Articles)});

            var articlesForPeople = new Dictionary<string, IList<DTO.Article>>();
            foreach (var person in people)
            {
                if (!articlesForPeople.ContainsKey(person.PersonId.ToString()) && person.Articles.Count > 0)
                {
                    articlesForPeople.Add(person.PersonId.ToString(), new List<DTO.Article>());
                    foreach (var article in person.Articles)
                    {
                        articlesForPeople[person.PersonId.ToString()].Add(Mapper.Map<DTO.Article>(article));
                    }
                }
            }

            return articlesForPeople;
        }
        
        public async Task<IDictionary<string, IList<DTO.Article>>> GetArticlesByUnitIdAsync(IEnumerable<long> ids)
        {
            var units =
                await UnitRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.Unit.Articles)});

            var articlesForUnits = new Dictionary<string, IList<DTO.Article>>();
            foreach (var unit in units)
            {
                if (!articlesForUnits.ContainsKey(unit.UnitId.ToString()) && unit.Articles.Count > 0)
                {
                    articlesForUnits.Add(unit.UnitId.ToString(), new List<DTO.Article>());
                    foreach (var article in unit.Articles)
                    {
                        articlesForUnits[unit.UnitId.ToString()].Add(Mapper.Map<DTO.Article>(article));
                    }
                }
            }

            return articlesForUnits;
        }
    }
}