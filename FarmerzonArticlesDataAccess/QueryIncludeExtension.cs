using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess
{
    public static class QueryIncludeExtension
    {
        public static IQueryable<T> IncludeMany<T>(this IQueryable<T> query, 
            IEnumerable<string> includes) where T : class
        {
            if (includes == null)
            {
                return query;
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public static IQueryable<T> IncludeMany<T>(this IQueryable<T> query,
            IEnumerable<string> includes, string relationship) where T : class
        {
            if (relationship == null)
            {
                return query.IncludeMany(includes);
            }

            query = query.Include(relationship);

            if (includes == null)
            {
                return query;
            }

            foreach (var include in includes)
            {
                query = query.Include($"{relationship}.{include}");
            }

            return query;
        }
    }
}