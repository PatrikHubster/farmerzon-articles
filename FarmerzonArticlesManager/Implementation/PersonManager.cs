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
    public class PersonManager : AbstractManager, IPersonManager
    {
        private IArticleRepository ArticleRepository { get; set; }
        private IPersonRepository PersonRepository { get; set; }

        public PersonManager(IMapper mapper, IPersonRepository personRepository, 
            IArticleRepository articleRepository) : base(mapper)
        {
            ArticleRepository = articleRepository;
            PersonRepository = personRepository;
        }
        
        public async Task<IList<DTO.Person>> GetEntitiesAsync(long? id, string userName, string normalizedUserName)
        {
            var people = await PersonRepository.GetEntitiesAsync(id, userName, normalizedUserName);
            return Mapper.Map<IList<DTO.Person>>(people);
        }

        public async Task<IDictionary<long, DTO.Person>> GetPeopleByArticleIdAsync(IEnumerable<long> ids)
        {
            var articles = 
                await ArticleRepository.GetEntitiesByIdAsync(ids, new List<string>{nameof(DAO.Article.Person)});
            var personForArticle = new Dictionary<long, DTO.Person>();
            var people = new Dictionary<long, DTO.Person>();
            foreach (var article in articles)
            {
                if (personForArticle.ContainsKey(article.ArticleId)) continue;
                
                // If you save the already converted units in an own dictionary you save the conversion if the
                // unit occurs 1000 times. If this is not necessary you have lost a single assignment.
                if (!people.ContainsKey(article.Person.PersonId))
                {
                    people.Add(article.Person.PersonId, Mapper.Map<DTO.Person>(article.Person));
                }
                personForArticle.Add(article.ArticleId, people[article.Person.PersonId]);
            }

            return personForArticle;
        }
    }
}