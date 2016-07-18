#region Extensions

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using Vserv.Common.Utils;

#endregion

namespace Vserv.Accounting.Common
{
    /// <summary>
    /// Extensions class
    /// </summary>
    public static class Extensions
    {
        #region Variables

        // Change the following keys to ensure uniqueness
        // Must be 8 bytes
        public static Byte[] KeyBytes = { 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18 };

        // Must be at least 8 characters
        public static String KeyString = "ABC12345";

        // Name for checksum value (unlikely to be used as arguments by user)
        public static String ChecksumKey = "__$$";

        #endregion

        #region Integer
        /// <summary>
        /// Converts an object to an int.
        /// </summary>
        /// <param name="item">The object to be converted.</param>
        /// <returns>
        /// A converted integer.
        /// </returns>
        public static int ToIntValue(this object item)
        {
            return (item != null) ? ToIntValue(item.ToStringValue()) : int.MinValue;
        }

        /// <summary>
        /// Converts a string to an int.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>
        /// A converted integer.
        /// </returns>
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
        /// <returns>
        /// A converted double.
        /// </returns>
        public static double ToDoubleValue(this object item)
        {
            return (item != null) ? ToDoubleValue(item.ToStringValue()) : double.MinValue;
        }

        /// <summary>
        /// Converts a string to an double.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>
        /// A converted double.
        /// </returns>
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
        /// <returns>
        /// A converted DateTime
        /// </returns>
        public static DateTime ToDateTimeValue(this DateTime? item)
        {
            return item ?? DateTime.MinValue;
        }

        /// <summary>
        /// Converts an object to a DateTime.
        /// </summary>
        /// <param name="item">The object to be converted..</param>
        /// <returns>
        /// A converted DateTime
        /// </returns>
        public static DateTime ToDateTimeValue(this object item)
        {
            return (item != null) ? ToDateTimeValue(item.ToStringValue()) : DateTime.MinValue;
        }

        /// <summary>
        /// Converts an string to a DateTime.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>
        /// A converted DateTime
        /// </returns>
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

        public static Dictionary<int, int> GetFinancialYearMonths(this DateTime inputDate)
        {
            Dictionary<int, int> monthsInfo = new Dictionary<int, int>();
            var currentMonthId = inputDate.Month;
            var currentYear = inputDate.Year;
            var nextYear = currentMonthId >= 4 ? currentYear + 1 : currentYear;
            DateTime end = new DateTime(nextYear, 4, 1);
            var diffMonths = (end.Month + end.Year * 12) - (inputDate.Month + inputDate.Year * 12);

            for (int i = 0; i < diffMonths; i++)
            {
                var cmonth = currentMonthId + i;
                var cyear = cmonth > 12 ? nextYear : currentYear;
                cmonth = cmonth > 12 ? cmonth % 12 : cmonth;

                monthsInfo.Add(cmonth, cyear);
            }

            return monthsInfo;
        }

        #endregion

        #region String

        /// <summary>
        /// Ints to string with left pad.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="totalWidth">The total width.</param>
        /// <returns></returns>
        public static string IntToStringWithLeftPad(this int number, int totalWidth)
        {
            return number.ToString().PadLeft(totalWidth, '0');
        }

        /// <summary>
        /// Trims a string.
        /// </summary>
        /// <param name="item">The object to be converted.</param>
        /// <returns>
        /// A trimmed string.
        /// </returns>
        public static string ToStringValue(this object item)
        {
            return (item != null) ? item.ToString().ToStringValue() : string.Empty;
        }

        /// <summary>
        /// Trims a string.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>
        /// A trimmed string.
        /// </returns>
        public static string ToStringValue(this string item)
        {
            return (item != null) ? item.Trim() : string.Empty;
        }

        /// <summary>
        /// Toes the pascal case.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>
        /// Pascal case string
        /// </returns>
        public static string ToPascalCase(this string item)
        {
            TextInfo ti = new CultureInfo("en-US", false).TextInfo;
            return ti.ToTitleCase(item.ToStringValue().ToLower());
        }

