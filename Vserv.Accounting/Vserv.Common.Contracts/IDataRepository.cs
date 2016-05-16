using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Vserv.Common.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataRepository
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataRepository<T> : IDataRepository
        where T : class, new()
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        T Add(T entity, string user);
        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Remove(T entity);
        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Remove(int id);
        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="user">The user.</param>
        void Update(T entity, string user);
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Get();
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
        IEnumerable<T> GetPaged(
            int skip,
            int take,
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            string includeProperties,
            out int count
        );
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
        IEnumerable<T> GetPaged(
            int skip,
            int take,
            Expression<Func<T, bool>> filter,
            string orderBy,
            string includeProperties,
            out int count
        );

        /// <summary>
        /// Gets the filtered entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        IEnumerable<T> GetFilteredEntities(
            Expression<Func<T, bool>> filter,
            string orderBy,
            string includeProperties,
            out int count
        );

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Gets the loaded.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        T GetLoaded(Expression<Func<T, bool>> key, string includeProperties);
    }
}