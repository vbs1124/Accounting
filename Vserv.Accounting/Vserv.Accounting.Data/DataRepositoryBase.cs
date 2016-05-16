#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using Vserv.Common.Data;
using Vserv.Common.Contracts;
using Vserv.Accounting.Data.Entity;
using Vserv.Accounting.Business.Common;
#endregion

namespace Vserv.Accounting.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Vserv.Common.Data.DataRepositoryBase{T,Vserv.Accounting.Data.Entity.VservAccountingDBEntities}" />
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, VservAccountingDBEntities>
        where T : class, new()
    {
        /// <summary>
        /// The _dbset
        /// </summary>
        protected DbSet<T> _dbset;
        /// <summary>
        /// The _dispose
        /// </summary>
        private bool _dispose;
        /// <summary>
        /// Initializes a new instance of the <see cref="DataRepositoryBase{T}" /> class.
        /// </summary>
        protected DataRepositoryBase()
        {

        }

        /// <summary>
        /// Adds the entity.
        /// </summary>
        /// <param name="entityContext">The entity context.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        protected override T AddEntity(VservAccountingDBEntities entityContext, T entity, string user)
        {
            _dbset = entityContext.Set<T>();
            var retVal = _dbset.Add(entity);
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
            _dbset = entityContext.Set<T>();
            _dbset.Attach(entity);
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
            _dbset = entityContext.Set<T>();
            return from e in _dbset
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
            _dbset = entityContext.Set<T>();
            return _dbset.Find(id);
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
        protected override IEnumerable<T> GetPagedEntity(VservAccountingDBEntities entityContext, int skip, int take, System.Linq.Expressions.Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties, out int count)
        {
            IQueryable<T> query = entityContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            count = query.Count();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).Skip(skip).Take(take).ToArray().ToList();
            }
            else
            {
                return query.OrderBy(a => 1).Skip(skip).Take(take).ToArray().ToList();
            }
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
        protected override IEnumerable<T> GetPagedEntity(VservAccountingDBEntities entityContext, int skip, int take, System.Linq.Expressions.Expression<Func<T, bool>> filter, string orderBy, string includeProperties, out int count)
        {
            IQueryable<T> query = entityContext.Set<T>();
            string[] orderby = !string.IsNullOrEmpty(orderBy) ? orderBy.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            count = query.Count();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (!string.IsNullOrWhiteSpace(orderBy) && orderby != null && orderby.Length > 0)
            {
                for (int i = 0; i < orderby.Length; i++)
                {
                    if (i == 0)
                    {
                        if (orderby[i].ToLower().Contains(" desc"))
                        {
                            query = FilterExpressionUtil.OrderByDescending(query, orderby[i].Trim().Split(' ')[0]);
                        }
                        else
                        {
                            query = FilterExpressionUtil.OrderBy(query, orderby[i].Trim());
                        }
                    }
                    else
                    {
                        if (orderby[i].ToLower().Contains(" desc"))
                        {
                            query = FilterExpressionUtil.ThenByDescending(query as IOrderedQueryable<T>, orderby[i].Trim().Split(' ')[0]);
                        }
                        else
                        {
                            query = FilterExpressionUtil.ThenBy(query as IOrderedQueryable<T>, orderby[i].Trim());
                        }
                    }
                }
                return query.Skip(skip).Take(take).ToArray().ToList();
            }
            else
            {
                return query.OrderBy(a => 1).Skip(skip).Take(take).ToArray().ToList();
            }
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
            string[] orderby = !string.IsNullOrEmpty(orderBy) ? orderBy.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            count = query.Count();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (!string.IsNullOrWhiteSpace(orderBy) && orderby != null && orderby.Length > 0)
            {
                for (int i = 0; i < orderby.Length; i++)
                {
                    if (i == 0)
                    {
                        if (orderby[i].ToLower().Contains(" desc"))
                        {
                            query = FilterExpressionUtil.OrderByDescending(query, orderby[i].Trim().Split(' ')[0]);
                        }
                        else
                        {
                            query = FilterExpressionUtil.OrderBy(query, orderby[i].Trim());
                        }
                    }
                    else
                    {
                        if (orderby[i].ToLower().Contains(" desc"))
                        {
                            query = FilterExpressionUtil.ThenByDescending(query as IOrderedQueryable<T>, orderby[i].Trim().Split(' ')[0]);
                        }
                        else
                        {
                            query = FilterExpressionUtil.ThenBy(query as IOrderedQueryable<T>, orderby[i].Trim());
                        }
                    }
                }
                return query.ToArray().ToList();
            }
            else
            {
                return query.OrderBy(a => 1).ToArray().ToList();
            }

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
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (query.Count(key) > 0)
                return query.First(key);
            return null;
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
                if (ent.State == System.Data.Entity.EntityState.Added)
                {
                    e.CreatedDate = DateTime.UtcNow;
                    //e.UpdatedDate = DateTime.UtcNow;
                    e.CreatedBy = user;
                    //e.UpdatedBy = user;
                }
                if (ent.State == System.Data.Entity.EntityState.Modified)
                {
                    e.UpdatedDate = DateTime.UtcNow;
                    e.UpdatedBy = user;
                }
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="isDisposed"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool isDisposed)
        {
            this._dispose = isDisposed;
        }
    }
}
