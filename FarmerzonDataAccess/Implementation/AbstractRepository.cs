using FarmerzonDataAccess.Context;

namespace FarmerzonDataAccess.Implementation
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