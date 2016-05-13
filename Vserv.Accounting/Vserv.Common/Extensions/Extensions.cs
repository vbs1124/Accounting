using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;

namespace Vserv.Common.Extensions
{
    /// <summary>
    /// Extensions class
    /// </summary>
    public static class Extensions
    {
        #region Integer
        /// <summary>
        /// Converts an object to an int.
        /// </summary>
        /// <param name="item">The object to be converted.</param>
        /// <returns>A converted integer.</returns>
        public static int ToIntValue(this object item)
        {
            return (item != null) ? ToIntValue(item.ToStringValue()) : int.MinValue;
        }

        /// <summary>
        /// Converts a string to an int.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>A converted integer.</returns>
        public static int ToIntValue(this string item)
        {
            int x;
            if (int.TryParse(item, out x))
            {
                return x;
            }
            else
            {
                return int.MinValue;
            }
        }

        #endregion

        #region Double
        /// <summary>
        /// Converts an object to an double.
        /// </summary>
        /// <param name="item">The object to be converted.</param>
        /// <returns>A converted double.</returns>
        public static double ToDoubleValue(this object item)
        {
            return (item != null) ? ToDoubleValue(item.ToStringValue()) : double.MinValue;
        }

        /// <summary>
        /// Converts a string to an double.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>A converted double.</returns>
        public static double ToDoubleValue(this string item)
        {
            double x;
            if (double.TryParse(item, out x))
            {
                return x;
            }
            else
            {
                return double.MinValue;
            }
        }
        #endregion

        #region DateTime
        /// <summary>
        /// Toes the date time value.
        /// </summary>
        /// <param name="item">The datetime item.</param>
        /// <returns>A converted DateTime</returns>
        public static DateTime ToDateTimeValue(this DateTime? item)
        {
            return item.HasValue ? item.Value : DateTime.MinValue;
        }

        /// <summary>
        /// Converts an object to a DateTime.
        /// </summary>
        /// <param name="item">The object to be converted..</param>
        /// <returns>A converted DateTime</returns>
        public static DateTime ToDateTimeValue(this object item)
        {
            return (item != null) ? ToDateTimeValue(item.ToStringValue()) : DateTime.MinValue;
        }