        /// <summary>
        /// Returns a value indicating whether the specified System.String object occurs within this string.
        /// </summary>
        /// <param name="item">The source.</param>
        /// <param name="value">The string for which to check.</param>
        /// <param name="comparison">The comparison type.</param>
        /// <returns>
        /// True if this string contains the specified System.String object otherwise, false.
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
        /// <returns>
        /// Returns a truncated string
        /// </returns>
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
        /// <returns>
        /// A System.String
        /// </returns>
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
        /// <returns>
        /// A System.String
        /// </returns>
        [DebuggerStepThrough]
        public static String TrimWhiteSpace(this String inputString)
        {
            return TrimHelper(inputString, true, true);
        }

        /// <summary>
        /// Trims the start whitespace.
        /// </summary>
        /// <param name="inputString">The input String.</param>
        /// <returns>
        /// A System.String
        /// </returns>
        [DebuggerStepThrough]
        public static String TrimStartWhiteSpace(this String inputString)
        {
            return TrimHelper(inputString, true);
        }

        /// <summary>
        /// Trims the end whitespace.
        /// </summary>
        /// <param name="inputString">The input String.</param>
        /// <returns>
        /// A System.String
        /// </returns>
        [DebuggerStepThrough]
        public static String TrimEndWhiteSpace(this String inputString)
        {
            return TrimHelper(inputString, trimEnd: true);
        }

        /// <summary>
        /// Removes extra spaces between words
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>
        /// A System.String
        /// </returns>
        public static String RemoveExtraSpaces(this String str)
        {
            Regex regulEx = new Regex(@"[\s]+");
            string result = regulEx.Replace(str, " ");
            return result;
        }

