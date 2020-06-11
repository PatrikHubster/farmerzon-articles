namespace FarmerzonArticlesDataAccess.Implementation
{
    public class AbstractRepository
    {
        protected FarmerzonArticlesContext Context { get; }

        public AbstractRepository(FarmerzonArticlesContext context)
        {
            Context = context;
        }
    }
}