using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccess.Context;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interfaces
{
    public interface IPersonRepository
    {
        public Task<IList<Person>> GetEntities(int? id, string userName, 
            string normalizedUserName, FarmerzonContext context);
    }
}