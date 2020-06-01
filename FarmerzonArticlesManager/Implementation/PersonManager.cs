using System.Collections.Generic;
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
        private IPersonRepository PersonRepository { get; set; }

        public PersonManager(IMapper mapper, IPersonRepository personRepository) : base(mapper)
        {
            PersonRepository = personRepository;
        }
        
        public async Task<IList<DTO.Person>> GetEntitiesAsync(int? id, string userName, string normalizedUserName)
        {
            var people = await PersonRepository.GetEntitiesAsync(id, userName, normalizedUserName);
            return Mapper.Map<IList<DTO.Person>>(people);
        }

        public async Task<DTO.Person> GetPersonByArticleAsync(DTO.Article article)
        {
            var convertedArticle = Mapper.Map<DAO.Article>(article);
            var person = await PersonRepository.GetPersonByArticleAsync(convertedArticle);
            return Mapper.Map<DTO.Person>(person);
        }
    }
}