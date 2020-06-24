using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class AbstractRepository<T>
    {
        protected FarmerzonArticlesContext Context { get; set; }

        public AbstractRepository(FarmerzonArticlesContext context)
        {
            Context = context;
        }
        
        public async Task<T> AddOrUpdateEntityAsync(T entity)
        {
            var savedEntry = entity;
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                savedEntry = (T) (await Context.AddAsync(entity)).Entity;
            }
            await Context.SaveChangesAsync();
            return savedEntry;
        }
    }
}