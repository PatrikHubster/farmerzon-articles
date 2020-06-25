using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesErrorHandling.CustomException;
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

        public async Task<IList<DTO.ArticleResponse>> GetEntitiesAsync(long? id, string name, string description,
            double? price, int? amount, double? size, DateTime? createdAt, DateTime? updatedAt, DateTime? expirationDate)
        {
            var articles = await ArticleRepository.GetEntitiesAsync(id, name, description, price,
                amount, size, createdAt, updatedAt, expirationDate);
            return Mapper.Map<IList<DTO.ArticleResponse>>(articles);
        }

        public async Task<IDictionary<string, IList<DTO.ArticleResponse>>> GetArticlesByNormalizedUserNameAsync(
            IEnumerable<string> normalizedUserNames)
        {
            var people = await PersonRepository.GetEntitiesByNormalizedUserNameAsync(normalizedUserNames,
                new List<string> {nameof(DAO.Person.Articles)});

            var articlesForPeople = new Dictionary<string, IList<DTO.ArticleResponse>>();
            foreach (var person in people)
            {
                if (!articlesForPeople.ContainsKey(person.NormalizedUserName) && person.Articles.Count > 0)
                {
                    articlesForPeople.Add(person.NormalizedUserName, new List<DTO.ArticleResponse>());
                    foreach (var article in person.Articles)
                    {
                        articlesForPeople[person.NormalizedUserName].Add(Mapper.Map<DTO.ArticleResponse>(article));
                    }
                }
            }

            return articlesForPeople;
        }

        public async Task<IDictionary<string, IList<DTO.ArticleResponse>>> GetArticlesByUnitIdAsync(
            IEnumerable<long> ids)
        {
            var units =
                await UnitRepository.GetEntitiesByIdAsync(ids, new List<string> {nameof(DAO.Unit.Articles)});

            var articlesForUnits = new Dictionary<string, IList<DTO.ArticleResponse>>();
            foreach (var unit in units)
            {
                if (!articlesForUnits.ContainsKey(unit.UnitId.ToString()) && unit.Articles.Count > 0)
                {
                    articlesForUnits.Add(unit.UnitId.ToString(), new List<DTO.ArticleResponse>());
                    foreach (var article in unit.Articles)
                    {
                        articlesForUnits[unit.UnitId.ToString()].Add(Mapper.Map<DTO.ArticleResponse>(article));
                    }
                }
            }

            return articlesForUnits;
        }

        public async Task<DTO.ArticleResponse> AddArticle(DTO.ArticleInput articleInput, string normalizedUserName,
            string userName)
        {
            if (articleInput.Amount == null || articleInput.Price == null || articleInput.Size == null || 
                normalizedUserName == null || userName == null)
            {
                throw new BadRequestException("The user or the article is invalid.");   
            }
            
            var managedUnit = await UnitRepository.GetOrAddEntityAsync(new DAO.Unit
            {
                Name = articleInput.Unit.Name
            });

            var managedPerson = await PersonRepository.GetOrAddEntityAsync(new DAO.Person
            {
                NormalizedUserName = normalizedUserName,
                UserName = userName
            });
            
            var managedArticle = await ArticleRepository.AddOrUpdateEntityAsync(new DAO.Article
            {
                Person = managedPerson,
                Unit = managedUnit,
                Amount = articleInput.Amount.Value,
                Description = articleInput.Description,
                Name = articleInput.Name,
                Price = articleInput.Price.Value,
                Size = articleInput.Size.Value,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
            
            return Mapper.Map<DTO.ArticleResponse>(managedArticle);
        }

        public async Task<IList<DTO.ArticleResponse>> GetArticlesByExpirationDate(int amount)
        {
            var articles = await ArticleRepository.GetArticlesByExpirationDate(amount);
            return Mapper.Map<IList<DTO.ArticleResponse>>(articles);
        }
    }
}