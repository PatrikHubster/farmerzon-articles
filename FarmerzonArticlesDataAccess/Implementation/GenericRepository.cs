using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FarmerzonArticlesDataAccess.Interface;
using FarmerzonArticlesDataAccessModel;
using FarmerzonArticlesErrorHandling.CustomException;
using Microsoft.EntityFrameworkCore;

namespace FarmerzonArticlesDataAccess.Implementation
{
    public abstract class GenericRepository<T> : AbstractRepository, IBasicRepository<T> where T : BaseModel
    {
        private const string EntityAlreadyExistsError = "This entry already exists in the system.";
        private const string EntityNotExistsError = "This entry does not exist in the system.";
        
        protected GenericRepository(FarmerzonArticlesContext context) : base(context)
        {
            // nothing to do here
        }
        
        protected abstract Task<T> GetEntityAsync(T entity);
        
        public virtual async Task<T> InsertEntityAsync(T entity)
        {
            var foundEntity = await GetEntityAsync(entity);
            if (foundEntity != null)
            {
                throw new BadRequestException(EntityAlreadyExistsError);
            }

            var result = await Context.Set<T>().AddAsync(entity);

            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public virtual async Task<T> InsertOrGetEntityAsync(T entity)
        {
            var foundEntity = await GetEntityAsync(entity);
            if (foundEntity != null)
            {
                return foundEntity;
            }
                
            var result = await Context.Set<T>().AddAsync(entity);

            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public virtual async Task UpdateEntityAsync(T entity)
        {
            var alreadyExistingEntity = await GetEntityAsync(entity);
            if (alreadyExistingEntity != null && alreadyExistingEntity.Id != entity.Id)
            {
                throw new BadRequestException(EntityAlreadyExistsError);
            }

            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task<T> RemoveEntityByIdAsync(long id)
        {
            var foundEntity = await GetEntityByIdAsync(id);
            if (foundEntity == null)
            {
                throw new NotFoundException(EntityNotExistsError);
            }

            return await RemoveEntityAsync(foundEntity);
        }

        public virtual async Task<T> RemoveEntityAsync(T entity)
        {
            var result = Context.Set<T>().Remove(entity);

            await Context.SaveChangesAsync();
            return result.Entity;
        }
        
        private IQueryable<T> BaseQuery(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IEnumerable<string> includes = null)
        {
            IQueryable<T> query = Context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.IncludeMany(includes);
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            
            return query;
        }

        public virtual async Task<IEnumerable<T>> GetEntitiesAsync(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IEnumerable<string> includes = null)
        {
            return await BaseQuery(filter: filter, orderBy: orderBy, includes: includes)
                .ToListAsync();
        }

        public virtual async Task<T> GetEntityAsync(Expression<Func<T, bool>> filter = null, 
            IEnumerable<string> includes = null)
        {
            return await BaseQuery(filter: filter, includes: includes)
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> GetEntitiesByIdAsync(IEnumerable<long> ids, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IEnumerable<string> includes = null)
        {
            return await BaseQuery(orderBy: orderBy, includes: includes)
                .Where(e => ids.Contains(e.Id))
                .ToListAsync();
        }

        public virtual async Task<T> GetEntityByIdAsync(long id, IEnumerable<string> includes = null)
        {
            return await BaseQuery(includes: includes)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}