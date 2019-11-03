using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Zek.Security
{

    public class CryptoHelper
    {
        public static byte[] CreateSaltBytes(int size = 32)
        {
            var buffer = new byte[size];
            new RNGCryptoServiceProvider().GetBytes(buffer);
            return buffer;
        }
        public static string CreateSaltString(int size = 32)
        {
            var buffer = new byte[size];
            new RNGCryptoServiceProvider().GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }
        public static void ClearBytes(byte[] buffer)
        {
            // Check arguments.
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            // Set each byte in the buffer to 0.
            for (var i = 0; i <= buffer.Length - 1; i++)
            {
                buffer[i] = 0;
            }
        }




        public static string SHA1Base64(string plainText, string salt = null)
        {
            return SHA1Base64(Encoding.UTF8.GetBytes(plainText + (salt ?? string.Empty)));
        }
        public static string SHA1Base64(byte[] buffer)
        {
            using (var provider = new SHA1Managed())
            {
                return Convert.ToBase64String(provider.ComputeHash(buffer));
            }
        }
        public static bool VerifySHA1Base64(string cypherText, string plainText, string salt = null)
        {
            return cypherText == SHA1Base64(plainText, salt);
        }


        public static string SHA256Base64(string plainText, string salt = null)
        {
            return SHA256Base64(Encoding.UTF8.GetBytes(plainText + (salt ?? string.Empty)));
        }
        public static string SHA256Base64(byte[] buffer)
        {
            using (var provider = new SHA256Managed())
            {
                return Convert.ToBase64String(provider.ComputeHash(buffer));
            }
        }
        public static bool VerifySHA256Base64(string cypherText, string plainText, string salt = null)
        {
            return cypherText == SHA256Base64(plainText, salt);
        }

        public static string SHA384Base64(string plainText, string salt = null)
        {
            return SHA384Base64(Encoding.UTF8.GetBytes(plainText + (salt ?? string.Empty)));
        }
        public static string SHA384Base64(byte[] buffer)
        {
            using (var provider = new SHA384Managed())
            {
                return Convert.ToBase64String(provider.ComputeHash(buffer));
            }
        }
        public static bool VerifySHA384Base64(string cypherText, string plainText, string salt = null)
        {
            return cypherText == SHA384Base64(plainText, salt);
        }


        public static string SHA512Base64(string plainText, string salt = null)
        {
            return SHA512Base64(Encoding.UTF8.GetBytes(plainText + (salt ?? string.Empty)));
        }
        public static string SHA512Base64(byte[] buffer)
        {
            using (var provider = new SHA512Managed())
            {
                return Convert.ToBase64String(provider.ComputeHash(buffer));
            }
        }
        public static bool VerifySHA512Base64(string cypherText, string plainText, string salt = null)
        {
            return cypherText == SHA512Base64(plainText, salt);
        }

        public static string MD5Base64(string plainText, string salt = null)
        {
            return MD5Base64(Encoding.UTF8.GetBytes(plainText + (salt ?? string.Empty)));
        }
        public static string MD5Base64(byte[] buffer)
        {
            using (var provider = new MD5CryptoServiceProvider())
            {
                return Convert.ToBase64String(provider.ComputeHash(buffer));
            }
        }
        public static bool VerifyMD5Base64(string cypherText, string plainText, string salt = null)
        {
            return cypherText == MD5Base64(plainText, salt);
        }


        #region Hex & Sum
        public static string SHA1Sum(string path)
        {
            return SHA1Hex(File.ReadAllBytes(path));
        }
        public static string SHA1Hex(string plainText, string salt = null)
        {
            return SHA1Hex(Encoding.UTF8.GetBytes(plainText + (salt ?? string.Empty)));
        }
        public static string SHA1Hex(byte[] buffer)
        {
            using (var provider = new SHA1Managed())
            {
                return ByteArrayToHex(provider.ComputeHash(buffer));
            }
        }
        public static bool VerifySHA1Hex(string cypherText, string plainText, string salt = null)
        {
            return cypherText == SHA1Hex(plainText, salt);
        }


        public static string SHA256Sum(string path)
        {
            return SHA256Hex(File.ReadAllBytes(path));
        }
        public static string SHA256Hex(string plainText, string salt = null)
        {
            return SHA256Hex(Encoding.UTF8.GetBytes(plainText + (salt ?? string.Empty)));
        }
        public static string SHA256Hex(byte[] buffer)
        {
            using (var provider = new SHA256Managed())
            {
                return ByteArrayToHex(provider.ComputeHash(buffer));
            }
        }
        public static bool VerifySHA256Hex(string cypherText, string plainText, string salt = null)
        {
            return cypherText == SHA256Hex(plainText, salt);
        }



        public static string SHA384Sum(string path)
        {
            return SHA384Hex(File.ReadAllBytes(path));
        }
        public static string SHA384Hex(string plainText, string salt = null)
        {
            return SHA384Hex(Encoding.UTF8.GetBytes(plainText + (salt ?? string.Empty)));
        }
        public static string SHA384Hex(byte[] buffer)
        {
            using (var provider = new SHA384Managed())
            {
                return ByteArrayToHex(provider.ComputeHash(buffer));
            }
        }
        public static bool VerifySHA384Hex(string cypherText, string plainText, string salt = null)
        {
            return cypherText == SHA384Hex(plainText, salt);
        }



        public static string SHA512Sum(string path)
        {
            return SHA512Hex(File.ReadAllBytes(path));
        }
        public static string SHA512Hex(string plainText, string salt = null)
        {
            return SHA512Hex(Encoding.UTF8.GetBytes(plainText + (salt ?? string.Empty)));
        }
        public static string SHA512Hex(byte[] buffer)
        {
            using (var provider = new SHA512Managed())
            {
                return ByteArrayToHex(provider.ComputeHash(buffer));
            }
        }
        public static bool VerifySHA512Hex(string cypherText, string plainText, string salt = null)
        {
            return cypherText == SHA512Hex(plainText, salt);
        }

        public static string MD5Sum(string path)
        {
            return MD5Hex(File.ReadAllBytes(path));
        }
        public static string MD5Hex(string plainText, string salt = null)
        {
            return MD5Hex(Encoding.UTF8.GetBytes(plainText + (salt ?? string.Empty)));
        }
        public static string MD5Hex(byte[] buffer)
        {
            using (var provider = new MD5CryptoServiceProvider())
            {
                return ByteArrayToHex(provider.ComputeHash(buffer));
            }
        }
        public static bool VerifyMD5Hex(string cypherText, string plainText, string salt = null)
        {
            return cypherText == MD5Hex(plainText, salt);
        }



        private static string ByteArrayToHex(byte[] buffer)
        {
            return BitConverter.ToString(buffer).Replace("-", string.Empty).ToLowerInvariant();
        }
        #endregion



        #region MachineKey
        public static string GetASPNET20MachineKey()
        {
            var aspnet20Machinekey = new StringBuilder();
            var key64Byte = GetRandomKey(64);
            var key32Byte = GetRandomKey(32);
            aspnet20Machinekey.Append("<machineKey \n");
            aspnet20Machinekey.Append("validationKey=\"" + key64Byte + "\"\n");
            aspnet20Machinekey.Append("decryptionKey=\"" + key32Byte + "\"\n");
            aspnet20Machinekey.Append("validation=\"SHA1\" decryption=\"AES\"\n");
            aspnet20Machinekey.Append("/>\n");
            return aspnet20Machinekey.ToString();
        }
        public static string GetASPNET11MachineKey()
        {
            var aspnet11Machinekey = new StringBuilder();
            var key64Byte = GetRandomKey(64);
            var key24Byte = GetRandomKey(24);

            aspnet11Machinekey.Append("<machineKey ");
            aspnet11Machinekey.Append("validationKey=\"" + key64Byte + "\"\n");
            aspnet11Machinekey.Append("decryptionKey=\"" + key24Byte + "\"\n");
            aspnet11Machinekey.Append("validation=\"SHA1\"\n");
            aspnet11Machinekey.Append("/>\n");
            return aspnet11Machinekey.ToString();
        }
        public static string GetRandomKey(int byteLength)
        {
            var len = byteLength * 2;
            var buff = new byte[len / 2];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buff);
            var sb = new StringBuilder(len);
            for (var i = 0; i < buff.Length; i++)
                sb.Append($"{buff[i]:X2}");
            return sb.ToString();
        }
        #endregion
    }
}
