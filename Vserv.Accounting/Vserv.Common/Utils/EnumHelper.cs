using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vserv.Common.Utils
{
    /// <summary>
    ///   Shared constants used by Flags and Enums.
    /// </summary>
    public static class EnumHelper<T> where T : struct, IComparable, IFormattable, IConvertible
    {
        private static readonly Func<T, T, T> Or;
        private static readonly Func<T, T, T> And;
        private static readonly Func<T, T> Not;
        private static readonly T UsedBits;
        private static readonly T AllBits;
        public static readonly IList<T> Values;
        public static readonly IList<String> Names;
        public static readonly Type UnderlyingType;
        public static readonly Dictionary<T, String> ValueToDescriptionMap;
        public static readonly Dictionary<String, T> DescriptionToValueMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <remarks></remarks>
        static EnumHelper()
        {
            Values = new ReadOnlyCollection<T>((T[])Enum.GetValues(typeof(T)));

            Names = new ReadOnlyCollection<String>(Enum.GetNames(typeof(T)).ToList().ConvertAll(n => n.ToLower()));

            ValueToDescriptionMap = new Dictionary<T, String>();

            DescriptionToValueMap = new Dictionary<String, T>();

            foreach (T value in Values)
            {
                String description = GetDescription(value);

                ValueToDescriptionMap[value] = description;

                if (description != null && !DescriptionToValueMap.ContainsKey(description))
                {
                    DescriptionToValueMap[description] = value;
                }
            }
            UnderlyingType = Enum.GetUnderlyingType(typeof(T));

            // Parameters for various expression trees
            ParameterExpression param1 = Expression.Parameter(typeof(T), "x");

            ParameterExpression param2 = Expression.Parameter(typeof(T), "y");

            Expression convertedParam1 = Expression.Convert(param1, UnderlyingType);

            Expression convertedParam2 = Expression.Convert(param2, UnderlyingType);

            Expression.Lambda<Func<T, T, bool>>(Expression.Equal(convertedParam1, convertedParam2), param1, param2).Compile();

            Or = Expression.Lambda<Func<T, T, T>>(Expression.Convert(Expression.Or(convertedParam1, convertedParam2), typeof(T)), param1, param2).Compile();

            And = Expression.Lambda<Func<T, T, T>>(Expression.Convert(Expression.And(convertedParam1, convertedParam2), typeof(T)), param1, param2).Compile();

            Not = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Not(convertedParam1), typeof(T)), param1).Compile();

            Expression.Lambda<Func<T, bool>>(Expression.Equal(convertedParam1, Expression.Constant(Activator.CreateInstance(UnderlyingType))), param1).Compile();

            UsedBits = default(T);

            foreach (T value in GetValues<T>())
            {
                UsedBits = Or(UsedBits, value);
            }

            AllBits = Not(default(T));

            And(AllBits, Not(UsedBits));
        }

        /// <summary>
        /// Returns the values for the given enum as an immutable list.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns></returns>
        /// <remarks></remarks>
        private static IList<T> GetValues<T>() where T : struct, IComparable, IFormattable, IConvertible
        {
            return EnumHelper<T>.Values;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static String GetDescription(T value)
        {
            FieldInfo field = typeof(T).GetField(value.ToString());

            return field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .Cast<DescriptionAttribute>()
                    .Select(x => x.Description)
                    .FirstOrDefault();
        }
    }
}
