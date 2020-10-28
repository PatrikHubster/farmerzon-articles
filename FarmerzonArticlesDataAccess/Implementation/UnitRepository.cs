using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        public UnitRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }

        protected override Task<Unit> GetEntityAsync(Unit entity)
        {
            throw new System.NotImplementedException();
        }
    }
}