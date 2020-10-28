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

        public UnitManager(ITransactionHandler transactionHandler, IMapper mapper, 
            IUnitRepository unitRepository) : base(transactionHandler, mapper)
        {
            UnitRepository = unitRepository;
        }

        public Task<DTO.UnitOutput> InsertEntityAsync(DTO.UnitInput entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<DTO.UnitOutput> UpdateEntityAsync(long id, DTO.UnitInput entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<DTO.UnitOutput> RemoveEntityByIdAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<DTO.UnitOutput>> GetEntitiesByIdAsync(IEnumerable<long> ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<DTO.UnitOutput> GetEntityByIdAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<DTO.UnitOutput>> GetEntitiesAsync(long? id = null, string name = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDictionary<string, DTO.UnitOutput>> GetEntitiesByArticleIdAsync(IEnumerable<long> ids)
        {
            throw new System.NotImplementedException();
        }
    }
}