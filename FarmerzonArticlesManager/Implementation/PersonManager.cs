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

        public async Task<IDictionary<string, DTO.Person>> GetPeopleByArticleIdAsync(IEnumerable<long> ids)
        {
            var articles = 
                await ArticleRepository.GetEntitiesByIdAsync(ids, new List<string>{nameof(DAO.Article.Person)});
            return articles.ToDictionary(key => key.ArticleId.ToString(), 
                value => Mapper.Map<DTO.Person>(value.Person));
        }
    }
}