        /// <summary>
        /// Converts an string to a DateTime.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>A converted DateTime</returns>
        public static DateTime ToDateTimeValue(this string item)
        {
            DateTime x;
            if (DateTime.TryParse(item, out x))
            {
                return x;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
        #endregion

        #region String


        public static string IntToStringWithLeftPad(this int number, int totalWidth)
        {
            return number.ToString().PadLeft(totalWidth, '0');
        }


        /// <summary>
        /// Trims a string.
        /// </summary>
        /// <param name="item">The object to be converted.</param>
        /// <returns>A trimmed string.</returns>
        public static string ToStringValue(this object item)
        {
            return (item != null) ? item.ToString().ToStringValue() : string.Empty;
        }

        /// <summary>
        /// Trims a string.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>A trimmed string.</returns>
        public static string ToStringValue(this string item)
        {
            return (item != null) ? item.ToString().Trim() : string.Empty;
        }

        /// <summary>
        /// Toes the pascal case.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>Pascal case string</returns>
        public static string ToPascalCase(this string item)
        {
            System.Globalization.TextInfo ti = new System.Globalization.CultureInfo("en-US", false).TextInfo;
            return ti.ToTitleCase(item.ToStringValue().ToLower());
        }

        /// <summary>
        /// Returns a value indicating whether the specified System.String object occurs within this string.
        /// </summary>
        /// <param name="item">The source.</param>
        /// <param name="value">The string for which to check.</param>
        /// <param name="comparison">The comparison type.</param>
        /// <returns>
        ///     True if this string contains the specified System.String object otherwise, false.
        /// </returns>
        public static bool Contains(this string item, string value, StringComparison comparison)
        {
            return item.IndexOf(value, comparison) >= 0;
        }

        /// <summary>
        /// Replaces the characters in the source string with the supplied replacement string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeCharacters">The characters to be removed.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <returns>
        /// Returns a string in which the required characters have been replaced.
        /// </returns>
        public static string ReplaceCharacters(this string sourceString, string removeCharacters, string replacement)
        {
            StringBuilder cleanString = new StringBuilder(sourceString.Length);

            foreach (char c in sourceString)
            {
                if (removeCharacters.IndexOf(c) < 0)
                {
                    cleanString.Append(c);
                }
                else
                {
                    cleanString.Append(replacement);
                }
            }

            return cleanString.ToString();
        }

        /// <summary>
        /// Truncates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <param name="appendDots">If set to <c>true</c> [append dots].</param>
        /// <returns>Returns a truncated string</returns>
        public static string Truncate(this string value, int length, bool appendDots)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            else if (value.Length > length)
            {
                return value.Substring(0, Math.Min(value.Length, length)) + (appendDots ? "..." : string.Empty);
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Removes the whitespace.
        /// </summary>
        /// <param name="inputString">The input String.</param>
        /// <returns>A System.String</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static String RemoveWhitespace(this String inputString)
        {
            try
            {
                return new Regex(@"\s*").Replace(inputString, String.Empty);
            }
            catch (Exception)
            {
                return inputString;
            }
        }

        /// <summary>
        /// Trims the whitespace.
        /// </summary>
        /// <param name="inputString">The input String.</param>
        /// <returns>A System.String</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static String TrimWhiteSpace(this String inputString)
        {
            return TrimHelper(inputString, true, true);
        }

        /// <summary>
        /// Trims the start whitespace.
        /// </summary>
        /// <param name="inputString">The input String.</param>
        /// <returns>A System.String</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static String TrimStartWhiteSpace(this String inputString)
        {
            return TrimHelper(inputString, true);
        }

        /// <summary>
        /// Trims the end whitespace.
        /// </summary>
        /// <param name="inputString">The input String.</param>
        /// <returns>A System.String</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static String TrimEndWhiteSpace(this String inputString)
        {
            return TrimHelper(inputString, trimEnd: true);
        }

        /// <summary>
        /// Removes extra spaces between words
        /// </summary>
        /// <param name="str"></param>
        /// <returns>A System.String</returns>
        /// <remarks></remarks>
        public static String RemoveExtraSpaces(this String str)
        {
            Regex regulEx = new Regex(@"[\s]+");
            string result = regulEx.Replace(str, " ");
            return result;
        }

        public static String SplitCamelCase(this String str)
        {
            return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        #endregion

        #region Boolean
        /// <summary>
        /// Converts an object to an int.
        /// </summary>
        /// <param name="item">The object to be converted.</param>
        /// <returns>A converted integer.</returns>
        public static bool ToBooleanValue(this object item)
        {
            return (item != null) ? ToBooleanValue(item.ToStringValue()) : false;
        }

        /// <summary>
        /// Converts a string to an int.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>A converted integer.</returns>
        public static bool ToBooleanValue(this string item)
        {
            bool x;
            if (bool.TryParse(item, out x))
            {
                return x;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Guid
        /// <summary>
        /// Converts an object to an Guid.
        /// </summary>
        /// <param name="item">The object to be converted.</param>
        /// <returns>A converted Guid.</returns>
        public static Guid ToGuidValue(this object item)
        {
            return (item != null) ? ToGuidValue(item.ToStringValue()) : Guid.Empty;
        }

        /// <summary>
        /// Converts a string to an Guid.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>A converted Guid.</returns>
        public static Guid ToGuidValue(this string item)
        {
            Guid x;
            if (Guid.TryParse(item, out x))
            {
                return x;
            }
            else
            {
                return Guid.Empty;
            }
        }
        #endregion

        #region Enumerables
        /// <summary>
        /// Picks the random.
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>Random list item</returns>
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        /// <summary>
        /// Picks the random.
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="count">The count.</param>
        /// <returns>Random list of count items</returns>
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        /// <summary>
        /// Shuffles the specified source.
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>Randomized list</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            Random rand = new Random();
            return source.OrderBy(x => rand.Next());
        }

        public static string ToDelimitedString<T>(this IEnumerable<T> values, string delimiter)
        {
            var result = new StringBuilder();
            var insertDelimiter = false;

            foreach (var value in values)
            {
                if (insertDelimiter)
                {
                    result.Append(delimiter);
                }
                else
                {
                    insertDelimiter = true;
                }

                result.Append(value.ToString());
            }

            return result.ToString();
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        #endregion

        #region Roles
        /// <summary>
        /// Determines whether the user is a member of one of the roles.
        /// </summary>
        /// <param name="user">The user to authorize.</param>
        /// <param name="roles">The authorized roles delimited by the specified character.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>
        ///      <c>true</c> if the user is in one of the roles otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInRoles(this IPrincipal user, string roles, char delimiter)
        {
            bool isInRoles = false;
            string[] roleArray;

            if (!String.IsNullOrEmpty(roles))
            {
                roleArray = roles.Split(new char[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string role in roleArray)
                {
                    if (user.IsInRole(role))
                    {
                        isInRoles = true;
                        break;
                    }
                }
            }

            return isInRoles;
        }
        #endregion

        #region Miscellaneous

        /// <summary>
        /// Checks if an object is null
        /// </summary>
        /// <param name="o">Object to check</param>
        /// <returns>True if null, False is not null.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static Boolean IsNull(this object o)
        {
            return ReferenceEquals(o, null);
        }

        /// <summary>
        /// Checks if an object is not null
        /// </summary>
        /// <param name="o">Object to evaluate</param>
        /// <returns>True if NOT null, false if null</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static Boolean IsNotNull(this object o)
        {
            return !ReferenceEquals(o, null);
        }

        /// <summary>
        /// Returns if a collection is null or Empty i.e does not contain any items in it.
        /// </summary>
        /// <param name="item">A collection</param>
        /// <returns>Boolean Value</returns>
        public static Boolean IsNullOrEmptyCollection(this System.Collections.ICollection item)
        {
            if (item != null && item.Count > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Trims the helper.
        /// </summary>
        /// <param name="inputString">The input String.</param>
        /// <param name="trimStart">if set to <c>true</c> [trim start].</param>
        /// <param name="trimEnd">if set to <c>true</c> [trim end].</param>
        /// <returns>A System.String</returns>
        /// <remarks></remarks>
        private static String TrimHelper(this String inputString, Boolean trimStart = false, Boolean trimEnd = false)
        {
            // End will point to the first non-trimmed character on the right
            // Start will point to the first non-trimmed character on the Left
            Int32 end = inputString.Length - 1;
            Int32 start = 0;

            // Trim specified characters. 
            if (trimStart)
            {
                for (start = 0; start < inputString.Length; start++)
                {
                    if (!inputString[start].IsWhiteSpace())
                    {
                        break;
                    }
                }
            }

            if (trimEnd)
            {
                for (end = inputString.Length - 0; end >= start; end--)
                {
                    if (!inputString[end].IsWhiteSpace())
                    {
                        break;
                    }
                }
            }

            Int32 length = end - start + 1;

            return length == inputString.Length
                      ? inputString
                      : (length == 0 ? String.Empty : inputString.Substring(start, length));
        }

        /// <summary>
        /// Determines whether [is white space] [the specified character].
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="isUnicode">if set to <c>true</c> [is unicode].</param>
        /// <returns><c>true</c> if [is white space] [the specified character]; otherwise, <c>false</c>.</returns>
        /// <remarks></remarks>
        private static Boolean IsWhiteSpace(this char character, Boolean isUnicode = false)
        {
            if (isUnicode)
            {
                return false;
            }

            return (character == ' ') || (character >= '\x0000' && character <= '\x001f') || character >= '\x007f'
                   || character == '\x0085';
        }

        #endregion
    }
}
