using System;
using System.Reflection;

namespace Zek.Extensions
{
    /// <summary>
    /// This class provides extension methods to work with <see cref="Type"/> object types
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns a new instance of the specified Type passing in the specified constructor arguments.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object CreateInstance(this Type type, params object[] args)
        {
            return Activator.CreateInstance(type, args);
        }

        /// <summary>
        /// Returns a new instance of the specified Type cast as the specified Generic type, passing in the specified constructor arguments.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(this Type type, params object[] args)
        {
            return (T)type.CreateInstance(args);
        }


        ///// <summary>
        ///// ანიჭებს Class-ის Property-ის მნიშვნელობას.
        ///// </summary>
        ///// <param name="obj">ობიექტი, რომლის Property-ს შეცვლაც გვინდა.</param>
        ///// <param name="name">Property-ს დასახელება.</param>
        ///// <param name="value">მნიშვნელობა, რომლის მინიჭებაც გვინდა.</param>
        //public static void SetPropertyValue(object obj, string name, object value)
        //{
        //    var pi = obj.GetType().GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        //    pi.SetValue(obj, value, null);
        //}
        ///// <summary>
        ///// ანიჭებს Class-ის Property-ის მნიშვნელობას.
        ///// </summary>
        ///// <param name="obj">ობიექტი, რომლის Property-ს შეცვლაც გვინდა.</param>
        ///// <param name="name">Property-ს დასახელება.</param>
        ///// <param name="value">მნიშვნელობა, რომლის მინიჭებაც გვინდა.</param>
        ///// <returns>აბრუნებს true-ს თუ იპოვა და მიანიჭა მნიშვნელობა.</returns>
        //public static bool TrySetPropertyValue(object obj, string name, object value)
        //{
        //    try
        //    {
        //        var pi = obj.GetType().GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        //        if (pi != null)
        //        {
        //            pi.SetValue(obj, value, null);
        //            return true;
        //        }
        //    }
        //    catch { }

        //    return false;
        //}

        /// <summary>
        /// ეძებს Class-ში Property-ს.
        /// </summary>
        /// <param name="obj">ობიექტი, რომლის Property-ს ვეძებთ</param>
        /// <param name="name">Property-ს დასახელება.</param>
        /// <returns>აბრუნებს true-ს თუ იპოვა.</returns>
        public static bool ContainsProperty(object obj, string name)
        {
            return obj.GetType().GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance) != null;
        }


        public static bool IsSubClass(this Type child, object parent)
        {
            return child != null && parent != null && parent.GetType().IsAssignableFrom(child);
        }
        public static bool IsSubClass(this object childObject, Type parent)
        {
            return childObject != null && parent != null && parent.IsInstanceOfType(childObject);
        }
        public static bool IsSubClass(this Type child, Type parent)
        {
            return child != null && parent != null && parent.IsAssignableFrom(child);
        }


        public static bool IsNullableType(this Type theValueType)
        {
            return theValueType.IsGenericType && theValueType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }


        /// <summary>
        ///  Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, Single, Decimal, String, DateTime and Guid.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Return true if type is primitive. Otherwise false</returns>
        public static bool IsPrimitive(this Type type)
        {
            return type.IsPrimitive
                || type == typeof(string)
                || type == typeof(char?)
                || type == typeof(DateTime)
                || type == typeof(DateTime?)
                || type == typeof(Guid)
                || type == typeof(Guid?)
                || type == typeof(bool?)
                || type == typeof(byte?)
                || type == typeof(sbyte?)
                || type == typeof(short?)
                || type == typeof(ushort?)
                || type == typeof(int?)
                || type == typeof(uint?)
                || type == typeof(long?)
                || type == typeof(ulong?)
                || type == typeof(decimal)
                || type == typeof(decimal?)
                || type == typeof(double?)
                || type == typeof(float?)
                ;
        }


        //public IEnumerable<PropertyInfo> GetProperties(object obj)
        //{
        //    Type t = obj.GetType();

        //    return t.GetProperties().Where(p => (p.Name != "EntityKey" && p.Name != "EntityState")).Select(p => p).ToList();
        //}
        /*
        using (var context = new ModelContainer())
        {
            // Access CSDL
            var container = context.MetadataWorkspace.GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);
            // Access name of related set exposed on your context
            var set = container.BaseEntitySets[context.YourEntitySet.EntitySet.Name];
            // Access all properties
            var properties = set.ElementType.Members.Select(m => m.Name).ToList();
            // Access only keys
            var keys = set.ElementType.KeyMembers.Select(m => m.Name).ToList();
        }
        */
    }
}