using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public class AbstractRepository<T> where T : class
    {
        protected FarmerzonArticlesContext Context { get; set; }

        public AbstractRepository(FarmerzonArticlesContext context)
        {
            Context = context;
        }

        protected IQueryable<T> AddIncludesToQuery(IQueryable<T> query, IList<string> includes)
        {
            if (includes != null && includes.Count > 0)
            {
                query = includes.Aggregate(query, 
                    (current, include) => current.Include(include));
            }

            return query;
        }
    }
}