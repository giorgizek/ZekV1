using System;
using System.Security.Cryptography;
using System.Text;
using Zek.Cryptography;
// ReSharper disable InconsistentNaming

namespace Zek.Utils
{
    public enum IdLinkMode
    {
        None,
        MD5,
        SHA1,
        SHA256,
        Aes
    }

    public static class IdLinkHelper
    {

        public static string Encode(string[] values, string key = null, IdLinkMode mode = IdLinkMode.SHA1)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (string.IsNullOrEmpty(key))
                mode = IdLinkMode.None;

            string hash;
            switch (mode)
            {
                case IdLinkMode.MD5:
                    using (var alg = SHA1.Create())
                    {
                        var plainText = string.Join(string.Empty, values) + key;
                        hash = Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
                    }
                    return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join("||", values) + "||" + hash));

                case IdLinkMode.SHA1:
                    using (var alg = SHA1.Create())
                    {
                        var plainText = string.Join(string.Empty, values) + key;
                        hash = Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
                    }
                    return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join("||", values) + "||" + hash));


                case IdLinkMode.SHA256:
                    using (var alg = SHA256.Create())
                    {
                        var plainText = string.Join(string.Empty, values) + key;
                        hash = Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
                    }
                    return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join("||", values) + "||" + hash));

                case IdLinkMode.Aes:
                    return AesHelper.Encrypt(string.Join("||", values), key);

                default:
                    return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join("||", values)));
            }
        }

        public static string[] Decode(string idLink, string key = null, IdLinkMode mode = IdLinkMode.SHA1)
        {
            if (idLink == null)
                throw new ArgumentNullException(nameof(idLink));

            byte[] array;
            try
            {
                array = Convert.FromBase64String(idLink);
            }
            catch
            {
                return null;
            }

            if (string.IsNullOrEmpty(key))
                mode = IdLinkMode.None;

            string[] values;
            switch (mode)
            {
                case IdLinkMode.Aes:
                    try
                    {
                        values = AesHelper.Decrypt(array, key).Split(new[] { "||" }, StringSplitOptions.None);
                    }
                    catch
                    {
                        return null;
                    }
                    break;

                case IdLinkMode.MD5:
                    values = Encoding.UTF8.GetString(array).Split(new[] { "||" }, StringSplitOptions.None);
                    if (values.Length == 1)
                        return null;

                    using (var alg = MD5.Create())
                    {
                        var hashed = values[values.Length - 1];
                        Array.Resize(ref values, values.Length - 1);

                        var plainText = string.Join(string.Empty, values) + key;
                        var hash = Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
                        if (hashed != hash)
                            return null;
                    }
                    break;

                case IdLinkMode.SHA1:
                    values = Encoding.UTF8.GetString(array).Split(new[] { "||" }, StringSplitOptions.None);
                    if (values.Length == 1)
                        return null;

                    using (var alg = SHA1.Create())
                    {
                        var hashed = values[values.Length - 1];
                        Array.Resize(ref values, values.Length - 1);

                        var plainText = string.Join(string.Empty, values) + key;
                        var hash = Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
                        if (hashed != hash)
                            return null;
                    }
                    break;

                case IdLinkMode.SHA256:
                    values = Encoding.UTF8.GetString(array).Split(new[] { "||" }, StringSplitOptions.None);
                    if (values.Length == 1)
                        return null;

                    using (var alg = SHA256.Create())
                    {
                        var hashed = values[values.Length - 1];
                        Array.Resize(ref values, values.Length - 1);

                        var plainText = string.Join(string.Empty, values) + key;
                        var hash = Convert.ToBase64String(alg.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
                        if (hashed != hash)
                            return null;
                    }
                    break;

                default:
                    values = Encoding.UTF8.GetString(array).Split(new[] { "||" }, StringSplitOptions.None);
                    break;
            }

            return values;
        }
    }
}
