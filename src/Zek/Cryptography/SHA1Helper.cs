using System;
using System.Security.Cryptography;
using System.Text;
using Zek.Utils;
// ReSharper disable InconsistentNaming

namespace Zek.Cryptography
{
    public static class SHA1Helper
    {
        public static string SHA1Hex(string plainText)
        {
            using (var sha1 = SHA1.Create())
            {
                return ByteArrayHelper.ByteArrayToHex(sha1.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
            }
        }
        public static bool VerifySHA1Hex(string cypherText, string plainText)
        {
            var sha1 = SHA1Hex(plainText);
            return cypherText == sha1;
        }



        public static string SHA1Base64(string plainText)
        {
            using (var sha1 = SHA1.Create())
            {
                return Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
            }
        }
        public static bool VerifySHA1Base64(string cypherText, string plainText)
        {
            var sha1 = SHA1Base64(plainText);
            return cypherText == sha1;
        }
    }
}
