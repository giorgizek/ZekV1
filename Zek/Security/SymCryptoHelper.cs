using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Zek.Security
{
    public class SymCryptoHelper
    {
        private static void Init(SymmetricAlgorithm symmetricAlgorithm, string salt, string key, string iv, bool useHasing)
        {
            if (symmetricAlgorithm == null)
                throw new ArgumentNullException(nameof(symmetricAlgorithm));
            symmetricAlgorithm.Mode = CipherMode.CBC;
            symmetricAlgorithm.Padding = PaddingMode.PKCS7;


            if (salt == null) salt = string.Empty;
            if (key == null) key = string.Empty;
            if (iv == null) iv = string.Empty;

            symmetricAlgorithm.Key = GetLegalKey(symmetricAlgorithm, salt, key, useHasing);
            var z_key = ByteArrayToHex(symmetricAlgorithm.Key);
            var keyy = HexToByteArray(z_key);

            symmetricAlgorithm.IV = GetLegalIV(symmetricAlgorithm, iv);
            var z_IV = ByteArrayToHex(symmetricAlgorithm.IV);
            var ivv = HexToByteArray(z_IV);
            

            if (ivv != null && z_key != null)
            {
                
            }
        }
        internal static string ByteArrayToHex(byte[] bytes)
        {
            var builder = new StringBuilder(bytes.Length * 2);

            foreach (var b in bytes)
            {
                builder.Append(b.ToString("X2"));
            }

            return builder.ToString();
        }
        public static byte[] HexToByteArray(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];

            for (var i = 0; i < hexString.Length; i += 2)
            {
                var s = hexString.Substring(i, 2);
                bytes[i / 2] = byte.Parse(s, NumberStyles.AllowHexSpecifier, null);
            }

            return bytes;
        }

        private static byte[] GetLegalKey(SymmetricAlgorithm symmetricAlgorithm, string salt, string key, bool useHashing)
        {
            if (useHashing)
            {
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    return md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                }
            }


            // Adjust key if necessary, and return a valid key
            if (symmetricAlgorithm.LegalKeySizes.Length > 0)
            {
                // Key sizes in bits
                var keySize = key.Length * 8;
                var minSize = symmetricAlgorithm.LegalKeySizes[0].MinSize;
                var maxSize = symmetricAlgorithm.LegalKeySizes[0].MaxSize;
                var skipSize = symmetricAlgorithm.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)
                {
                    // Extract maximum size allowed
                    key = key.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {
                    var validSize = keySize <= minSize ? minSize : keySize - keySize % skipSize + skipSize;
                    if (keySize < validSize)
                    {
                        // Pad the key with asterisk to make up the size
                        key = key.PadRight(validSize / 8, '*');
                    }
                }
            }

            if (salt.Length < 8)
                salt = salt.PadRight(8, '*');

            var bytes = new Rfc2898DeriveBytes(key, Encoding.UTF8.GetBytes(salt));
            return bytes.GetBytes(key.Length);
        }
        private static byte[] GetLegalIV(SymmetricAlgorithm symmetricAlgorithm, string iv)
        {
            symmetricAlgorithm.GenerateIV();
            var legalIVLength = symmetricAlgorithm.IV.Length;

            if (iv.Length > legalIVLength)
                iv = iv.Substring(0, legalIVLength);
            else if (iv.Length < legalIVLength)
                iv = iv.PadRight(legalIVLength, ' ');

            return Encoding.ASCII.GetBytes(iv);
        }

        //private static SymmetricAlgorithm GetSymmetricAlgorithm(SymmetricAlgorithmTypes algorithmType)
        //{
        //    SymmetricAlgorithm symmetricAlgorithm;

        //    // Select symmetric algorithm
        //    switch (algorithmType)
        //    {
        //        case SymmetricAlgorithmTypes.Rijndael:
        //            symmetricAlgorithm = new RijndaelManaged();
        //            break;
        //        case SymmetricAlgorithmTypes.RC2:
        //            symmetricAlgorithm = new RC2CryptoServiceProvider();
        //            break;
        //        case SymmetricAlgorithmTypes.DES:
        //            symmetricAlgorithm = new DESCryptoServiceProvider();
        //            break;
        //        case SymmetricAlgorithmTypes.TripleDES:
        //            symmetricAlgorithm = new TripleDESCryptoServiceProvider();
        //            break;

        //        default:
        //            throw new ArgumentException(@"Please choose correct 'Symmetric Algorithm Type'", "algorithmType");
        //    }
        //    symmetricAlgorithm.Mode = CipherMode.CBC;

        //    return symmetricAlgorithm;
        //}


        public static string TripleDESEncrypt(string plainText, string salt = null, string key = null, string iv = null, bool useHasing = false)
        {
            return Encrypt(new TripleDESCryptoServiceProvider(), plainText, salt, key, iv, useHasing);
        }
        public static string TripleDESDecrypt(string cipherText, string salt = null, string key = null, string iv = null, bool useHasing = false)
        {
            return Decrypt(new TripleDESCryptoServiceProvider(), cipherText, salt, key, iv, useHasing);
        }



        private static string Encrypt(SymmetricAlgorithm symmetricAlgorithm, string plainText, string salt, string key, string iv, bool useHasing)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                return plainText;

            Init(symmetricAlgorithm, salt, key, iv, useHasing);

            ICryptoTransform cryptoTransform = null;
            byte[] encrypted;
            try
            {
                cryptoTransform = symmetricAlgorithm.CreateEncryptor();
                using (var buffer = new MemoryStream())
                {
                    using (var stream = new CryptoStream(buffer, cryptoTransform, CryptoStreamMode.Write))
                    {
                        using (var writer = new StreamWriter(stream, Encoding.Unicode))
                        {
                            writer.Write(plainText);
                        }
                        encrypted = buffer.ToArray();
                    }
                }
            }
            finally
            {
                cryptoTransform?.Dispose();
            }

            return Convert.ToBase64String(encrypted);
        }
        private static string Decrypt(SymmetricAlgorithm symmetricAlgorithm, string cipherText, string salt, string key, string iv, bool useHasing)
        {
            if (string.IsNullOrWhiteSpace(cipherText))
                return cipherText;

            Init(symmetricAlgorithm, salt, key, iv, useHasing);

            ICryptoTransform cryptoTransform = null;
            string plaintext = null;
            try
            {
                cryptoTransform = symmetricAlgorithm.CreateDecryptor();
                using (var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var stream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            plaintext = reader.ReadToEnd();
                        }
                    }
                }
            }
            finally
            {
                cryptoTransform?.Dispose();
            }
            return plaintext;
        }
    }
}
