#region Namespaces

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Vserv.Accounting.Business.Common;
using Vserv.Accounting.Data.Entity;
using Vserv.Common.Contracts;
using Vserv.Common.Data;

#endregion

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, VservAccountingDBEntities>
        where T : class, new()
    {
        /// <summary>
        /// The _dbset
        /// </summary>
        protected DbSet<T> Dbset;

        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        protected override T AddEntity(VservAccountingDBEntities entityContext, T entity, string user)
        {
            Dbset = entityContext.Set<T>();
            var retVal = Dbset.Add(entity);
            //    SetAuditValues(entityContext, entity, user);
            return retVal;
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="user">The user.</param>
        protected override void UpdateEntity(VservAccountingDBEntities entityContext, T entity, string user)
        {
            Dbset = entityContext.Set<T>();
            Dbset.Attach(entity);
            entityContext.Entry(entity).State = EntityState.Modified;
            //  SetAuditValues(entityContext, entity, user);
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <returns></returns>
        protected override IEnumerable<T> GetEntities(VservAccountingDBEntities entityContext)
        {
            Dbset = entityContext.Set<T>();
            return from e in Dbset
                   select e;
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        protected override T GetEntity(VservAccountingDBEntities entityContext, int id)
        {
            Dbset = entityContext.Set<T>();
            return Dbset.Find(id);
        }

        /// <summary>
        /// Gets a paged list of entities
        /// </summary>
        /// <param name="entityContext">DbContext</param>
        /// <param name="skip">Integer for the Skip portion of the paging</param>
        /// <param name="take">Integer for the Take portion of the paging</param>
        /// <param name="filter">Lambda expression to use for filtering</param>
        /// <param name="orderBy">Lambda expression to use for Ordering</param>
        /// <param name="includeProperties">Comma seperated list of Includes</param>
        /// <param name="count">Returns the total number of records</param>
        /// <returns></returns>
        protected override IEnumerable<T> GetPagedEntity(VservAccountingDBEntities entityContext, int skip, int take, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties, out int count)
        {
            IQueryable<T> query = entityContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            count = query.Count();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                if (query != null)
                    query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return orderBy != null ? orderBy(query).Skip(skip).Take(take).ToArray().ToList() : query.OrderBy(a => 1).Skip(skip).Take(take).ToArray().ToList();
        }

        /// <summary>
        /// Gets a paged list of entities
        /// </summary>
        /// <param name="entityContext">DbContext</param>
        /// <param name="skip">Integer for the Skip portion of the paging</param>
        /// <param name="take">Integer for the Take portion of the paging</param>
        /// <param name="filter">Lambda expression to use for filtering</param>
        /// <param name="orderBy">Comma separated list of property names to use for Ordering</param>
        /// <param name="includeProperties">Comma seperated list of Includes</param>
        /// <param name="count">Returns the total number of records</param>
        /// <returns></returns>
        protected override IEnumerable<T> GetPagedEntity(VservAccountingDBEntities entityContext, int skip, int take, Expression<Func<T, bool>> filter, string orderBy, string includeProperties, out int count)
        {
            IQueryable<T> query = entityContext.Set<T>();
            string[] orderby = !string.IsNullOrEmpty(orderBy) ? orderBy.Split(separator: new[] { ',' }, options: StringSplitOptions.RemoveEmptyEntries) : null;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            count = query.Count();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            if (!string.IsNullOrWhiteSpace(orderBy) && orderby != null && orderby.Length > 0)
            {
                for (int i = 0; i < orderby.Length; i++)
                {
                    if (i == 0)
                    {
                        query = @orderby[i].ToLower().Contains(" desc") ? FilterExpressionUtil.OrderByDescending(query, @orderby[i].Trim().Split(' ')[0]) : FilterExpressionUtil.OrderBy(query, @orderby[i].Trim());
                    }
                    else
                    {
                        query = @orderby[i].ToLower().Contains(" desc") ? FilterExpressionUtil.ThenByDescending(query as IOrderedQueryable<T>, @orderby[i].Trim().Split(' ')[0]) : FilterExpressionUtil.ThenBy(query as IOrderedQueryable<T>, @orderby[i].Trim());
                    }
                }
                return query.Skip(skip).Take(take).ToArray().ToList();
            }
            return query.OrderBy(a => 1).Skip(skip).Take(take).ToArray().ToList();
        }

        /// <summary>
        /// Gets the filtered entities.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        protected override IEnumerable<T> GetFilteredEntities(VservAccountingDBEntities entityContext, Expression<Func<T, bool>> filter, string orderBy,
            string includeProperties, out int count)
        {
            IQueryable<T> query = entityContext.Set<T>();
            string[] orderby = !string.IsNullOrEmpty(orderBy) ? orderBy.Split(separator: new[] { ',' }, options: StringSplitOptions.RemoveEmptyEntries) : null;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            count = query.Count();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            if (string.IsNullOrWhiteSpace(orderBy) || @orderby == null || @orderby.Length <= 0)
                return query.OrderBy(a => 1).ToArray().ToList();

            for (int i = 0; i < @orderby.Length; i++)
            {
                if (i == 0)
                {
                    query = @orderby[i].ToLower().Contains(" desc") ? FilterExpressionUtil.OrderByDescending(query, @orderby[i].Trim().Split(' ')[0]) : FilterExpressionUtil.OrderBy(query, @orderby[i].Trim());
                }
                else
                {
                    query = @orderby[i].ToLower().Contains(" desc") ? FilterExpressionUtil.ThenByDescending(query as IOrderedQueryable<T>, @orderby[i].Trim().Split(' ')[0]) : FilterExpressionUtil.ThenBy(query as IOrderedQueryable<T>, @orderby[i].Trim());
                }
            }
            return query.ToArray().ToList();
        }

        /// <summary>
        /// Allows for retrieving an entity loaded with the Includes specified
        /// </summary>
        /// <param name="context">The DbContext</param>
        /// <param name="key">Lambda expression for finding the Entity</param>
        /// <param name="includeProperties">Comma seperated list of child Entities to include, i.e. "Franchise, FranchiseOperation"</param>
        /// <returns>
        /// Returns Entity of Type T
        /// </returns>
        protected override T GetLoadedEntity(VservAccountingDBEntities context, Expression<Func<T, bool>> key, string includeProperties)
        {
            DbQuery<T> query = context.Set<T>();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return query.Count(key) > 0 ? query.First(key) : null;
        }

        /// <summary>
        /// Sets the Audit Values
        /// </summary>
        /// <param name="entityContext">DbContext</param>
        /// <param name="entity">Entity of Type T</param>
        /// <param name="user">UserName to stamp the records with</param>
        protected void SetAuditValues(VservAccountingDBEntities entityContext, T entity, string user)
        {
            entityContext.ChangeTracker.DetectChanges();
            var set = entityContext.ChangeTracker.Entries()
                .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified) && e.Entity is IAuditableEntity);
            foreach (var ent in set)
            {
                IAuditableEntity e = ent.Entity as IAuditableEntity;
                if (ent.State == EntityState.Added)
                {
                    if (e != null)
                    {
                        e.CreatedDate = DateTime.UtcNow;
                        //e.UpdatedDate = DateTime.UtcNow;
                        e.CreatedBy = user;
                    }
                    //e.UpdatedBy = user;
                }
                if (ent.State != EntityState.Modified) continue;
                if (e == null) continue;
                e.UpdatedDate = DateTime.UtcNow;
                e.UpdatedBy = user;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="isDisposed"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool isDisposed)
        {
        }
    }
}
