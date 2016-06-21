using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Vserv.Common.Contracts;

namespace Vserv.Common.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <seealso cref="Vserv.Common.Contracts.IDataRepository{TEntity}" />
    public abstract class DataRepositoryBase<TEntity, TContext> : IDataRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext, new()
    {
        /// <summary>
        /// The _logger
        /// </summary>
        protected ILogger Logger = new Logger();

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRepositoryBase{TEntity, TContext}"/> class.
        /// </summary>
        protected DataRepositoryBase()
        {
            Logger.LoggerType = GetType();
        }

        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        protected abstract TEntity AddEntity(TContext entityContext, TEntity entity, string user);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="user">The user.</param>
        protected abstract void UpdateEntity(TContext entityContext, TEntity entity, string user);

        protected abstract void AddOrUpdateEntity(TContext entityContext, TEntity entity, string user);

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <returns></returns>
        protected abstract IEnumerable<TEntity> GetEntities(TContext entityContext);

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        protected abstract TEntity GetEntity(TContext entityContext, int id);

        /// <summary>
        /// Gets the paged entity.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        protected abstract IEnumerable<TEntity> GetPagedEntity(TContext entityContext, int skip, int take, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties, out int count);

        /// <summary>
        /// Gets the paged entity.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        protected abstract IEnumerable<TEntity> GetPagedEntity(TContext entityContext, int skip, int take, Expression<Func<TEntity, bool>> filter, string orderBy, string includeProperties, out int count);

        /// <summary>
        /// Gets the filtered entities.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        protected abstract IEnumerable<TEntity> GetFilteredEntities(TContext entityContext,
            Expression<Func<TEntity, bool>> filter, string orderBy, string includeProperties, out int count);

        /// <summary>
        /// Gets the loaded entity.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <param name="key">The key.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        protected abstract TEntity GetLoadedEntity(TContext entityContext, Expression<Func<TEntity, bool>> key, string includeProperties);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public virtual TEntity Add(TEntity entity, string user)
        {
            using (var entityContext = new TContext())
            {
                var addedEntity = AddEntity(entityContext, entity, user);
                entityContext.SaveChanges();
                return addedEntity;
            }
        }

        public virtual void AddOrUpdate(TEntity entity, string user)
        {
            using (var entityContext = new TContext())
            {
                AddOrUpdateEntity(entityContext, entity, user);
                entityContext.SaveChanges();
            }
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Remove(TEntity entity)
        {
            using (var entityContext = new TContext())
            {
                entityContext.Entry(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="id"></param>
        public virtual void Remove(int id)
        {
            using (var entityContext = new TContext())
            {
                var entity = GetEntity(entityContext, id);
                if (entity != null)
                {
                    entityContext.Entry(entity).State = EntityState.Deleted;
                    entityContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="user">The user.</param>
        public virtual void Update(TEntity entity, string user)
        {
            using (var entityContext = new TContext())
            {
                UpdateEntity(entityContext, entity, user);
                entityContext.SaveChanges();

            }
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> Get()
        {
            using (var entityContext = new TContext())
                return (GetEntities(entityContext)).ToArray().ToList();
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(int id)
        {
            using (var entityContext = new TContext())
                return GetEntity(entityContext, id);
        }

        /// <summary>
        /// Gets the paged.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPaged(int skip, int take, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties, out int count)
        {

            using (var entityContext = new TContext())
            {
                return GetPagedEntity(entityContext, skip, take, filter, orderBy, includeProperties, out count);
            }
        }

        /// <summary>
        /// Gets the paged.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPaged(int skip, int take, Expression<Func<TEntity, bool>> filter, string orderBy, string includeProperties, out int count)
        {

            using (var entityContext = new TContext())
            {
                entityContext.Database.CommandTimeout = 600;
                return GetPagedEntity(entityContext, skip, take, filter, orderBy, includeProperties, out count);
            }
        }

        /// <summary>
        /// Gets the filtered entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetFilteredEntities(Expression<Func<TEntity, bool>> filter, string orderBy,
            string includeProperties, out int count)
        {
            using (var entityContext = new TContext())
            {
                return GetFilteredEntities(entityContext, filter, orderBy, includeProperties, out count);
            }
        }

        /// <summary>
        /// Gets the loaded.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public TEntity GetLoaded(Expression<Func<TEntity, bool>> key, string includeProperties)
        {
            using (var entityContext = new TContext())
            {
                return GetLoadedEntity(entityContext, key, includeProperties);
            }
        }
    }
}
