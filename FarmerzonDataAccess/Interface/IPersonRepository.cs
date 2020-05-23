using System.Collections.Generic;
using System.Threading.Tasks;
using FarmerzonDataAccessModel;

namespace FarmerzonDataAccess.Interface
{
    public interface IPersonRepository
    {
        public Task<IList<Person>> GetEntities(int? id, string userName, string normalizedUserName);
    }
}