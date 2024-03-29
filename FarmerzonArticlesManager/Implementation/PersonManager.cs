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

        public PersonManager(ITransactionHandler transactionHandler, IMapper mapper,
            IPersonRepository personRepository) : base(transactionHandler, mapper)
        {
            PersonRepository = personRepository;
        }

        public async Task<IDictionary<string, DTO.PersonOutput>> GetEntitiesByArticleIdAsync(IEnumerable<long> ids)
        {
            var foundPeople = await PersonRepository.GetEntitiesByArticleIdAsync(ids);
            return Mapper.Map<IDictionary<string, DTO.PersonOutput>>(foundPeople);
        }
    }
}