        /// <summary>
        /// Splits the camel case.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static String SplitCamelCase(this String str)
        {
            return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        public static String ToEncryptedString(this String inputString)
        {
            return EncryptString(inputString);
        }

        public static String ToDecryptedString(this String inputString)
        {
            return DecryptString(inputString);
        }

        public static String ToEncryptedString(this int value)
        {
            return EncryptString(Convert.ToString(value));
        }

        public static int ToDecryptedInt(this String value)
        {
            var result = DecryptString(value);
            return String.IsNullOrWhiteSpace(result) ? 0 : Convert.ToInt32(result);
        }

        /// <summary>
        /// Converts a Byte array to a String of hex characters
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static String GetString(Byte[] data)
        {
            StringBuilder results = new StringBuilder();

            foreach (Byte b in data)
            {
                results.Append(b.ToString("X2"));
            }

            return results.ToString();
        }

        /// <summary>
        /// Converts a String of hex characters to a Byte array
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static Byte[] GetBytes(String data)
        {
            // GetString() encodes the hex-numbers with two digits
            Byte[] results = new Byte[data.Length / 2];

            for (Int32 count = 0; count < data.Length; count += 2)
                results[count / 2] = Convert.ToByte(data.Substring(count, 2), 16);

            return results;
        }

        private static string EncryptString(String inputString)
        {
            try
            {
                Byte[] keyData = Encoding.UTF8.GetBytes(KeyString.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] textData = Encoding.UTF8.GetBytes(inputString);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateEncryptor(keyData, KeyBytes), CryptoStreamMode.Write);
                cs.Write(textData, 0, textData.Length);
                cs.FlushFinalBlock();

                return GetString(ms.ToArray());
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        private static string DecryptString(String inputString)
        {
            try
            {
                Byte[] keyData = Encoding.UTF8.GetBytes(KeyString.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] textData = GetBytes(inputString);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateDecryptor(keyData, KeyBytes), CryptoStreamMode.Write);
                cs.Write(textData, 0, textData.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        #endregion

        #region Boolean
        /// <summary>
        /// Converts an object to an int.
        /// </summary>
        /// <param name="item">The object to be converted.</param>
        /// <returns>
        /// A converted integer.
        /// </returns>
        public static bool ToBooleanValue(this object item)
        {
            return (item != null) && ToBooleanValue(item.ToStringValue());
        }

        /// <summary>
        /// Converts a string to an int.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>
        /// A converted integer.
        /// </returns>
        public static bool ToBooleanValue(this string item)
        {
            bool x;
            return bool.TryParse(item, out x) && x;
        }

        #endregion

        #region Guid
        /// <summary>
        /// Converts an object to an Guid.
        /// </summary>
        /// <param name="item">The object to be converted.</param>
        /// <returns>
        /// A converted Guid.
        /// </returns>
        public static Guid ToGuidValue(this object item)
        {
            return (item != null) ? ToGuidValue(item.ToStringValue()) : Guid.Empty;
        }

        /// <summary>
        /// Converts a string to an Guid.
        /// </summary>
        /// <param name="item">The string to be converted.</param>
        /// <returns>
        /// A converted Guid.
        /// </returns>
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
        /// <returns>
        /// Randomized list
        /// </returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            Random rand = new Random();
            return source.OrderBy(x => rand.Next());
        }
        /// <summary>
        /// To the delimited string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The values.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }
        /// <summary>
        /// To the hash set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }
        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
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
        ///   <c>true</c> if the user is in one of the roles otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInRoles(this IPrincipal user, string roles, char delimiter)
        {
            bool isInRoles = false;

            if (String.IsNullOrEmpty(roles)) return false;
            var roleArray = roles.Split(separator: new[] { delimiter }, options: StringSplitOptions.RemoveEmptyEntries);

            if (roleArray.Any(user.IsInRole))
            {
                isInRoles = true;
            }

            return isInRoles;
        }
        #endregion

        #region Encrypt or Decrypt String

        /// <summary>
        /// This is an extension method for decrypting the String.
        /// </summary>
        /// <param name="sourceDictionary">Source dictionary value.</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static void ToDecryptedString(this Dictionary<String, String> sourceDictionary, String args)
        {
            EncryptedString encryptedString = new EncryptedString(args);

            foreach (KeyValuePair<String, String> kValue in encryptedString)
            {
                sourceDictionary.Add(kValue.Key, kValue.Value);
            }
        }

        /// <summary>
        /// This is an extension method for EncryptedString which returns the encrypted String for String type dictionary.
        /// </summary>
        /// <param name="sourceDictionary">Source dictionary value.</param>
        /// <returns></returns>
        public static String ToEncryptedString(this Dictionary<String, String> sourceDictionary)
        {
            EncryptedString encryptedString = new EncryptedString(sourceDictionary);
            return encryptedString.ToString();
        }

        #endregion

        #region Miscellaneous

        /// <summary>
        /// Checks if an object is null
        /// </summary>
        /// <param name="o">Object to check</param>
        /// <returns>
        /// True if null, False is not null.
        /// </returns>
        [DebuggerStepThrough]
        public static Boolean IsNull(this object o)
        {
            return ReferenceEquals(o, null);
        }

        /// <summary>
        /// Checks if an object is not null
        /// </summary>
        /// <param name="o">Object to evaluate</param>
        /// <returns>
        /// True if NOT null, false if null
        /// </returns>
        [DebuggerStepThrough]
        public static Boolean IsNotNull(this object o)
        {
            return !ReferenceEquals(o, null);
        }

        /// <summary>
        /// Trims the helper.
        /// </summary>
        /// <param name="inputString">The input String.</param>
        /// <param name="trimStart">if set to <c>true</c> [trim start].</param>
        /// <param name="trimEnd">if set to <c>true</c> [trim end].</param>
        /// <returns>
        /// A System.String
        /// </returns>
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
        /// <returns>
        ///   <c>true</c> if [is white space] [the specified character]; otherwise, <c>false</c>.
        /// </returns>
        private static Boolean IsWhiteSpace(this char character, Boolean isUnicode = false)
        {
            if (isUnicode)
            {
                return false;
            }

            return (character == ' ') || (character >= '\x0000' && character <= '\x001f') || character >= '\x007f'
                   || character == '\x0085';
        }

        public static String GetValue(this Dictionary<String, String> list, String fieldName)
        {
            String strValue;
            list.TryGetValue(fieldName, out strValue);
            return strValue;
        }

        /// <summary>
        /// Converts an object of to type T
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="obj">object to convert</param>
        /// <returns>A T</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static T To<T>(this object obj)
        {
            Type t = typeof(T);

            return t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(Nullable<>))
                      ? (obj.IsNull() ? (T)(object)null : (T)Convert.ChangeType(obj, Nullable.GetUnderlyingType(t)))
                      : (obj == DBNull.Value
                            ? (T)((object)default(T) ?? String.Empty)
                            : (obj is IConvertible) ? (T)Convert.ChangeType(obj, t) : (T)obj);
        }

        /// <summary>
        /// Returns if a collection is null or Empty i.e does not contain any items in it.
        /// </summary>
        /// <param name="item">A collection</param>
        /// <returns>Boolean Value</returns>
        public static Boolean IsNullOrEmptyCollection(this ICollection item)
        {
            if (item != null && item.Count > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns the description for the given value,
        /// as specified by DescriptionAttribute, or null
        /// if no description is present.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="item">Value to fetch description for</param>
        /// <returns>The description of the value, or null if no description
        /// has been specified (but the value is a named value).</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="item"/>
        /// is not a named member of the enum
        ///   </exception>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static String GetDescription<T>(this T item) where T : struct, IComparable, IFormattable, IConvertible
        {
            String description;

            if (!EnumHelper<T>.ValueToDescriptionMap.TryGetValue(item, out description))
            {
                throw new ArgumentOutOfRangeException("item");
            }

            return description;
        }

        /// <summary>
        /// Returns the description for the given value,
        /// as specified by DescriptionAttribute, or value
        /// if no description is present.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="item">Value to fetch description for</param>
        /// <returns>The description of the value, or value if no description
        /// has been specified (but the value is a named value).</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <paramref name="item"/>
        /// is not a named member of the enum
        ///   </exception>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static String ToDescription<T>(this T item) where T : struct, IComparable, IFormattable, IConvertible
        {
            String description;

            return EnumHelper<T>.ValueToDescriptionMap.TryGetValue(item, out description)
                      ? (description ?? item.ToString(CultureInfo.InvariantCulture))
                      : item.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Attempts to find a value with the given description.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="description">Description to find</param>
        /// <param name="value">Enum value corresponding to given description (on return)</param>
        /// <returns>True if a value with the given description was found,
        /// false otherwise.</returns>
        /// <remarks>More than one value may have the same description. In this unlikely
        /// situation, the first value with the specified description is returned.</remarks>
        [DebuggerStepThrough]
        public static Boolean TryParseDescription<T>(String description, out T value)
           where T : struct, IComparable, IFormattable, IConvertible
        {
            if (description.IsNullOrEmpty())
            {
                throw new ArgumentException("Description cannot be null or empty.");
            }

            return EnumHelper<T>.DescriptionToValueMap.TryGetValue(description, out value);
        }

        /// <summary>
        /// Parses the name of an enum value.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="@enum">The enum.</param>
        /// <param name="enum"></param>
        /// <param name="name">The name.</param>
        /// <returns>The parsed value</returns>
        /// <exception cref="ArgumentException">
        /// The name could not be parsed.
        ///   </exception>
        /// <remarks>This method only considers named values: it does not parse comma-separated
        /// combinations of flags enums.</remarks>
        [DebuggerStepThrough]
        public static T ParseName<T>(this T @enum, String name) where T : struct, IComparable, IFormattable, IConvertible
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("name");
            }

            T value;

            if (!TryParseName(name, out value))
            {
                throw new ArgumentException("Unknown name", name);
            }

            return value;
        }

        /// <summary>
        /// Parses the name of an enum value.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>The parsed value</returns>
        /// <exception cref="ArgumentException">
        /// The name could not be parsed.
        ///   </exception>
        /// <remarks>This method only considers named values: it does not parse comma-separated
        /// combinations of flags enums.</remarks>
        [DebuggerStepThrough]
        public static T ParseName<T>(String name) where T : struct, IComparable, IFormattable, IConvertible
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("name");
            }

            T value;

            if (!TryParseName(name, out value))
            {
                throw new ArgumentException("Unknown name", name);
            }

            return value;
        }

        /// <summary>
        /// Parses the name of an enum value.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>The parsed value</returns>
        /// <exception cref="ArgumentException">
        /// The name could not be parsed.
        ///   </exception>
        /// <remarks>This method only considers named values: it does not parse comma-separated
        /// combinations of flags enums.</remarks>
        [DebuggerStepThrough]
        public static T ParseNameOrDefault<T>(String name) where T : struct, IComparable, IFormattable, IConvertible
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("name");
            }

