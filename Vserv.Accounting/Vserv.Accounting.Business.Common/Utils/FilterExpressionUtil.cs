using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Vserv.Accounting.Common.Enums;

namespace Vserv.Accounting.Business.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class FilterExpressionUtil
    {
        /// <summary>
        /// Gets the lambda filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static LambdaExpression GetLambdaFilter(Filter filter)
        {
            Type entityType = filter.EntityType;
            var item = Expression.Parameter(entityType, entityType.Name);
            var value = Expression.Constant(filter.Value);
            var op = GetExpression(GetPropertyExpression(filter, item), value, filter);
            var func = typeof(Func<,>);
            func.MakeGenericType(entityType, typeof(bool));
            return Expression.Lambda(func.MakeGenericType(entityType, typeof(bool)), op, item);
        }

        /// <summary>
        /// Gets the lambda filters.
        /// </summary>
        /// <param name="filterset">The filterset.</param>
        /// <returns></returns>
        public static Expression GetLambdaFilters(FilterSet filterset)
        {
            if (filterset.Filters.Count > 1 && filterset.Filters.ElementAt(0).FirstOrDefault() != null)
            {
                Type filterType = Type.GetType(filterset.Filters.ElementAt(0).FirstOrDefault().Type) ?? filterset.Filters.ElementAt(0).FirstOrDefault().EntityType;
                var item = Expression.Parameter(filterType, filterType.Name);
                Expression leftSide = null;
                Expression rightSide = null;
                Expression filterExpression = null;
                foreach (var filterList in filterset.Filters)
                {
                    if (leftSide == null)
                    {
                        leftSide = GetInnerFilter(filterList, item);
                        filterExpression = leftSide;
                        continue;
                    }
                    if (rightSide == null)
                    {
                        rightSide = GetInnerFilter(filterList, item);
                        filterExpression = Expression.Or(leftSide, rightSide);
                        continue;
                    }
                    filterExpression = Expression.Or(filterExpression, GetInnerFilter(filterList, item));
                }
                var func = typeof(Func<,>);
                func.MakeGenericType(filterType, typeof(bool));
                return Expression.Lambda(func.MakeGenericType(filterType, typeof(bool)), filterExpression, item);
            }
            else if (filterset.Filters.Count == 1)
            {
                return GetLambdaFilters(filterset.Filters.FirstOrDefault());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the inner filter.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        static Expression GetInnerFilter(ICollection<Filter> filters, Expression p)
        {
            if (filters.Count > 0)
            {

                var item = p;
                Expression leftSide = null;
                Expression rightSide = null;
                Expression filterExpression = null;
                foreach (var filter in filters)
                {
                    Expression left = GetPropertyExpression(filter, item);
                    Expression right = null;
                    if (!filter.IsCollectionQuery)
                    {

                        //Expression left = Expression.Property(item, filter.PropertyName);
                        right = Expression.Constant(filter.Value);
                    }
                    else
                    {
                        Filter innerFilter = new Filter();
                        innerFilter.IsCollectionQuery = false;
                        innerFilter.Operator = filter.Operator;
                        innerFilter.PropertyName = GetCollectionPropertyName(filter, item);
                        innerFilter.Type = filter.CollectionType;
                        innerFilter.Value = filter.Value;
                        List<Filter> innerfilters = new List<Filter>(){
                            innerFilter
                        };
                        right = GetLambdaFilters(innerfilters);
                        filter.Operator = FilterOperator.Any;
                    }
                    Expression comparison = GetExpression(left, right, filter);
                    if (leftSide == null)
                    {
                        leftSide = comparison;
                        filterExpression = leftSide;
                        continue;
                    }
                    if (rightSide == null)
                    {
                        rightSide = comparison;
                        filterExpression = Expression.AndAlso(leftSide, rightSide);
                        continue;
                    }
                    filterExpression = Expression.AndAlso(filterExpression, comparison);
                }
                return filterExpression;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the type of the collection.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        static Type GetCollectionType(Filter filter)
        {
            Type entityType = filter.EntityType;
            List<string> props = filter.PropertyName.Split('.').ToList();
            Type type = new Type[] { entityType }[0];
            StringBuilder sb = new StringBuilder();
            bool isProp = false;
            foreach (string prop in props)
            {
                if (!isProp)
                {
                    PropertyInfo pi = type.GetProperty(prop);
                    type = pi.PropertyType;
                    if (pi.PropertyType.Name.Contains("ICollection") && pi.PropertyType.FullName.Contains("Entities." + filter.CollectionEntityType.Name))
                    {
                        return type;
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Gets the name of the collection property.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        static string GetCollectionPropertyName(Filter filter, Expression item)
        {
            Type entityType = filter.CollectionEntityType;
            List<string> props = filter.CollectionPropertyName.Split('.').ToList();
            Type type = new Type[] { entityType }[0];
            StringBuilder sb = new StringBuilder();
            bool isProp = false;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop);
                type = pi.PropertyType;
                if (isProp)
                {
                    if (pi.PropertyType.Name.Contains("ICollection") && pi.PropertyType.FullName.Contains("Entities." + filter.CollectionEntityType.Name))
                    {
                        sb.Append(prop);
                        //sb.Append(".");
                    }
                }
                else
                {
                    sb.Append(prop);
                    if (props.IndexOf(prop) < props.Count() - 1)
                        sb.Append(".");
                    isProp = true;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the lambda filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        public static Expression GetLambdaFilters(ICollection<Filter> filters)
        {
            if (filters.Count > 0)
            {
                Type entityType = filters.ElementAt(0).EntityType;
                var item = Expression.Parameter(entityType, entityType.Name);
                Expression leftSide = null;
                Expression rightSide = null;
                Expression filterExpression = null;
                foreach (var filter in filters)
                {

                    Expression left = GetPropertyExpression(filter, item);
                    Expression comparison = null;
                    if (left is MethodCallExpression)
                    {
                        comparison = left;
                    }
                    else
                    {
                        Expression right = null;
                        if (!filter.IsCollectionQuery)
                        {
                            right = Expression.Constant(filter.Value);
                        }
                        else
                        {
                            Filter innerFilter = new Filter();
                            innerFilter.IsCollectionQuery = false;
                            innerFilter.Operator = filter.Operator;
                            innerFilter.PropertyName = GetCollectionPropertyName(filter, item);
                            innerFilter.Type = filter.CollectionType;
                            innerFilter.Value = filter.Value;
                            List<Filter> innerfilters = new List<Filter>(){
                            innerFilter
                        };
                            right = GetLambdaFilters(innerfilters);
                            filter.Operator = FilterOperator.Any;
                        }
                        comparison = GetExpression(left, right, filter);
                    }
                    if (leftSide == null)
                    {
                        leftSide = comparison;
                        filterExpression = leftSide;
                        continue;
                    }
                    if (rightSide == null)
                    {
                        rightSide = comparison;
                        filterExpression = Expression.AndAlso(leftSide, rightSide);
                        continue;
                    }
                    filterExpression = Expression.AndAlso(filterExpression, comparison);
                }
                var func = typeof(Func<,>);
                func.MakeGenericType(entityType, typeof(bool));
                return Expression.Lambda(func.MakeGenericType(entityType, typeof(bool)), filterExpression, item);
            }
            else
            {
                return GetLambdaFilter(filters.First());
            }
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        /// <param name="prop">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        static Expression GetExpression(Expression prop, Expression value, Filter filter)
        {
            Expression IgnoreCase = Expression.Constant(true);
            Expression ci = Expression.Constant(System.Globalization.CultureInfo.InvariantCulture);

            switch (filter.Operator)
            {
                case FilterOperator.Equals:
                    value = Expression.Convert(value, prop.Type);
                    return Expression.Equal(prop, value);
                case FilterOperator.Contains:
                    MethodInfo contains = typeof(String).GetMethod("Contains", new Type[] { typeof(String) });
                    return Expression.Call(prop, contains, value);
                case FilterOperator.DoesNotContain:
                    MethodInfo notContains = typeof(String).GetMethod("Contains", new Type[] { typeof(String) });
                    Expression expr = Expression.Call(prop, notContains, value);
                    return Expression.Not(expr);
                case FilterOperator.StartsWith:
                    MethodInfo startsWith = typeof(String).GetMethod("StartsWith", new Type[] { typeof(String) });
                    return Expression.Call(prop, startsWith, value);
                case FilterOperator.StartsWithForEntities:
                    MethodInfo startsWithForEntities = typeof(String).GetMethod("StartsWith", new Type[] { typeof(String), typeof(System.Boolean), typeof(System.Globalization.CultureInfo) });
                    return Expression.Call(prop, startsWithForEntities, new Expression[] { value, IgnoreCase, ci });
                case FilterOperator.EndsWith:
                    MethodInfo endsWith = typeof(String).GetMethod("EndsWith", new Type[] { typeof(String) });
                    return Expression.Call(prop, endsWith, value);
                case FilterOperator.EndsWithForEntities:
                    MethodInfo endsWithForEntities = typeof(String).GetMethod("EndsWith", new Type[] { typeof(String), typeof(System.Boolean), typeof(System.Globalization.CultureInfo) });
                    return Expression.Call(prop, endsWithForEntities, new Expression[] { value, IgnoreCase, ci });
                case FilterOperator.Any:
                    Func<MethodInfo, bool> methodLambda = m => m.Name == "Any" && m.GetParameters().Count() == 1;
                    MethodInfo method = typeof(Enumerable).GetMethods().Where(m => m.Name == "Any" && m.GetParameters().Length == 2).Single().MakeGenericMethod(filter.CollectionEntityType);
                    return Expression.Call(method, prop, value);
                case FilterOperator.GreaterThanOrEqual:
                    value = Expression.Convert(value, prop.Type);
                    return Expression.GreaterThanOrEqual(prop, value);
                case FilterOperator.GreaterThan:
                    value = Expression.Convert(value, prop.Type);
                    return Expression.GreaterThan(prop, value);
                case FilterOperator.LessThan:
                    value = Expression.Convert(value, prop.Type);
                    return Expression.LessThan(prop, value);
                case FilterOperator.LessThanOrEqual:
                    value = Expression.Convert(value, prop.Type);
                    return Expression.LessThanOrEqual(prop, value);
                case FilterOperator.NotEquals:
                    value = Expression.Convert(value, prop.Type);
                    return Expression.NotEqual(prop, value);
                case FilterOperator.DateTimeCompare:
                    if (IsNullabelType(prop.Type))
                        prop = Expression.Convert(prop, typeof(DateTime));
                    MethodInfo compare = typeof(DateTime).GetMethod("Equals", new Type[] { typeof(DateTime) });
                    return Expression.Call(prop, compare, value);
                default:
                    return Expression.Equal(prop, value);
            }
        }

        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }
        /// <summary>
        /// Orders the by descending.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }
        /// <summary>
        /// Thens the by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }
        /// <summary>
        /// Thens the by descending.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }
        /// <summary>
        /// Applies the order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

        /// <summary>
        /// Gets the property expression1.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        static Expression GetPropertyExpression1(Filter filter, Expression item)
        {
            Type entityType = filter.EntityType;
            string[] props = filter.PropertyName.Split('.');
            Type type = new Type[] { entityType }[0];
            Expression expr = null;
            foreach (string prop in props)
            {
                if (expr == null)
                {
                    PropertyInfo pi = type.GetProperty(prop);
                    expr = Expression.Property(item, pi);
                    type = pi.PropertyType;
                }
                else
                {
                    PropertyInfo pi = type.GetProperty(prop);
                    expr = Expression.Property(expr, type, prop);
                    type = pi.PropertyType;
                }
            }
            return expr;
        }

        /// <summary>
        /// Gets the property expression.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        static Expression GetPropertyExpression(Filter filter, Expression item)
        {
            Type entityType = filter.EntityType;
            List<string> props = filter.PropertyName.Split('.').ToList();
            Type type = new Type[] { entityType }[0];
            Expression expr = null;
            foreach (string prop in props)
            {
                if (expr == null)
                {
                    PropertyInfo pi = type.GetProperty(prop);
                    expr = Expression.Property(item, pi);
                    type = pi.PropertyType;
                    if (pi.PropertyType.Name.Contains("ICollection") && pi.PropertyType.FullName.Contains("Entities." + filter.CollectionEntityType.Name))
                        return expr;
                }
                else
                {
                    PropertyInfo pi = type.GetProperty(prop);
                    expr = Expression.Property(expr, type, prop);
                    type = pi.PropertyType;
                    if (pi.PropertyType.Name.Contains("ICollection") && pi.PropertyType.FullName.Contains("Entities." + filter.CollectionEntityType.Name))
                        return expr;
                }
            }
            return expr;
        }

        /// <summary>
        /// Determines whether [is nullabel type] [the specified t].
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        static bool IsNullabelType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

    }
}
