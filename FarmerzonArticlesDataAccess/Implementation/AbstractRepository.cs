namespace FarmerzonArticlesDataAccess.Implementation
{
    public class AbstractRepository
    {
        protected FarmerzonArticlesContext Context { get; set; }

        public AbstractRepository(FarmerzonArticlesContext context)
        {
            Context = context;
        }
    }
}