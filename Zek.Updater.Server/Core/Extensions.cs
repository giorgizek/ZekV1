using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Zek
{
    public static partial class Ext
    {
        public static string IfNullEmpty(this string str)
        {
            return str ?? string.Empty;
        }
        public static bool ToBoolean(this string str)
        {
            str = str.IfNullEmpty().ToUpperInvariant();
            return (str == "TRUE" || str == "YES" || str == "1" || str == "ON");
        }
        public static int ToInt32(this string str)
        {
            str = str.IfNullEmpty().ToUpperInvariant();
            int result;
            return int.TryParse(str, out result) ? result : 0;
        }


        public static string MD5HexFile(string file)
        {
            return MD5Hex(File.ReadAllBytes(file));
        }
        public static string MD5HexText(string plainText)
        {
            return MD5Hex(Encoding.UTF8.GetBytes(plainText));
        }
        private static string MD5Hex(byte[] buffer)
        {
            using (var md5Hash = new MD5CryptoServiceProvider())
            {
                var computed = md5Hash.ComputeHash(buffer);

                return BitConverter.ToString(computed).Replace("-", string.Empty).ToLowerInvariant();
            }
        }

        public static bool GetConfigBool(string key)
        {
            return GetConfigString(key).ToBoolean();
        }
        public static int GetConfigInt32(string key)
        {
            return GetConfigString(key).ToInt32();
        }
        public static string GetConfigString(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }
        public static void SetConfig(Configuration config, string key, string value)
        {
            if (config.AppSettings.Settings[key] == null)
                config.AppSettings.Settings.Add(key, value);
            else
                config.AppSettings.Settings[key].Value = value;
        }
        public static void SetConfig(Configuration config, string key, bool value)
        {
            SetConfig(config, key, (value ? "1" : "0"));
        }
        public static bool ExistsConfigFile()
        {
            return ExistsConfigFile(System.Reflection.Assembly.GetEntryAssembly());
        }
        public static bool ExistsConfigFile(System.Reflection.Assembly assembly)
        {
            return File.Exists(assembly.Location + ".config");
        }


        public static T DeserializeXml<T>(string xml)
        {
            return DeserializeXml<T>(Encoding.UTF8.GetBytes(xml));
        }
        public static T DeserializeXml<T>(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
            {
                var ser = new XmlSerializer(typeof(T));
                return (T)ser.Deserialize(ms);
            }
        }
        public static string SerializeXml(object instance)
        {
            using (var ms = new MemoryStream())
            {
                var xmls = new XmlSerializer(instance.GetType());
                using (var xmlWriter = new XmlTextWriter(ms, null) { Formatting = Formatting.Indented, Indentation = 2 })
                {
                    var ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    xmls.Serialize(xmlWriter, instance, ns, null, null);
                }

                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }


    }



}
