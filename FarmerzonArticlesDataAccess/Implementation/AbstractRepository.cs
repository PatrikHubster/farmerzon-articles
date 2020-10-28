namespace FarmerzonArticlesDataAccess.Implementation
{
    public class AbstractRepository
    {
        protected FarmerzonArticlesContext Context { get; set; }

        protected AbstractRepository(FarmerzonArticlesContext context)
        {
            Context = context;
        }
    }
}