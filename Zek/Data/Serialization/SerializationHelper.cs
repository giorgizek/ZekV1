using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Zek.Extensions;
using Zek.IO;

namespace Zek.Data.Serialization
{
    public class SerializationHelper
    {
        #region Serialize
        public static byte[] SerializeXml(object instance, bool compress = false)
        {
            if (instance == null) return null;

            using (var ms = new MemoryStream())
            {
                var xmls = new XmlSerializer(instance.GetType());
                //var xsn = new XmlSerializerNamespaces();
                //xsn.Add(string.Empty, null);
                //xmls.Serialize(ms, instance, xsn);

                using (var xmlWriter = new XmlTextWriter(ms, null) { Formatting = Formatting.Indented, Indentation = 2 })
                {
                    xmls.Serialize(xmlWriter, instance, null, null, null);
                }

                return compress ? GZipHelper.Compress(ms.ToArray()) : ms.ToArray();
            }
        }
        public static byte[] SerializeXmlWithoutNamespaces(object instance, bool compress = false)
        {
            if (instance == null) return null;

            using (var ms = new MemoryStream())
            {
                var xsn = new XmlSerializerNamespaces();
                xsn.Add(string.Empty, string.Empty);

                var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Encoding = new UTF8Encoding(false) };
                //settings.Indent = true;
                //settings.IndentChars = "\t";
                //settings.NewLineChars = Environment.NewLine;
                //settings.ConformanceLevel = ConformanceLevel.Document;


                var xmls = new XmlSerializer(instance.GetType());
                using (var writer = XmlWriter.Create(ms, settings))
                {
                    xmls.Serialize(writer, instance, xsn, null, null);
                }
                return compress ? GZipHelper.Compress(ms.ToArray()) : ms.ToArray();
            }
        }
        public static byte[] SerializeBinary(object instance, bool compress = false)
        {
            if (instance == null) return null;

            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, instance);

                return compress ? GZipHelper.Compress(ms.ToArray()) : ms.ToArray();
            }
        }

        public static string SerializeXmlString(object instance, bool compress = false, bool includeNamespaces = false)
        {
            if (instance == null) return null;

            var buffer = includeNamespaces ? SerializeXml(instance, compress) : SerializeXmlWithoutNamespaces(instance, compress);
            return compress ? Convert.ToBase64String(buffer) : buffer.UTF8ArrayToString();
        }
        public static string SerializeBinaryString(object instance, bool compress = false)
        {
            if (instance == null) return null;
            return Convert.ToBase64String(SerializeBinary(instance, compress));
        }


        public static void SerializeXmlFile(object instance, string path, bool compress = false)
        {
            InternalCheck(instance);
            File.WriteAllBytes(path, SerializeXml(instance, compress));
        }
        public static void SerializeBinaryFile(object instance, string path, bool compress = false)
        {
            InternalCheck(instance);
            File.WriteAllBytes(path, SerializeBinary(instance, compress));
        }


        public static bool IsXmlSerializable(object objectToCheck)
        {
            try
            {
                if (objectToCheck == null)
                    throw new ArgumentNullException(nameof(objectToCheck));

                var checkType = objectToCheck.GetType();

                return checkType.IsSerializable;
            }
            catch
            {
                return false;
            }
        }
        private static void InternalCheck(object instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance), @"The instance object is null.");
        }
        #endregion

        #region Deserialize
        public static T DeserializeXmlString<T>(string xml, bool decompress = false)
        {
            if (string.IsNullOrEmpty(xml)) return default(T);

            return DeserializeXml<T>(decompress ? Convert.FromBase64String(xml) : xml.ToUTF8Array(), decompress);
        }

        public static T DeserializeBinaryString<T>(string binary, bool decompress = false)
        {
            return string.IsNullOrEmpty(binary)
                ? default(T)
                : DeserializeBinary<T>(Convert.FromBase64String(binary), decompress);
        }


        public static T DeserializeXmlFile<T>(string path, bool decompress = false)
        {
            return DeserializeXml<T>(File.ReadAllBytes(path), decompress);
        }

        public static T DeserializeBinaryFile<T>(string path, bool decompress = false)
        {
            return DeserializeBinary<T>(File.ReadAllBytes(path), decompress);
        }


        public static T DeserializeXml<T>(byte[] buffer, bool decompress = false)
        {
            if (buffer == null || buffer.Length == 0) return default(T);
            using (var ms = new MemoryStream(decompress ? GZipHelper.Decompress(buffer) : buffer))
            {
                var ser = new XmlSerializer(typeof(T));
                return (T)ser.Deserialize(ms);
            }
        }

        public static T DeserializeBinary<T>(byte[] buffer, bool decompress = false)
        {
            if (buffer == null || buffer.Length == 0) return default(T);

            using (var ms = new MemoryStream(decompress ? GZipHelper.Decompress(buffer) : buffer))
            {
                var bf = new BinaryFormatter();
                //var bf = new BinaryFormatter { Binder = new DeserializationBinder(typeof(T)) };
                return (T)bf.Deserialize(ms);
            }
        }

        //private class DeserializationBinder : SerializationBinder
        //{
        //    private readonly Type _type;
        //    public DeserializationBinder(Type type)
        //    {
        //        switch (type.Name)
        //        {
        //            case "IList`1":
        //            case "List`1":
        //                //Type type = abc.GetType().GetGenericArguments()[0];
        //                //var type = abc.GetType().GetProperty("Item").PropertyType;
        //                //var type = abc.GetType().GetTypeInfo().GenericTypeArguments[0];
        //                var args = type.GetGenericArguments();
        //                if (args.Length > 0)
        //                    _type = args[0];
        //                break;
        //            default:
        //                _type = type;
        //                break;
        //        }
        //    }

        //    public override Type BindToType(string assemblyName, string typeName)
        //    {
        //        return (assemblyName.Contains("System") || assemblyName.Contains("mscorlib"))
        //            ? null//Type.GetType(String.Format("{0}, {1}", typeName, assemblyName))
        //                : _type;
        //    }
        //}
        #endregion
    }


}
