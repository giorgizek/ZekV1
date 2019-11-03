using System;
using System.Text;

namespace Zek.Utils
{
    public class StringHelper
    {
        public static string Replace(string str, string[] from, string[] to)
        {
            if (string.IsNullOrEmpty(str) || from == null || from.Length == 0 || to == null || to.Length == 0)
                return str;

            if (from.Length != to.Length)
                throw new ArgumentException($"'{nameof(from)}' and '{nameof(to)}' params size must be same", nameof(to));

            var sb = new StringBuilder(str);
            for (var i = 0; i < from.Length; i++)
            {
                sb.Replace(from[i], to[i]);
            }

            return sb.ToString();
        }


        /// <summary>
        /// Finds char array count from text.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="searchChars"></param>
        /// <returns></returns>
        public static int FindCount(string str, string searchChars)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            var count = 0;
            foreach (var c1 in str)
            {
                foreach (var c2 in searchChars)
                {
                    if (c1 == c2)
                        count++;
                }
            }

            return count;
        }
    }
}
