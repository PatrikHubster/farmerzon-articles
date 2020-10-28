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
        private const string OperationNotAllowed = "This address does not exist or is not accessible for this user.";

        private static readonly IList<string> Includes = new List<string>
        {
            nameof(DAO.Article.Unit),
            nameof(DAO.Article.Person)
        };

        private IPersonRepository PersonRepository { get; set; }
        private IUnitRepository UnitRepository { get; set; }
        private IArticleRepository ArticleRepository { get; set; }

        public ArticleManager(ITransactionHandler transactionHandler, IMapper mapper,
            IPersonRepository personRepository, IUnitRepository unitRepository,
            IArticleRepository articleRepository) : base(transactionHandler, mapper)
        {
            PersonRepository = personRepository;
            UnitRepository = unitRepository;
            ArticleRepository = articleRepository;
        }

        private async Task<DAO.Article> CheckAccessRightsAndGetArticleAsync(long articleId, string userName,
            string normalizedUserName)
        {
            var foundArticle = await ArticleRepository.GetEntityByIdAsync(articleId, includes: Includes);

            if (foundArticle == null)
            {
                throw new UnautherizedException(OperationNotAllowed);
            }

            if (foundArticle.Person.UserName != userName ||
                foundArticle.Person.NormalizedUserName != normalizedUserName)
            {
                throw new UnautherizedException(OperationNotAllowed);
            }

            return foundArticle;
        }

        public async Task<DTO.ArticleOutput> InsertEntityAsync(DTO.ArticleInput entity, string userName,
            string normalizedUserName)
        {
            try
            {
                await TransactionHandler.BeginTransactionAsync();
                var person = new DAO.Person
                {
                    UserName = userName,
                    NormalizedUserName = normalizedUserName
                };
                var managedPerson = await PersonRepository.InsertOrGetEntityAsync(person);

                var convertedUnit = Mapper.Map<DAO.Unit>(entity.Unit);
                var managedUnit = await UnitRepository.InsertOrGetEntityAsync(convertedUnit);

                var article = new DAO.Article
                {
                    Person = managedPerson,
                    Unit = managedUnit,
                    Name = entity.Name,
                    Description = entity.Description,
                    Price = entity.Price,
                    Amount = entity.Amount,
                    Size = entity.Size,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    ExpirationDate = entity.ExpirationDate
                };
                var managedArticle = await ArticleRepository.InsertEntityAsync(article);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.ArticleOutput>(managedArticle);
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

        public async Task<DTO.ArticleOutput> UpdateEntityAsync(long id, DTO.ArticleInput entity, string userName,
            string normalizedUserName)
        {
            try
            {
                await TransactionHandler.BeginTransactionAsync();
                var foundArticle = await CheckAccessRightsAndGetArticleAsync(id, userName, normalizedUserName);

                var convertedUnit = Mapper.Map<DAO.Unit>(entity.Unit);
                var managedUnit = await UnitRepository.InsertOrGetEntityAsync(convertedUnit);

                foundArticle.Unit = managedUnit;
                foundArticle.Name = entity.Name;
                foundArticle.Description = entity.Description;
                foundArticle.Price = entity.Price;
                foundArticle.Amount = entity.Amount;
                foundArticle.Size = entity.Size;
                foundArticle.UpdatedAt = DateTime.UtcNow;
                foundArticle.ExpirationDate = entity.ExpirationDate;
                await ArticleRepository.UpdateEntityAsync(foundArticle);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.ArticleOutput>(foundArticle);
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

        public async Task<DTO.ArticleOutput> RemoveEntityByIdAsync(long id, string userName, string normalizedUserName)
        {
            try
            {
                await TransactionHandler.BeginTransactionAsync();
                var foundArticle = await CheckAccessRightsAndGetArticleAsync(id, userName, normalizedUserName);
                var removedArticle = await ArticleRepository.RemoveEntityAsync(foundArticle);
                await TransactionHandler.CommitTransactionAsync();
                return Mapper.Map<DTO.ArticleOutput>(removedArticle);
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

        public async Task<IEnumerable<DTO.ArticleOutput>> GetEntitiesAsync(long? id = null, string name = null,
            string description = null, double? price = null, int? amount = null, double? size = null,
            DateTime? createdAt = null, DateTime? updatedAt = null, DateTime? expirationDate = null)
        {
            var foundArticles = await ArticleRepository.GetEntitiesAsync(filter:
                a => (id == null || a.Id == id) &&
                     (name == null || a.Name == name) &&
                     (description == null || a.Description == description) &&
                     (price == null || a.Price == price) &&
                     (amount == null || a.Amount == amount) &&
                     (size == null || a.Size == size) &&
                     (createdAt == null || a.CreatedAt == createdAt) &&
                     (updatedAt == null || a.UpdatedAt == updatedAt) &&
                     (expirationDate == null || a.ExpirationDate == expirationDate));
            return Mapper.Map<IEnumerable<DTO.ArticleOutput>>(foundArticles);
        }

        public async Task<IDictionary<string, IEnumerable<DTO.ArticleOutput>>> GetEntitiesByNormalizedUserNameAsync(
            IEnumerable<string> normalizedUserNames)
        {
            var foundArticles = await ArticleRepository.GetEntitiesByNormalizedUserNameAsync(normalizedUserNames);
            return Mapper.Map<IDictionary<string, IEnumerable<DTO.ArticleOutput>>>(foundArticles);
        }

        public async Task<IDictionary<string, IEnumerable<DTO.ArticleOutput>>> GetEntitiesByUnitIdAsync(
            IEnumerable<long> ids)
        {
            var foundArticles = await ArticleRepository.GetEntitiesByUnitIdAsync(ids);
            return Mapper.Map<IDictionary<string, IEnumerable<DTO.ArticleOutput>>>(foundArticles);
        }

        public async Task<IDictionary<DateTime, IEnumerable<DTO.ArticleOutput>>> GetEntitiesByExpirationDateAsync(
            int amount)
        {
            var foundArticles = await ArticleRepository.GetEntitiesByExpirationDateAsync(amount);
            return Mapper.Map<IDictionary<DateTime, IEnumerable<DTO.ArticleOutput>>>(foundArticles);
        }

        public async Task<IEnumerable<DTO.ArticleOutput>> GetEntitiesByIdAsync(IEnumerable<long> ids)
        {
            var foundArticles = await ArticleRepository.GetEntitiesByIdAsync(ids);
            return Mapper.Map<IEnumerable<DTO.ArticleOutput>>(foundArticles);
        }

        public async Task<DTO.ArticleOutput> GetEntityByIdAsync(long id)
        {
            var foundArticle = await ArticleRepository.GetEntityByIdAsync(id);
            return Mapper.Map<DTO.ArticleOutput>(foundArticle);
        }
    }
}