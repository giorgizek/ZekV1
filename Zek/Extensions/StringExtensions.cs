using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using Zek.Extensions.Collections;

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
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string ToNumericOnly(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var result = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                {
                    result.Append(str[i]);
                }
            }

            return result.ToString();
        }
        /// <summary>
        /// ამოწმებს მნიშნელობა ნულია თუ არა და უკეთებს Trim-ს.
        /// </summary>
        /// <param name="str">ტექსტი რისი ქლეანინგი გვინდა.</param>
        public static string IfNullEmptyTrim(this string str)
        {
            return str.IfNullEmpty().Trim();
        }

       


        /// <summary>
        /// ტექსტში იღებს 2 და მეტ გამოტოვებებს.
        /// </summary>
        /// <param name="str">ტექსტი სადაც ორი ან მეტი გამოტოვებაა.</param>
        /// <returns>აბრუნებს ტექსტს სადაც 1 გამოტოვებაა.</returns>
        public static string CollapseSpaces(this string str)
        {
            return string.IsNullOrEmpty(str) ? str : Regex.Replace(str, @"\s+", " ");
        }

        /// <summary>
        /// Finds char array count from text.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="searchChars"></param>
        /// <returns></returns>
        public static int FindCount(this string str, string searchChars)
        {
            return string.IsNullOrEmpty(str)
                ? 0
                : str.ToCharArray().Where(searchChars.Contains).Count();
        }

      


        public static string ReplaceAll(this string str, string fromLetters, string toLetters)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(fromLetters) || string.IsNullOrEmpty(toLetters))
                return str;

            for (var i = 0; i < fromLetters.Length; i++)
            {
                str = str.Replace(fromLetters[i], toLetters[i]);
            }

            return str;
        }
        public static string ReplaceAll(this string str, string[] from, string[] to)
        {
            if (string.IsNullOrEmpty(str) || from == null || to == null)
                return str;

            if (from.Length != to.Length)
                throw new ArgumentException(@"'from' length doesn't equals 'to' length.", nameof(to));

            for (var i = 0; i < from.Length; i++)
            {
                str = str.Replace(from[i], to[i]);
            }

            return str;
        }

        public static string[] Split(this string str, string seperator, StringSplitOptions options = StringSplitOptions.None)
        {
            return str.Split(new[] { seperator }, options);
        }





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
            if (length == null)
                length = str.Length - startIndex;
            else if (length == 0)
                return string.Empty;

            if (startIndex < 0)
                startIndex = 0;

            return str.Length > startIndex ? (str.Length > startIndex + length ? str.Substring(startIndex, length.Value) : str.Substring(startIndex)) : string.Empty;
        }


        /// <summary>
        /// Converts string to base 64 string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToBase64String(this string str)
        {
            return string.IsNullOrEmpty(str)
                ? str
                : Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// Converts  base 64 string to normal string.
        /// </summary>
        /// <param name="encoded"></param>
        /// <returns></returns>
        public static string FromBase64String(this string encoded)
        {
            return string.IsNullOrEmpty(encoded)
                ? encoded
                : Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
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
        /// Returns a Boolean indicating whether the String is not NULL, not Empty and consists non white-space characters.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullAndWhiteSpace(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Returns a Boolean indicating whether the String is NULL, Empty or consists only of white-space characters.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Returns a Boolean indicating whether the String is not NULL and Empty.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullAndEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
        /// <summary>
        /// Returns a Boolean indicating whether the String is NULL or Empty.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Extracts all digits from a string.
        /// </summary>
        /// <param name = "value">String containing digits to extract</param>
        /// <returns>
        /// All digits contained within the input string
        /// </returns>
        public static string ExtractDigits(this string value)
        {
            return value.Where(char.IsDigit).Aggregate(new StringBuilder(value.Length), (sb, c) => sb.Append(c)).ToString();
        }

        /// <summary>
        /// Gets the string before the given string parameter.
        /// </summary>
        /// <param name = "value">The default value.</param>
        /// <param name = "x">The given string parameter.</param>
        /// <returns></returns>
        /// <remarks>Unlike GetBetween and GetAfter, this does not Trim the result.</remarks>
        public static string GetBefore(this string value, string x)
        {
            var xPos = value.IndexOf(x, StringComparison.Ordinal);
            return xPos == -1 ? String.Empty : value.Substring(0, xPos);
        }

        /// <summary>
        /// 	Gets the string between the given string parameters.
        /// </summary>
        /// <param name = "value">The source value.</param>
        /// <param name = "x">The left string sentinel.</param>
        /// <param name = "y">The right string sentinel</param>
        /// <returns></returns>
        /// <remarks>Unlike GetBefore, this method trims the result</remarks>
        public static string GetBetween(this string value, string x, string y)
        {
            var xPos = value.IndexOf(x, StringComparison.Ordinal);
            var yPos = value.LastIndexOf(y, StringComparison.Ordinal);

            if (xPos == -1 || xPos == -1)
                return string.Empty;

            var startIndex = xPos + x.Length;
            return startIndex >= yPos ? String.Empty : value.Substring(startIndex, yPos - startIndex).Trim();
        }

        /// <summary>
        /// 	Gets the string after the given string parameter.
        /// </summary>
        /// <param name = "value">The default value.</param>
        /// <param name = "x">The given string parameter.</param>
        /// <returns></returns>
        /// <remarks>Unlike GetBefore, this method trims the result</remarks>
        public static string GetAfter(this string value, string x)
        {
            var xPos = value.LastIndexOf(x, StringComparison.Ordinal);

            if (xPos == -1)
                return string.Empty;

            var startIndex = xPos + x.Length;
            return startIndex >= value.Length ? string.Empty : value.Substring(startIndex).Trim();
        }

        /// <summary>
        /// 	Remove any instance of the given character from the current string.
        /// </summary>
        /// <param name = "value">
        /// 	The input.
        /// </param>
        /// <param name = "removeCharc">
        /// 	The remove char.
        /// </param>
        public static string Remove(this string value, params char[] removeCharc)
        {
            var result = value;
            if (!string.IsNullOrEmpty(result) && removeCharc != null)
                Array.ForEach(removeCharc, c => result = result.Remove(c.ToString(CultureInfo.InvariantCulture)));

            return result;
        }

        /// <summary>
        /// Remove any instance of the given string pattern from the current string.
        /// </summary>
        /// <param name="value">The input.</param>
        /// <param name="strings">The strings.</param>
        /// <returns></returns>
        public static string Remove(this string value, params string[] strings)
        {
            return strings.Aggregate(value, (current, c) => current.Replace(c, string.Empty));
        }

        /// <summary>
        /// Replace all values in string
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="oldValues">List of old values, which must be replaced</param>
        /// <param name="replacePredicate">Function for replacement old values</param>
        /// <returns>Returns new string with the replaced values</returns>
        /// <example>
        /// 	<code language="c#">
        ///         var str = "White Red Blue Green Yellow Black Gray";
        ///         var achromaticColors = new[] {"White", "Black", "Gray"};
        ///         str = str.ReplaceAll(achromaticColors, v => "[" + v + "]");
        /// 	</code>
        /// </example>
        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, Func<string, string> replacePredicate)
        {
            var sbStr = new StringBuilder(value);
            foreach (var oldValue in oldValues)
            {
                var newValue = replacePredicate(oldValue);
                sbStr.Replace(oldValue, newValue);
            }

            return sbStr.ToString();
        }

        /// <summary>
        /// Replace all values in string
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="oldValues">List of old values, which must be replaced</param>
        /// <param name="newValue">New value for all old values</param>
        /// <returns>Returns new string with the replaced values</returns>
        /// <example>
        /// 	<code>
        ///         var str = "White Red Blue Green Yellow Black Gray";
        ///         var achromaticColors = new[] {"White", "Black", "Gray"};
        ///         str = str.ReplaceAll(achromaticColors, "[AchromaticColor]");
        ///         // str == "[AchromaticColor] Red Blue Green Yellow [AchromaticColor] [AchromaticColor]"
        /// 	</code>
        /// </example>
        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, string newValue)
        {
            var sbStr = new StringBuilder(value);
            foreach (var oldValue in oldValues)
                sbStr.Replace(oldValue, newValue);

            return sbStr.ToString();
        }

        /// <summary>
        /// Replace all values in string
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="oldValues">List of old values, which must be replaced</param>
        /// <param name="newValues">List of new values</param>
        /// <returns>Returns new string with the replaced values</returns>
        /// <example>
        /// 	<code>
        ///         var str = "White Red Blue Green Yellow Black Gray";
        ///         var achromaticColors = new[] {"White", "Black", "Gray"};
        ///         var exquisiteColors = new[] {"FloralWhite", "Bistre", "DavyGrey"};
        ///         str = str.ReplaceAll(achromaticColors, exquisiteColors);
        /// 	</code>
        /// </example>
        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, IEnumerable<string> newValues)
        {
            var sb = new StringBuilder(value);
            var newValueEnum = newValues.GetEnumerator();
            foreach (var old in oldValues)
            {
                if (!newValueEnum.MoveNext())
                    throw new ArgumentOutOfRangeException(nameof(newValues), @"newValues sequence is shorter than oldValues sequence");
                sb.Replace(old, newValueEnum.Current);
            }
            if (newValueEnum.MoveNext())
                throw new ArgumentOutOfRangeException(nameof(newValues), @"newValues sequence is longer than oldValues sequence");

            return sb.ToString();
        }

        /// <summary>
        /// 	Ensures that a string starts with a given prefix.
        /// </summary>
        /// <param name = "value">The string value to check.</param>
        /// <param name = "prefix">The prefix value to check for.</param>
        /// <returns>The string value including the prefix</returns>
        /// <example>
        /// 	<code>
        /// 		var extension = "txt";
        /// 		var fileName = string.Concat(file.Name, extension.EnsureStartsWith("."));
        /// 	</code>
        /// </example>
        public static string EnsureStartsWith(this string value, string prefix)
        {
            return value.StartsWith(prefix) ? value : string.Concat(prefix, value);
        }

        /// <summary>
        /// 	Ensures that a string ends with a given suffix.
        /// </summary>
        /// <param name = "value">The string value to check.</param>
        /// <param name = "suffix">The suffix value to check for.</param>
        /// <returns>The string value including the suffix</returns>
        /// <example>
        /// 	<code>
        /// 		var url = "http://www.pgk.de";
        /// 		url = url.EnsureEndsWith("/"));
        /// 	</code>
        /// </example>
        public static string EnsureEndsWith(this string value, string suffix)
        {
            return value.EndsWith(suffix) ? value : string.Concat(value, suffix);
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

        /// <summary>
        /// If string not has text throws error.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static string AssertHasText(this string str, string errorMessage)
        {
            if (!string.IsNullOrEmpty(str))
                return str;
            throw new ArgumentException(errorMessage);
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

        public static string PadChopRight(this string str, int length)
        {
            if (str == null) return null;
            return str.Length > length
                    ? str.Substring(0, length)
                    : str.PadRight(length);
        }

        public static string PadChopLeft(this string str, int length)
        {
            if (str == null) return null;
            return str.Length > length
                    ? str.Substring(str.Length - length, length)
                    : str.PadLeft(length);
        }
  

        public static string VerticalEtc(this string str, int maxLines, string etcString = "(...)")
        {
            if (string.IsNullOrEmpty(str) || !str.Contains(Environment.NewLine)) return str;

            var arr = str.Split(new[] { Environment.NewLine }, maxLines, StringSplitOptions.None);
            var res = arr.ToString(Environment.NewLine);
            if (res.Length < str.Length)
                res += etcString;
            str = res;
            return str;
        }

        public static string Etc(this string str, int max, string etcString = "(...)")
        {
            if (!string.IsNullOrEmpty(str) && (str.Length > max))
                return str.Left(max - (!string.IsNullOrEmpty(etcString) ? etcString.Length : 0)) + etcString;
            return str;
        }


        public static string EtcLines(this string str, int max, string etcString = "(...)")
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var pos = str.IndexOfAny(new[] { '\n', '\r' });
            if (pos != -1 && pos + etcString.Length < max)
                max = pos + etcString.Length;

            if (str.Length > max)
                return str.Left(max - (!string.IsNullOrEmpty(etcString) ? etcString.Length : 0)) + etcString;
            return str;
        }

        public static bool ContinuesWith(this string str, string subString, int pos)
        {
            return str.IndexOf(subString, pos, StringComparison.Ordinal) == pos;
        }

        public static string RemoveChars(this string str, params char[] chars)
        {
            var sb = new StringBuilder(str.Length);
            for (var i = 0; i < str.Length; i++)
            {
                if (!chars.Contains(str[i]))
                    sb.Append(str[i]);
            }
            return sb.ToString();
        }

        public static string Replace(this string str, Dictionary<string, string> replacements)
        {
            return replacements.Aggregate(str, (s, kvp) => s.Replace(kvp.Key, kvp.Value));
        }

        //public static string ToFormatted(string format, object arg0)
        //{
        //    return string.Format(format, arg0);
        //}
        //public static string ToFormatted(string format, object arg0, object arg1)
        //{
        //    return string.Format(format, arg0, arg1);
        //}
        //public static string ToFormatted(string format, object arg0, object arg1, object arg2)
        //{
        //    return string.Format(format, arg0, arg1, arg2);
        //}
        //public static string ToFormatted(this string pattern, params object[] parameters)
        //{
        //    return string.Format(pattern, parameters);
        //}
        //public static string ToFormatted(this string format, IFormatProvider provider, params object[] args)
        //{
        //    return string.Format(provider, format, args);
        //}

        public static string FormatWith(this string instance, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, instance, args);
        }


        public static string Indent(this string str, int numChars)
        {
            return Indent(str, numChars, ' ');
        }

        public static string Indent(this string str, int numChars, char indentChar)
        {
            var space = new string(indentChar, numChars);
            var sb = new StringBuilder();
            using (var sr = new StringReader(str))
            {
                for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
                {
                    sb.Append(space);
                    sb.AppendLine(line);
                }
            }

            var result = sb.ToString(0, str.EndsWith(Environment.NewLine) ? sb.Length : Math.Max(sb.Length - 2, 0));

            return result;
        }

        public static string NiceName(this string memberName)
        {
            return memberName.Contains('_') ? memberName.Replace('_', ' ') : memberName.SpacePascal();
        }

        public static string SpacePascal(this string pascalStr)
        {
            return SpacePascal(pascalStr, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en");
        }

        public static string SpacePascal(this string pascalStr, bool preserveUppercase)
        {

            var sb = new StringBuilder();
            if (pascalStr.Length > 0)
                sb.Append(pascalStr[0]);

            for (var i = 1; i < pascalStr.Length; i++)
            {
                var a = pascalStr[i];
                if (char.IsUpper(a) && !char.IsUpper(pascalStr[i - 1]))
                {
                    if (sb.Length > 0)
                        sb.Append(' ');
                    if (preserveUppercase || i == 0 || (i < pascalStr.Length - 1 && char.IsUpper(pascalStr[i + 1])))
                        sb.Append(a);
                    else
                        sb.Append(char.ToLower(a));
                }
                else
                    sb.Append(a);
            }

            return sb.ToString();
        }


        public static string FirstUpperInvariant(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;
            return char.ToUpperInvariant(str[0]) + str.Substring(1);
        }
        public static string FirstUpper(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string Replicate(this string str, int times)
        {
            if (string.IsNullOrEmpty(str)) return str;

            if (times < 0)
                throw new ArgumentException("times");

            var sb = new StringBuilder(str.Length * times);
            for (var i = 0; i < times; i++)
                sb.Append(str);
            return sb.ToString();
        }

        public static string Reverse(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            var arr = new char[str.Length];
            var len = str.Length;
            for (var i = 0; i < len; i++)
                arr[i] = str[len - 1 - i];
            return new string(arr);
        }

        public static bool Like(this string str, string pattern)
        {
            if (string.IsNullOrEmpty(str)) return false;

            pattern = Regex.Escape(pattern);
            pattern = pattern.Replace("%", ".*").Replace("_", ".");
            pattern = pattern.Replace(@"\[", "[").Replace(@"\]", "]").Replace(@"\^", "^");
            return Regex.IsMatch(str, pattern);
        }

        public static string RemoveDiacritics(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var normalizedString = str.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            for (var i = 0; i < normalizedString.Length; i++)
            {
                var c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string Combine(this string separator, params object[] elements)
        {
            var sb = new StringBuilder();
            foreach (var item in elements)
            {
                if (item != null)
                {
                    sb.Append(item);
                    sb.Append(separator);
                }
            }

            return sb.ToString(0, Math.Max(0, sb.Length - separator.Length));  // Remove at the end is faster
        }

        public static string CombineIfNotEmpty(this string separator, params object[] elements)
        {
            var sb = new StringBuilder();
            foreach (var item in elements)
            {
                if (string.IsNullOrEmpty(item?.ToString())) continue;
                sb.Append(item);
                sb.Append(separator);
            }

            return sb.ToString(0, Math.Max(0, sb.Length - separator.Length));  // Remove at the end is faster
        }

        public static StringBuilder AppendLines(this StringBuilder sb, IEnumerable<string> strings)
        {
            foreach (var item in strings)
            {
                sb.AppendLine(item);
            }

            return sb;
        }

        public static int CountRepetitions(this string text, string part)
        {
            var result = 0;
            var index = 0;
            while (true)
            {
                index = text.IndexOf(part, index, StringComparison.Ordinal);
                if (index == -1)
                    return result;

                index += part.Length;
                result++;
            }
        }
    }
}
