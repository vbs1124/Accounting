using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Vserv.Common.Contracts;

namespace Vserv.Common.Data
{
    public abstract class DataRepositoryBase<TEntity, TContext> : IDataRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext, new()
    {
        protected ILogger _logger = new Logger();

        protected DataRepositoryBase()
        {
            _logger.LoggerType = this.GetType();
        }

        protected abstract TEntity AddEntity(TContext entityContext, TEntity entity, string user);

        protected abstract void UpdateEntity(TContext entityContext, TEntity entity, string user);

        protected abstract IEnumerable<TEntity> GetEntities(TContext entityContext);

        protected abstract TEntity GetEntity(TContext entityContext, int id);

        protected abstract IEnumerable<TEntity> GetPagedEntity(TContext entityContext, int skip, int take, Expression<System.Func<TEntity, bool>> filter, System.Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties, out int count);

        protected abstract IEnumerable<TEntity> GetPagedEntity(TContext entityContext, int skip, int take, Expression<System.Func<TEntity, bool>> filter, string orderBy, string includeProperties, out int count);

        protected abstract IEnumerable<TEntity> GetFilteredEntities(TContext entityContext,
            Expression<Func<TEntity, bool>> filter, string orderBy, string includeProperties, out int count);

        protected abstract TEntity GetLoadedEntity(TContext entityContext, Expression<Func<TEntity, bool>> key, string includeProperties);

        public virtual TEntity Add(TEntity entity, string user)
        {
            using (var entityContext = new TContext())
            {
                var addedEntity = AddEntity(entityContext, entity, user);
                entityContext.SaveChanges();
                return addedEntity;
            }
        }

        public virtual void Remove(TEntity entity)
        {
            using (var entityContext = new TContext())
            {
                entityContext.Entry<TEntity>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public virtual void Remove(int id)
        {
            using (var entityContext = new TContext())
            {
                var entity = GetEntity(entityContext, id);
                if (entity != null)
                {
                    entityContext.Entry<TEntity>(entity).State = EntityState.Deleted;
                    entityContext.SaveChanges();
                }
            }
        }

        public virtual void Update(TEntity entity, string user)
        {
            using (var entityContext = new TContext())
            {
                UpdateEntity(entityContext, entity, user);
                entityContext.SaveChanges();

            }
        }

        public IEnumerable<TEntity> Get()
        {
            using (var entityContext = new TContext())
                return (GetEntities(entityContext)).ToArray().ToList();
        }

        public TEntity Get(int id)
        {
            using (var entityContext = new TContext())
                return GetEntity(entityContext, id);
        }

        public IEnumerable<TEntity> GetPaged(int skip, int take, Expression<System.Func<TEntity, bool>> filter, System.Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties, out int count)
        {

            using (var entityContext = new TContext())
            {
                return GetPagedEntity(entityContext, skip, take, filter, orderBy, includeProperties, out count);
            }
        }

        public IEnumerable<TEntity> GetPaged(int skip, int take, Expression<System.Func<TEntity, bool>> filter, string orderBy, string includeProperties, out int count)
        {

            using (var entityContext = new TContext())
            {
                entityContext.Database.CommandTimeout = 600;
                return GetPagedEntity(entityContext, skip, take, filter, orderBy, includeProperties, out count);
            }
        }

        public IEnumerable<TEntity> GetFilteredEntities(Expression<Func<TEntity, bool>> filter, string orderBy,
            string includeProperties, out int count)
        {
            using (var entityContext = new TContext())
            {
                return GetFilteredEntities(entityContext, filter, orderBy, includeProperties, out count);
            }
        }

        public TEntity GetLoaded(Expression<Func<TEntity, bool>> key, string includeProperties)
        {
            using (var entityContext = new TContext())
            {
                return GetLoadedEntity(entityContext, key, includeProperties);
            }
        }
    }
}
