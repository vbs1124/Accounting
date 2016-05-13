using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Vserv.Common.Contracts
{
    public interface IDataRepository
    {
    }

    public interface IDataRepository<T> : IDataRepository
        where T : class, new()
    {
        T Add(T entity, string user);
        void Remove(T entity);
        void Remove(int id);
        void Update(T entity, string user);
        IEnumerable<T> Get();
        IEnumerable<T> GetPaged(
            int skip,
            int take,
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            string includeProperties,
            out int count
        );
        IEnumerable<T> GetPaged(
            int skip,
            int take,
            Expression<Func<T, bool>> filter,
            string orderBy,
            string includeProperties,
            out int count
        );

        IEnumerable<T> GetFilteredEntities(
            Expression<Func<T, bool>> filter,
            string orderBy,
            string includeProperties,
            out int count
        );

        T Get(int id);
        T GetLoaded(Expression<Func<T, bool>> key, string includeProperties);
    }
}