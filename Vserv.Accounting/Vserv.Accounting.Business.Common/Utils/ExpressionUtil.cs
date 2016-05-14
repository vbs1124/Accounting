using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Vserv.Accounting.Common.Enums;

namespace Vserv.Accounting.Business.Common
{

    public static class ExpressionUtil
    {

        public static Expression GetLambdaExpression(FilterExpressionNode rootNode)
        {
            if ((rootNode.FilterExpressionNodes != null && rootNode.FilterExpressionNodes.Count > 0) || (rootNode.Filters != null && rootNode.Filters.Count > 0))
            {
                //get the base type for the expression
                Type entityType = GetExpressionType(rootNode);
                //Create the paramater expression as the base
                var item = Expression.Parameter(entityType, entityType.Name);
                //set up the return values
                var func = typeof(Func<,>);
                func.MakeGenericType(entityType, typeof(bool));
                //return the resutls of GetExpression as a lambda expression
                return Expression.Lambda(func.MakeGenericType(entityType, typeof(bool)), GetExpression(rootNode), item);
            }
            else
            {
                throw new InvalidOperationException("Empty collection supplied.");
            }
        }

        public static Expression GetExpression(FilterExpressionNode rootNode)
        {
            //check to ensure we have values to work with
            if ((rootNode.FilterExpressionNodes != null && rootNode.FilterExpressionNodes.Count > 0) || (rootNode.Filters != null && rootNode.Filters.Count > 0))
            {
                //set-up
                Expression filterExpression = null;
                Type entityType = GetExpressionType(rootNode);
                var item = Expression.Parameter(entityType, entityType.Name);
                //if there are no child nodes
                if (rootNode.FilterExpressionNodes == null || rootNode.FilterExpressionNodes.Count == 0)
                {
                    //get an expression that evaluates the filter list in the node
                    filterExpression = GetInnerFilter(rootNode.Filters, item, rootNode.Logic);
                    return filterExpression;
                }
                //otherwise
                else
                {
                    //begin walking the tree to return an expression at each level that can have an and/or operator applied
                    //iterate the nodes in the node list of the given root
                    foreach (var filterNode in rootNode.FilterExpressionNodes)
                    {
                        //recursion begins here
                        if (filterExpression == null)
                        {
                            filterExpression = GetExpression(filterNode);
                        }else{
                            //apply root node logic to current state of expression and next evlaution of GetExpression
                         if (rootNode.Logic == FilterLogic.And)
                                filterExpression = Expression.AndAlso(filterExpression, GetExpression(filterNode));
                            else
                             filterExpression = Expression.Or(filterExpression, GetExpression(filterNode));
                        }
                    }
                    //if the given root node has only filters add them to the epression
                    if (rootNode.Filters != null && rootNode.Filters.Count > 0)
                    {
                        if (rootNode.Logic == FilterLogic.And)
                            filterExpression = Expression.AndAlso(filterExpression,GetInnerFilter(rootNode.Filters, item, rootNode.Logic));
                        else
                            filterExpression = Expression.Or(filterExpression, GetInnerFilter(rootNode.Filters, item, rootNode.Logic));
                    }
                    return filterExpression;
                }
            }
            else
            {
                throw new InvalidOperationException("A filter or filter node must be supplied.");
            }
        }

       

        public static Type GetExpressionType(FilterExpressionNode root)
        {
            Type type = null;
            while (type == null)
            {
                if(root.Filters != null && root.Filters.Count > 0)
                    type = root.Filters.ElementAt(0).EntityType;
                else
                    foreach (var node in root.FilterExpressionNodes)
	                {
                        type = GetExpressionType(node);
                        if (type != null)
                            break;
	                }    
            }
            return type;
        }

        static Expression GetExpression(Expression prop, Expression value, Filter filter)
        {
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
                case FilterOperator.EndsWith:
                    MethodInfo endsWith = typeof(String).GetMethod("EndsWith", new Type[] { typeof(String) });
                    return Expression.Call(prop, endsWith, value);
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

        static bool IsNullabelType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        static Expression GetInnerFilter(ICollection<Filter> filters, Expression p, FilterLogic op)
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
                        if(op == FilterLogic.And)
                            filterExpression = Expression.AndAlso(leftSide, rightSide);
                        else
                            filterExpression = Expression.Or(leftSide, rightSide);
                        continue;
                    }
                    if(op == FilterLogic.And)
                        filterExpression = Expression.AndAlso(filterExpression, comparison);
                    else
                        filterExpression = Expression.Or(filterExpression, comparison);
                }
                return filterExpression;
            }
            else
            {
                return null;
            }
        }

        static string GetCollectionPropertyName(Filter filter, Expression item)
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
                        isProp = true;
                        //sb.Append(prop);
                        //sb.Append(".");
                    }
                }
                else
                {
                    sb.Append(prop);
                    if (props.IndexOf(prop) < props.Count() - 1)
                        sb.Append(".");
                }
            }
            return sb.ToString();
        }

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
    }
}
