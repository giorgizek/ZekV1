using System;
using System.Text;

namespace Zek.Extensions
{
    public static class ByteExtensions
    {
        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToBase64String(this byte[] bytes)
        {
            return bytes != null ? Convert.ToBase64String(bytes) : null;
        }

        /// <summary>
        /// Converts string to UTF-8 bytes array.
        /// </summary>
        /// <param name="str">text</param>
        /// <returns>returns bytes array</returns>
        public static byte[] ToUTF8Array(this string str)
        {
            return str != null ? Encoding.UTF8.GetBytes(str) : null;
        }
        /// <summary>
        /// Converts an UTF-8 bytes array to string.
        /// </summary>
        /// <param name="bytes">UTF-8 bytes array</param>
        /// <returns>returns string</returns>
        public static string UTF8ArrayToString(this byte[] bytes)
        {
            return bytes != null ? Encoding.UTF8.GetString(bytes) : null;
        }
    }
}
