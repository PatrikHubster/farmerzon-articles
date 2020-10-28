using AutoMapper;
using FarmerzonArticlesDataAccess.Interface;

namespace FarmerzonArticlesManager.Implementation
{
    public abstract class AbstractManager
    {
        protected ITransactionHandler TransactionHandler { get; set; }
        protected IMapper Mapper { get; set; }
        
        protected AbstractManager(ITransactionHandler transactionHandler, IMapper mapper)
        {
            TransactionHandler = transactionHandler;
            Mapper = mapper;
        } 
    }
}