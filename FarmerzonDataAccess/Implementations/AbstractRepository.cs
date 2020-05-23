using FarmerzonDataAccess.Context;

namespace FarmerzonDataAccess.Implementations
{
    public class AbstractRepository
    {
        protected FarmerzonContext Context { get; set; }

        public AbstractRepository(FarmerzonContext context)
        {
            Context = context;
        }
    }
}