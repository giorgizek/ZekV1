using System;
using System.Globalization;
using System.Text;

namespace Zek.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// იღებს String-ს ტექსტიდან. თუ ტექსტი == null მაშინ აბრუნებს ""
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IfNullEmpty(this string str)
        {
            return str ?? string.Empty;
        }
        /// <summary>
        /// იღებს String-ს ტექსტიდან. თუ ტექსტი == null ან "" მაშინ აბრუნებს null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IfEmptyNull(this string str)
        {
            return string.IsNullOrEmpty(str) ? null : str;
        }

        /// <summary>
        /// იღებს String-ს ტექსტიდან. თუ ტექსტი == null ან "" მაშინ აბრუნებს null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IfWhiteSpaceNull(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str;
        }


        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string ToDigitOnly(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var result = new StringBuilder();
            foreach (var c in str)
            {
                if (char.IsDigit(c))
                    result.Append(c);
            }

            return result.ToString();
        }

        public static string ToAlphanumeric(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var result = new StringBuilder();
            foreach (var c in str)
            {
                if (char.IsLetterOrDigit(c))
                    result.Append(c);
            }

            return result.ToString();
        }

        public static string[] Split(this string str, string seperator, StringSplitOptions options = StringSplitOptions.None)
        {
            return str.Split(new[] { seperator }, options);
        }

        /*
        /// <summary>
        /// Returns a normalized representation of the specified <paramref name="key"/>
        /// by converting keys to their upper cased invariant culture representation.
        /// </summary>
        /// <param name="key">The key to normalize.</param>
        /// <returns>A normalized representation of the specified <paramref name="key"/>.</returns>
        public static string Normalize(this string key)
        {
            return key?.Normalize().ToUpperInvariant();
        }
        */

        /// <summary>
        /// იღებს ტექსტიდან ნაწილს.
        /// </summary>
        /// <param name="str">ტექსტი.</param>
        /// <param name="startIndex">დაწყების ინდექსი.</param>
        /// <param name="length">სიმბოლოების ზომა.</param>
        /// <returns>აბრუნებს ამოჭრილ ტექსტს</returns>
        public static string SafeSubstring(this string str, int startIndex, int? length = null)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            switch (length)
            {
                case null:
                    length = str.Length - startIndex;
                    break;
                case 0:
                    return string.Empty;
            }

            if (startIndex < 0)
                startIndex = 0;

            return str.Length > startIndex ? (str.Length > startIndex + length ? str.Substring(startIndex, length.Value) : str.Substring(startIndex)) : string.Empty;
        }



        /// <summary>
        /// Returns a string containing a specified number of characters from the left side of a string.
        /// </summary>
        /// <param name="str">Required. String expression from which the leftmost characters are returned</param>
        /// <param name="length">Required. Integer expression. Numeric expression indicating how many characters to return. If 0, a zero-length string ("") is returned. If greater than or equal to the number of characters in str, the entire string is returned.</param>
        /// <returns>Returns a string containing a specified number of characters from the left side of a string.</returns>
        public static string Left(this string str, int length)
        {
            if (string.IsNullOrEmpty(str) || length >= str.Length || length < 0)
                return str;

            return str.Substring(0, length);
        }

        /// <summary>
        /// Returns a string containing a specified number of characters from the right side of a string.
        /// </summary>
        /// <param name="str">Required. String expression from which the rightmost characters are returned.</param>
        /// <param name="length">Required. Integer. Numeric expression indicating how many characters to return. If 0, a zero-length string ("") is returned. If greater than or equal to the number of characters in str, the entire string is returned.</param>
        /// <returns>Returns a string containing a specified number of characters from the right side of a string.</returns>
        public static string Right(this string str, int length)
        {
            if (string.IsNullOrEmpty(str) || length >= str.Length || length < 0)
                return str;

            return str.Substring(str.Length - length);
        }








        /// <summary>
        /// If string not has text returns default text.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        public static string DefaultText(this string str, string defaultText)
        {
            return !string.IsNullOrEmpty(str) ? str : defaultText;
        }

        public static bool Contains(this string str, string value, StringComparison comparisonType)
        {
            return str.IndexOf(value, comparisonType) != -1;
        }

        public static string Add(this string str, string separator, string part)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (!string.IsNullOrEmpty(part))
                    return str + separator + part;
                return str;
            }
            return part;
        }

        public static string AddLine(this string str, string part)
        {
            return Add(str, Environment.NewLine, part);
        }

        public static string[] Lines(this string str)
        {
            return !string.IsNullOrEmpty(str)
                ? str.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                : new string[0];
        }

        public static string Etc(this string str, int max, string etcString = "(...)")
        {
            if (!string.IsNullOrEmpty(str) && (str.Length > max))
                return str.Left(max - (!string.IsNullOrEmpty(etcString) ? etcString.Length : 0)) + etcString;
            return str;
        }

        public static string FirstUpperInvariant(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return char.ToUpperInvariant(str[0]) + str.Substring(1);
        }
        public static string FirstUpper(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return char.ToUpper(str[0]) + str.Substring(1);
        }


        public static string NormalizeKey(this string key)
        {
            return key?.Normalize().ToUpperInvariant();
        }





        /// <summary>
        /// იღებს Bool-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            switch (str.ToUpperInvariant())
            {
                case "TRUE":
                case "Y":
                case "YES":
                case "1":
                case "ON":
                    return true;

                default:
                    return false;
            }
        }
        /// <summary>
        /// იღებს Byte-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(this string str, byte defaultValue = 0)
        {
            //str = str.IfNullEmpty();
            byte result;
            return byte.TryParse(str, out result) ? result : defaultValue;
        }
        /// <summary>
        /// იღებს Int16-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ToInt16(this string str, short defaultValue = 0)
        {
            //str = str.IfNullEmpty();
            short result;
            return short.TryParse(str, out result) ? result : defaultValue;
        }
        /// <summary>
        /// იღებს Int32-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt32(this string str, int defaultValue = 0)
        {
            //str = str.IfNullEmpty();
            int result;
            return int.TryParse(str, out result) ? result : defaultValue;
        }
        /// <summary>
        /// იღებს Int64-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToInt64(this string str, long defaultValue = 0)
        {
            //str = str.IfNullEmpty();
            long result;
            return long.TryParse(str, out result) ? result : defaultValue;
        }
        /// <summary>
        /// იღებს ToDecimal-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defaultValue = 0M)
        {
            //str = str.IfNullEmpty();
            decimal result;
            return decimal.TryParse(str, out result) ? result : defaultValue;
        }
        /// <summary>
        /// იღებს Double-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defaultValue = 0D)
        {
            //str = str.IfNullEmpty();
            double result;
            return double.TryParse(str, out result) ? result : defaultValue;
        }

        /// <summary>
        /// Gets GUID value from string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string str, Guid defaultValue = default(Guid))
        {
            //str = str.IfNullEmpty();
            Guid result;
            return Guid.TryParse(str, out result) ? result : defaultValue;
            //var result = Guid.Empty;
            //try
            //{
            //    result = new Guid(str);
            //}
            //catch { }
            //return result;
        }
        /// <summary>
        ///  Gets DateTime value from string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, DateTime? defaultValue = null)
        {
            if (defaultValue == null)
                defaultValue = DateTime.MinValue;

            DateTime result;
            return DateTime.TryParse(str, out result) ? result : defaultValue.Value;
        }
        public static DateTime ParseUniversalDateTime(this string str, DateTime? defaultValue = null)
        {
            if (defaultValue == null)
                defaultValue = DateTime.MinValue;

            DateTime result;
            return DateTime.TryParseExact(str, DateTimeExtensions.UniversalDateTimeFormat, null, DateTimeStyles.None, out result) ? result : defaultValue.Value;
        }


        public static bool ToBoolean(this string str, string error)
        {
            var result = ToNullableBoolean(str);
            if (result.HasValue) return result.Value;
            throw new FormatException(error);
        }
        public static byte ToByte(this string str, string error)
        {
            byte result;
            if (byte.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static short ToInt16(this string str, string error)
        {
            short result;
            if (short.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static int ToInt32(this string str, string error)
        {
            int result;
            if (int.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static long ToInt64(this string str, string error)
        {
            long result;
            if (long.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static float ToSingle(this string str, string error)
        {
            float result;
            if (float.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static double ToDouble(this string str, string error)
        {
            double result;
            if (double.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static decimal ToDecimal(this string str, string error)
        {
            decimal result;
            if (decimal.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static Guid ToGuid(this string str, string error)
        {
            str = str.IfNullEmpty();
            Guid result;

            if (Guid.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static DateTime ToDateTime(this string str, string error)
        {
            str = str.IfNullEmpty();

            DateTime result;
            if (DateTime.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }



        public static bool? ToNullableBoolean(this string str)
        {
            str = str.IfNullEmpty().ToUpperInvariant();
            switch (str)
            {
                case "TRUE":
                case "YES":
                case "1":
                case "ON":
                    return true;

                case "FALSE":
                case "NO":
                case "0":
                case "OFF":
                    return false;
            }
            return null;
        }
        public static byte? ToNullableByte(this string str)
        {
            byte result;
            return byte.TryParse(str, out result) ? (byte?)result : null;
        }
        public static short? ToNullableInt16(this string str)
        {
            short result;
            return short.TryParse(str, out result) ? (short?)result : null;
        }
        public static int? ToNullableInt32(this string str)
        {
            int result;
            return int.TryParse(str, out result) ? (int?)result : null;
        }
        public static long? ToNullableInt64(this string str)
        {
            long result;
            return long.TryParse(str, out result) ? (long?)result : null;
        }
        public static float? ToNullableSingle(this string str)
        {
            float result;
            return float.TryParse(str, out result) ? (float?)result : null;
        }
        public static double? ToNullableDouble(this string str)
        {
            double result;
            return double.TryParse(str, out result) ? (double?)result : null;
        }
        public static decimal? ToNullableDecimal(this string str)
        {
            decimal result;
            return decimal.TryParse(str, out result) ? (decimal?)result : null;
        }
        public static Guid? ToNullableGuid(this string str)
        {
            Guid result;
            return Guid.TryParse(str, out result) ? (Guid?)result : null;
        }
        public static DateTime? ToNullableDateTime(this string str)
        {
            DateTime result;
            return DateTime.TryParse(str, out result) ? (DateTime?)result : null;
        }


        /*

        /// <summary>
        /// იღებს GroupSymbol=მძიმე და DecimalSymbol-ს Replace-ს უკეთებს წერტილზე.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string PrepareNumber(this string str)
        {
            str = str.Replace(" ", string.Empty).Replace(" ", string.Empty);//Remove chars: 32 and 160
            return str.Contains(",") && !str.Contains(".") ? str.Replace(",", ".") : str;
        }
        /// <summary>
        /// Trying to parse text to decimal in.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ParseDecimal(this string str, decimal defaultValue = 0M)
        {
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;

            decimal result;
            str = str.PrepareNumber();

            // Try parsing using the user's culture
            if (decimal.TryParse(str, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result))
            {
            }
            // Parse using an invariant culture
            else if (decimal.TryParse(str, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out result))
            {
            }
            else
            {
                return defaultValue;
            }

            return result;
        }
        /// <summary>
        /// Trying to parse text to double in.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ParseDouble(this string str, double defaultValue = 0D)
        {
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;

            double result;
            str = str.PrepareNumber();

            // Try parsing using the user's culture
            if (double.TryParse(str, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result))
            {
            }
            // Parse using an invariant culture
            else if (double.TryParse(str, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out result))
            {
            }
            else
            {
                return defaultValue;
            }

            return result;
        }
        /// <summary>
        /// Trying to parse text to float in.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ParseSingle(this string str, float defaultValue = 0F)
        {
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;

            float result;
            str = str.PrepareNumber();
            // Try parsing using the user's culture
            if (float.TryParse(str, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result))
            {
            }
            // Parse using an invariant culture
            else if (float.TryParse(str, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out result))
            {
            }
            else
            {
                return defaultValue;
            }

            return result;
        }

        /// <summary>
        /// Trying to parse text to DateTime in.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(this string str, DateTime defaultValue = new DateTime())
        {
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;

            DateTime result;
            // Try parsing using the user's culture
            if (DateTime.TryParse(str, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result))
            {
            }
            // Parse using an invariant culture
            else if (DateTime.TryParse(str, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out result))
            {
            }
            else
            {
                return defaultValue;
            }

            return result;
        }
        /// <summary>
        /// Trying to parse uiversal text to DateTime in.zzzz
        /// </summary>
        /// <param name="str">yyyy-MM-dd HH:mm:ss.fff, yyyy-MM-dd HH:mm:ss, yyyy-MM-dd HH:mm, yyyy-MM-dd</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ParseUniversalDateTime(this string str, DateTime defaultValue = new DateTime())
        {
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;

            DateTime date;
            if (DateTime.TryParseExact(str, new[] { UniversalDateTimeMillisecondFormat, UniversalDateTimeFormat, "yyyy-MM-dd HH:mm", UniversalDateFormat }, null, DateTimeStyles.None, out date))
                return date;

            return defaultValue;
        }
        /// <summary>
        /// Trying to parse uiversal text to DateTime in.zzzz
        /// </summary>
        /// <param name="str">yyyy-MM-dd HH:mm:ss.fff, yyyy-MM-dd HH:mm:ss, yyyy-MM-dd HH:mm, yyyy-MM-dd</param>
        /// <returns></returns>
        public static DateTime? ParseUniversalNullableDateTime(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;

            DateTime date;
            if (DateTime.TryParseExact(str, new[] { UniversalDateTimeMillisecondFormat, UniversalDateTimeFormat, "yyyy-MM-dd HH:mm", UniversalDateFormat }, null, DateTimeStyles.None, out date))
                return date;

            return null;
        }
        */
    }
}
