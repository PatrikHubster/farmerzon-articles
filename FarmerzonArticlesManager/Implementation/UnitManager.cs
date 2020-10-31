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
    public class UnitManager : AbstractManager, IUnitManager
    {
        private IUnitRepository UnitRepository { get; set; }

        public UnitManager(ITransactionHandler transactionHandler, IMapper mapper, 
            IUnitRepository unitRepository) : base(transactionHandler, mapper)
        {
            UnitRepository = unitRepository;
        }

        public async Task<DTO.UnitOutput> InsertEntityAsync(DTO.UnitInput entity)
        {
            try
            {
                await TransactionHandler.BeginTransactionAsync();
                var convertedUnit = Mapper.Map<DAO.Unit>(entity);
                var insertedUnit = await UnitRepository.InsertEntityAsync(convertedUnit);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.UnitOutput>(insertedUnit);
            }
            catch
            {
                await TransactionHandler.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await TransactionHandler.DisposeTransactionAsync();
            }
        }

        public async Task<DTO.UnitOutput> UpdateEntityAsync(long id, DTO.UnitInput entity)
        {
            try
            {
                await TransactionHandler.BeginTransactionAsync();
                var foundUnit = await UnitRepository.GetEntityByIdAsync(id);
                if (foundUnit == null)
                {
                    throw new NotFoundException("This unit does not exist.");
                }
                
                foundUnit.Name = entity.Name;

                await UnitRepository.UpdateEntityAsync(foundUnit);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.UnitOutput>(foundUnit);
            }
            catch
            {
                await TransactionHandler.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await TransactionHandler.DisposeTransactionAsync();
            }
        }

        public async Task<DTO.UnitOutput> RemoveEntityByIdAsync(long id)
        {
            try
            {
                await TransactionHandler.BeginTransactionAsync();
                var removedUnit = await UnitRepository.RemoveEntityByIdAsync(id);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.UnitOutput>(removedUnit);
            }
            catch
            {
                await TransactionHandler.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await TransactionHandler.DisposeTransactionAsync();
            }
        }

        public async Task<IEnumerable<DTO.UnitOutput>> GetEntitiesByIdAsync(IEnumerable<long> ids)
        {
            var foundUnits = await UnitRepository.GetEntitiesByIdAsync(ids);
            return Mapper.Map<IEnumerable<DTO.UnitOutput>>(foundUnits);
        }

        public async Task<DTO.UnitOutput> GetEntityByIdAsync(long id)
        {
            var foundUnit = await UnitRepository.GetEntityByIdAsync(id);
            return Mapper.Map<DTO.UnitOutput>(foundUnit);
        }

        public async Task<IEnumerable<DTO.UnitOutput>> GetEntitiesAsync(long? id = null, string name = null)
        {
            var foundUnits = await UnitRepository.GetEntitiesAsync(filter: 
                u => (id == null || u.Id == id) && (name == null || u.Name == name));
            return Mapper.Map<IEnumerable<DTO.UnitOutput>>(foundUnits);
        }

        public async Task<IDictionary<string, DTO.UnitOutput>> GetEntitiesByArticleIdAsync(IEnumerable<long> ids)
        {
            var foundUnits = await UnitRepository.GetEntitiesByArticleIdAsync(ids);
            return Mapper.Map<IDictionary<string, DTO.UnitOutput>>(foundUnits);
        }
    }
}