            T value;

            return !TryParseName(name, out value) ? default(T) : value;
        }

        /// <summary>
        /// Attempts to find a value for the specified name.
        /// Only names are considered - not numeric values.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="name">Name to parse</param>
        /// <param name="value">Enum value corresponding to given name (on return)</param>
        /// <returns>Whether the parse attempt was successful or not</returns>
        /// <remarks>If the name is not parsed, <paramref name="value"/> will
        /// be set to the zero value of the enum. This method only
        /// considers named values: it does not parse comma-separated
        /// combinations of flags enums.</remarks>
        [DebuggerStepThrough]
        public static Boolean TryParseName<T>(String name, out T value)
           where T : struct, IComparable, IFormattable, IConvertible
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("name");
            }

            Int32 index = EnumHelper<T>.Names.IndexOf(name.ToLower(CultureInfo.CurrentCulture));

            if (index == -1)
            {
                value = default(T);
                return false;
            }

            value = EnumHelper<T>.Values[index];

            return true;
        }

        /// <summary>
        /// Verifies whether a passed String is a valid Enum of type T
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>The try parse name.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static Boolean TryParseName<T>(this String name) where T : struct, IComparable, IFormattable, IConvertible
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("name");
            }

            Int32 index = EnumHelper<T>.Names.IndexOf(name.ToLower(CultureInfo.CurrentCulture));

            return index != -1;
        }

        /// <summary>
        /// Returns the underlying type for the enum
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>The underlying type (Byte, Int32 etc) for the enum</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static Type GetUnderlyingType<T>() where T : struct, IComparable, IFormattable, IConvertible
        {
            return EnumHelper<T>.UnderlyingType;
        }

        /// <summary>
        /// Check String for empty and null.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns><c>true</c> if [is null or empty] [the specified obj]; otherwise, <c>false</c>.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static Boolean IsNullOrEmpty(this object obj)
        {
            return obj is String ? ((String)obj).IsNullOrEmpty() : obj.IsNull();
        }

        /// <summary>
        /// Check String for empty and null.
        /// </summary>
        /// <param name="stringObject">The String to check.</param>
        /// <returns>True if null or empty, otherwise False</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static Boolean IsNullOrEmpty(this String stringObject)
        {
            return String.IsNullOrWhiteSpace(stringObject);
        }

        /// <summary>
        /// Check is null or white space
        /// </summary>
        /// <param name="stringObject"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Boolean IsNullOrWhiteSpace(this String stringObject)
        {
            return String.IsNullOrWhiteSpace(stringObject);
        }


        /// <summary>
        /// Clones a List collection of ICloneable objects
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="listToClone">The List collection to clone</param>
        /// <returns>A List&lt;T&gt;</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static List<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.IsNull() ? null : listToClone.Select(item => (T)((ICloneable)item).Clone()).ToList();
        }

        /// <summary>
        /// Clones the specified dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns>A Dictionary&lt;TKey,TValue&gt;</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static IDictionary<TKey, TValue> Clone<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary.IsNullOrEmpty())
            {
                return new Dictionary<TKey, TValue>();
            }

            var clone = new Dictionary<TKey, TValue>();

            Boolean keyIsCloneable = default(TKey) is ICloneable;

            Boolean valueIsCloneable = default(TValue) is ICloneable;

            foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
            {
                TKey key = keyIsCloneable ? (TKey)((ICloneable)keyValuePair.Key).Clone() : keyValuePair.Key;
                TValue value = valueIsCloneable ? (TValue)((ICloneable)keyValuePair.Value).Clone() : keyValuePair.Value;

                clone.Add(key, value);
            }

            return clone;
        }

        /// <summary>
        /// Deep Clones an Object
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="obj">Actual Object</param>
        /// <returns>Cloned Object</returns>
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(this T source)
        {
            var dcs = new DataContractSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                dcs.WriteObject(ms, source);
                ms.Seek(0, SeekOrigin.Begin);
                return (T)dcs.ReadObject(ms);
            }
        }

        #endregion
    }
}
