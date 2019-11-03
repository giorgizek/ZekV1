using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace Zek.Extensions.Clone
{

    /// <summary>
    /// Provides a method for performing a deep copy of an object.
    /// Binary Serialization is used to perform the copy.
    /// </summary>
    public static class CloneExtensions
    {
        ///// <span class="code-SummaryComment"><summary></span>
        ///// Perform a deep Copy of the object.
        ///// <span class="code-SummaryComment"></summary></span>
        ///// <span class="code-SummaryComment"><typeparam name="T">The type of object being copied.</typeparam></span>
        ///// <span class="code-SummaryComment"><param name="source">The object instance to copy.</param></span>
        ///// <span class="code-SummaryComment"><returns>The copied object.</returns></span>
        //public static T CloneByBinaryFormatterSerialize<T>(this T source)
        //{
        //    if (!typeof(T).IsSerializable)
        //        throw new ArgumentException("The type must be serializable.", "source");

        //    // Don't serialize a null object, simply return the default for that object
        //    if (Object.ReferenceEquals(source, null))
        //        return default(T);

        //    using (var ms = new MemoryStream())
        //    {
        //        var bf = new BinaryFormatter();
        //        bf.Serialize(ms, source);
        //        ms.Seek(0, SeekOrigin.Begin);
        //        return (T)bf.Deserialize(ms);
        //    }
        //}


        //public static T CloneByXmlSerialize<T>(this T source)
        //{
        //    // Don't serialize a null object, simply return the default for that object
        //    if (Object.ReferenceEquals(source, null))
        //        return default(T);

        //    using (var ms = new MemoryStream())
        //    {
        //        var serializer = new XmlSerializer(source.GetType());
        //        using (XmlWriter writer = XmlTextWriter.Create(ms))
        //        {
        //            serializer.Serialize(writer, source);
        //        }
        //        ms.Seek(0, SeekOrigin.Begin);
        //        return (T)serializer.Deserialize(ms);
        //    }
        //}


    }
}
