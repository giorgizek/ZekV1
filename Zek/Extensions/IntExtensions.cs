using System;
using System.Globalization;

namespace Zek.Extensions
{
    /// <summary>
    /// Class contains extension methods to work with Int type object
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// იღებს ტექსტს და წინ ამატებს შესაბამის რაოდენობის 0-ს
        /// </summary>
        /// <returns></returns>
        public static string PadLeftZero(this int str, int totalWidth)
        {
            
            return str.ToString(CultureInfo.InvariantCulture).PadLeft(totalWidth, '0');
        }


        #region "IsOdd"

        /// <summary>
        /// Returns a Boolean indicating whether this Integer is an Odd number.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsOdd(this int i)
        {
            return i % 2 != 0;
        }

        #endregion

        #region "IsEven"

        /// <summary>
        /// Returns a Boolean indicating whether this Integer is an Even number.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsEven(this int i)
        {
            return i % 2 == 0;
        }

        #endregion

        #region "UpTo"

        /// <summary>
        /// Iterates from the Int through the specified stopAt and calls the specified Action for each passing in the iterator.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="stopAt"></param>
        /// <param name="action"></param>
        public static void UpTo(this int i, int stopAt, Action<int> action)
        {
            for (var a = i; a <= stopAt; a++)
            {
                action(a);
            }
        }

        #endregion

        #region "Between"

        /// <summary>
        /// Returns a Boolean value indicating that the integer is "between" the low and high values specified. Ex: 1 is between 0 and 2, but -1 or 3 are not.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static bool Between(this int i, int low, int high)
        {
            return i >= low && i <= high;
        }
        /// <summary>
        /// Returns a Boolean value indicating that the integer is "not between" the low and high values specified. Ex: -1 or 3 are not between 0 and 2, but 1 is.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static bool NotBetween(this int i, int low, int high)
        {
            return !Between(i, low, high);
        }

        #endregion

        #region Times

        /// <summary>
        /// Executes the specified action the specified number of times, passing in the iterator value each time.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="action"></param>
        public static void Times(this int i, Action<int> action)
        {
            for (var c = 0; c < i; c++)
            {
                action(c);
            }
        }

        #endregion

        #region "TimeSpan extensions"

        /// <summary>
        /// Returns a TimeSpan that represents the integer in milliseconds
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpan Milliseconds(this int i)
        {
            return TimeSpan.FromMilliseconds(i);
        }

        /// <summary>
        /// Returns a TimeSpan that represents the integer in seconds
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpan Seconds(this int i)
        {
            return TimeSpan.FromSeconds(i);
        }

        /// <summary>
        /// Returns a TimeSpan that represents the integer in minutes
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpan Minutes(this int i)
        {
            return TimeSpan.FromMinutes(i);
        }

        /// <summary>
        /// Returns a TimeSpan that represents the integer in hours
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpan Hours(this int i)
        {
            return TimeSpan.FromHours(i);
        }

        /// <summary>
        /// Returns a TimeSpan that represents the integer in days
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpan Days(this int i)
        {
            return TimeSpan.FromDays(i);
        }

        ///// <summary>
        ///// Returns a TimeSpan that represents the integer in weeks
        ///// </summary>
        ///// <param name="i"></param>
        ///// <returns></returns>
        //public static System.TimeSpan Weeks(this int i)
        //{
        //    return ((TimeSpanWrapper)i.Days() * 7);
        //}

        #endregion

        #region Math
        //http://en.wikipedia.org/wiki/Modulo_operation
        public static int Mod(this int a, int b)
        {
            var result = a % b;

            if (a < 0)
                result += b;

            if (b < 0)
                result -= b;

            return result;
        }

        public static long Mod(this long a, long b)
        {
            var mod = a % b;

            if (a < 0)
                mod += b;

            if (b < 0)
                mod -= b;

            return mod;
        }

        public static int DivMod(this int a, int b, out int mod)
        {
            var result = Math.DivRem(a, b, out mod);

            if (a < 0)
            {
                result--;
                mod += b;
            }

            if (b < 0)
            {
                result++;
                mod -= b;
            }

            return result;
        }

        public static long DivMod(this long a, long b, out long mod)
        {
            var result = Math.DivRem(a, b, out mod);

            if (a < 0)
            {
                result--;
                mod += b;
            }

            if (b < 0)
            {
                result++;
                mod -= b;
            }

            return result;
        }
        #endregion

        #region Roman & ABC && Hex
        /// <summary>
        /// Converts Arabic numerals to Roman.
        /// </summary>
        /// <param name="value">Arabic numerals for converting to the Roman format.</param>
        /// <returns>Returns Arabics numerals in Roman.</returns>
        public static string ToRoman(this int value)
        {
            int[] arabics = { 1, 5, 10, 50, 100, 1000 };
            char[] romans = { 'I', 'V', 'X', 'L', 'C', 'M' };
            int[] subs = { 0, 0, 0, 2, 2, 4 };

            var result = string.Empty;
            while (value > 0)
            {
                for (var i = 5; i >= 0; i--)
                {
                    if (value >= arabics[i])
                    {
                        result += romans[i];
                        value -= arabics[i];
                        break;
                    }

                    var flag = false;
                    for (var j = subs[i]; j < i; j++)
                    {
                        if (arabics[j] == arabics[i] - arabics[j]) continue;
                        if (value >= arabics[i] - arabics[j])
                        {
                            result += romans[j];
                            result += romans[i];
                            value -= arabics[i] - arabics[j];
                            flag = true;
                            break;
                        }
                    }
                    if (flag) break;
                }
            }
            return result;
        }
        /// <summary>
        /// Converts the number to A B C D representation for numbering of the list.
        /// </summary>
        /// <param name="value">A number for converting into the A B C representation.</param>
        /// <returns>String representation of the value in A B C D format.</returns>
        public static string ToABC(this int value)
        {
            char[] abc = {
						'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 
						'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
						};
            if (value < 1) return string.Empty;
            var count = 0;
            while (value > 26)
            {
                count++;
                value -= 26;
            }
            return new string(abc[value - 1], count + 1);
        }


        /// <summary>
        /// Converts int to Hex.
        /// </summary>
        /// <param name="value">int value</param>
        /// <returns>Hex converted value</returns>
        public static string ToHex(this int value)
        {
            return value.ToString("X");
        }
        /// <summary>
        /// Converts int to Hex.
        /// </summary>
        /// <param name="value">hex value</param>
        /// <returns>returns converted to int value</returns>
        public static int HexToInt32(this string value)
        {
            return Convert.ToInt32(value, 16);
        }
        #endregion
    }
}