using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Zek.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// ადარებს ორ მნიშვნელობას ერთმანეთს.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრები ერთმანეთის ტოლია.</returns>
        public static bool Compare(this object val1, object val2)
        {
            if (val1 != null && val2 != null && val1.Equals(val2)) return true;
            return val1 == val2;
        }

        /// <summary>
        /// აკონვერტირებს მოცემულ ტიპად ობიექტს.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="target"></param>
        public static void Cast<T>(this object obj, out T target) where T : IConvertible
        {
            target = (T)Convert.ChangeType(obj, typeof(T));
        }

        public static T CopyObject<T>(this T obj, bool deep = false, bool property = true, bool field = false) //where T : class
        {
            return (T)CopyObject((object)obj, deep, property, field);
        }


        public static object CopyObject(this object obj, bool deep = false, bool property = true, bool field = false)
        {
            if (obj == null) return null;
            if (!field && !property)
                throw new ArgumentException(@"Parameter required (field, property).", nameof(field));

            var type = obj.GetType();
            if (type.IsValueType || type == typeof(string))
                return obj;
            //else if (type.IsArray)
            //{
            //    Type elementType = Type.GetType(type.FullName.Replace("[]", string.Empty));
            //    var array = obj as Array;
            //    Array copied = Array.CreateInstance(elementType, array.Length);
            //    for (int i = 0; i < array.Length; i++)
            //    {
            //        copied.SetValue(CopyObject(array.GetValue(i), deep, property), i);
            //    }
            //    return Convert.ChangeType(copied, obj.GetType());
            //}
            if (!type.IsClass) throw new ArgumentException("Unknown type");


            var result = Activator.CreateInstance(obj.GetType());
            if (property)
            {
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var prop in properties.Where(p => p.CanRead && p.CanWrite))
                {
                    switch (prop.PropertyType.Name)
                    {
                        case "IList`1":
                        case "List`1":
                            if (!deep) continue;
                            var listObject = (IList)prop.GetValue(result, null);
                            if (listObject != null)
                            {
                                foreach (var item in (IList)prop.GetValue(obj, null))
                                {
                                    listObject.Add(CopyObject(item, true, true, field));
                                }
                            }
                            break;


                        case "ICollection`1":
                        case "HashSet`1":
                            //if (!deep) continue;
                            //Type t = typeof(System.Collections.IEnumerable);
                            //Console.WriteLine(t.IsAssignableFrom(T)); //returns true for collentions
                            //var hashSetObject = Activator.CreateInstance(type);
                            //var hashSetObject = (ICollection)field.GetValue(result);
                            //if (hashSetObject != null)
                            //{
                            //    foreach (object item in ((IEnumerable)field.GetValue(obj)))
                            //    {
                            //        ((ICollection<object>)hashSetObject).Add(CopyObject(propertyValue, deep, property, field));
                            //    }
                            //}
                            break;

                        default:
                            if (deep || prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                            {
                                var propertyValue = prop.GetValue(obj, null);
                                if (propertyValue == null)
                                    continue;
                                prop.SetValue(result, CopyObject(propertyValue, deep, true, field), null);
                            }
                            break;
                    }
                }
            }

            if (field)
            {
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var fi in fields)
                {
                    switch (fi.FieldType.Name)
                    {
                        case "IList`1":
                        case "List`1":
                            if (!deep) continue;

                            var listObject = (IList)fi.GetValue(result);
                            if (listObject != null)
                            {
                                foreach (var item in (IList)fi.GetValue(obj))
                                {
                                    listObject.Add(CopyObject(item, true, property, true));
                                }
                            }
                            break;


                        case "ICollection`1":
                        case "HashSet`1":
                            //if (!deep) continue;
                            //Type t = typeof(System.Collections.IEnumerable);
                            //Console.WriteLine(t.IsAssignableFrom(T)); //returns true for collentions
                            //var hashSetObject = Activator.CreateInstance(type);
                            //var hashSetObject = (ICollection)field.GetValue(result);
                            //if (hashSetObject != null)
                            //{
                            //    foreach (object item in ((IEnumerable)field.GetValue(obj)))
                            //    {
                            //        ((ICollection<object>)hashSetObject).Add(CopyObject(item, deep, property, field));
                            //    }
                            //}
                            break;

                        default:


                            if (deep || fi.FieldType.IsValueType || fi.FieldType == typeof(string))
                            {
                                var fieldValue = fi.GetValue(obj);
                                if (fieldValue == null)
                                    continue;
                                fi.SetValue(result, CopyObject(fieldValue, deep, property, true));
                            }
                            break;
                    }
                }
            }

            return result;
        }


        public static void Assign(this object source, object dest, bool deep = false, bool property = true, bool field = false)
        {
            if (source == null || dest == null) return;

            if (!field && !property)
                throw new ArgumentException(@"Parameter required (field, property).", nameof(field));

            var type1 = source.GetType();
            var type2 = dest.GetType();

            if ((type1.IsValueType && type2.IsValueType) || (type1 == typeof(string) && type2 == typeof(string)))
            {
                dest = source;
                return;
            }


            if (!type1.IsClass || !type2.IsClass) throw new ArgumentException("Unknown type");


            if (property)
            {
                var properties = (from t1 in type1.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                  join t2 in type2.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) on t1.Name equals t2.Name
                                  select new { Type1 = t1, Type2 = t2 }).ToArray();
                foreach (var prop in properties)
                {
                    switch (prop.Type1.PropertyType.Name)
                    {
                        case "IList`1":
                        case "List`1":
                            if (!deep) continue;
                            var listObject = (IList)prop.Type2.GetValue(dest, null);
                            if (listObject != null)
                            {
                                foreach (var item in (IList)prop.Type1.GetValue(source, null))
                                {
                                    listObject.Add(CopyObject(item, true, true, field));
                                }
                            }
                            break;


                        case "ICollection`1":
                        case "HashSet`1":
                            //if (!deep) continue;
                            //Type t = typeof(System.Collections.IEnumerable);
                            //Console.WriteLine(t.IsAssignableFrom(T)); //returns true for collentions
                            //var hashSetObject = Activator.CreateInstance(type);
                            //var hashSetObject = (ICollection)field.GetValue(result);
                            //if (hashSetObject != null)
                            //{
                            //    foreach (object item in ((IEnumerable)field.GetValue(obj)))
                            //    {
                            //        ((ICollection<object>)hashSetObject).Add(CopyObject(propertyValue, deep, property, field));
                            //    }
                            //}
                            break;

                        default:
                            if (deep
                                || (prop.Type1.PropertyType.IsValueType && prop.Type2.PropertyType.IsValueType)
                                || (prop.Type1.PropertyType == typeof(string) && prop.Type2.PropertyType == typeof(string))
                                )
                            {
                                var propertyValue = prop.Type1.GetValue(source, null);
                                if (propertyValue == null)
                                    continue;
                                prop.Type2.SetValue(dest, CopyObject(propertyValue, deep, true, field), null);
                            }
                            break;
                    }
                }
            }

            if (field)
            {
                var fields = (from t1 in type1.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                              join t2 in type2.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) on t1.Name equals t2.Name
                              select new { Type1 = t1, Type2 = t2 }).ToArray();
                foreach (var fi in fields)
                {
                    switch (fi.Type1.FieldType.Name)
                    {
                        case "IList`1":
                        case "List`1":
                            if (!deep) continue;

                            var listObject = (IList)fi.Type2.GetValue(dest);
                            if (listObject != null)
                            {
                                foreach (var item in (IList)fi.Type1.GetValue(source))
                                {
                                    listObject.Add(CopyObject(item, true, property, true));
                                }
                            }
                            break;


                        case "ICollection`1":
                        case "HashSet`1":
                            //if (!deep) continue;
                            //Type t = typeof(System.Collections.IEnumerable);
                            //Console.WriteLine(t.IsAssignableFrom(T)); //returns true for collentions
                            //var hashSetObject = Activator.CreateInstance(type);
                            //var hashSetObject = (ICollection)field.GetValue(result);
                            //if (hashSetObject != null)
                            //{
                            //    foreach (object item in ((IEnumerable)field.GetValue(obj)))
                            //    {
                            //        ((ICollection<object>)hashSetObject).Add(CopyObject(item, deep, property, field));
                            //    }
                            //}
                            break;

                        default:


                            if (deep
                                || (fi.Type1.FieldType.IsValueType && fi.Type2.FieldType.IsValueType)
                                || (fi.Type1.FieldType == typeof(string) && fi.Type2.FieldType == typeof(string))
                                )
                            {
                                var fieldValue = fi.Type1.GetValue(source);
                                if (fieldValue == null)
                                    continue;
                                fi.Type2.SetValue(dest, CopyObject(fieldValue, deep, property, true));
                            }
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Creates a deep copy of object by serializing to memory stream.
        /// </summary>
        /// <param name="obj"></param>
        public static T DeepClone<T>(this T obj) where T : class
        {
            if (obj == null)
                return null;

            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                return (T)bf.Deserialize(ms);
            }
        }


        public static object GetPropertyValue(this object obj, string propertyName, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
            var type = obj.GetType();
            var propInfo = type.GetProperty(propertyName, bindingFlags);
            if (propInfo == null)
                throw new ArgumentException($"{propertyName} is not a valid property of type: {type.FullName}");

            return propInfo.GetValue(obj, null);
        }
        public static void SetPropertyValue(this object obj, string propertyName, object value, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
            var type = obj.GetType();
            var propInfo = type.GetProperty(propertyName, bindingFlags);
            if (propInfo == null)
                throw new ArgumentException($"{propertyName} is not a valid property of type: {type.FullName}");

            propInfo.SetValue(obj, value, null);
        }
        public static bool TrySetPropertyValue(this object obj, string propertyName, object value, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
            try
            {
                var type = obj.GetType();
                var propInfo = type.GetProperty(propertyName, bindingFlags);
                if (propInfo != null)
                    propInfo.SetValue(obj, value, null);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static object GetFieldValue(this object obj, string fieldName, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
            var type = obj.GetType();
            var fieldInfo = type.GetField(fieldName, bindingFlags);
            if (fieldInfo == null)
                throw new ArgumentException($"{fieldName} is not a valid field of type: {type.FullName}");

            return fieldInfo.GetValue(obj);
        }
        public static void SetFieldValue(this object obj, string fieldName, object value, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
            var type = obj.GetType();
            var fieldInfo = type.GetField(fieldName, bindingFlags);
            if (fieldInfo == null)
                throw new ArgumentException($"{fieldName} is not a valid field of type: {type.FullName}");

            fieldInfo.SetValue(obj, value);
        }
        public static bool TrySetFieldValue(this object obj, string fieldName, object value, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic| BindingFlags.Instance)
        {
            try
            {
                var type = obj.GetType();
                var fieldInfo = type.GetField(fieldName, bindingFlags);
                if (fieldInfo != null)
                    fieldInfo.SetValue(obj, value);

                return true;
            }
            catch
            {
                return false;
            }
        }




        public static Dictionary<string, object> GetPropertiesDictionary(this object obj, bool throwError = true, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
            var type = obj.GetType();
            var properties = type.GetProperties(bindingFlags);
            var result = new Dictionary<string, object>();
            foreach (var prop in properties.Where(p => p.CanRead))
            {
                try
                {
                    var propertyValue = prop.GetValue(obj, null);
                    result.Add(prop.Name, propertyValue);
                }
                catch (Exception ex)
                {
                    if (throwError)
                        throw;

                    result.Add(prop.Name, ex.Message);
                }

                //switch (prop.PropertyType.Name)
                //{
                //case "IList`1":
                //case "List`1":
                //    if (!deep) continue;
                //    var listObject = (IList)prop.GetValue(result, null);
                //    if (listObject != null)
                //    {
                //        foreach (var item in ((IList)prop.GetValue(obj, null)))
                //        {
                //            listObject.Add(CopyObject(item, true, true, field));
                //        }
                //    }
                //    break;


                //case "ICollection`1":
                //case "HashSet`1":
                //    //if (!deep) continue;
                //    //Type t = typeof(System.Collections.IEnumerable);
                //    //Console.WriteLine(t.IsAssignableFrom(T)); //returns true for collentions
                //    //var hashSetObject = Activator.CreateInstance(type);
                //    //var hashSetObject = (ICollection)field.GetValue(result);
                //    //if (hashSetObject != null)
                //    //{
                //    //    foreach (object item in ((IEnumerable)field.GetValue(obj)))
                //    //    {
                //    //        ((ICollection<object>)hashSetObject).Add(CopyObject(propertyValue, deep, property, field));
                //    //    }
                //    //}
                //    break;

                //default:
                //    if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                //    {
                //        var propertyValue = prop.GetValue(obj, null);
                //        if (propertyValue == null)
                //            continue;
                //        result.Add(prop.Name, propertyValue);
                //    }
                //break;
                //}
            }

            return result;
        }

        /*public static TOutput CopyObject<TOutput>(this object obj) where TOutput : new()
        {
            //var destinationProperties = typeof(TOutput).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(prop => prop.CanWrite);
            //var sourceProperties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(prop => prop.CanRead);


            var destinationProperties = typeof(TOutput).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(prop => prop.CanWrite);
            var sourceProperties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(prop => prop.CanRead);

            var memberBindings = sourceProperties.Join(destinationProperties,
                sourceProperty => sourceProperty.Name,
                destinationProperty => destinationProperty.Name,
                (sourceProperty, destinationProperty) => new
                {
                    fiFrom = sourceProperty,
                    fiTo = destinationProperty
                }).ToList();

            TOutput result = new TOutput();

            foreach (var item in memberBindings)
            {
                item.fiTo.SetValue(result, item.fiFrom.GetValue(obj, null), null);
            }

            return result;

            //foreach (FieldInfo fiFrom in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            //{
            //    FieldInfo fiTo = obj.GetType().GetField(fiFrom.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            //    if (fiTo != null && fiTo.FieldType.IsAssignableFrom(fiFrom.FieldType))
            //    {

            //    }
            //}



        }*/




        //public static TOutput CopyObject<TOutput>(this object obj)
        //{
        //    var input = Expression.Parameter(obj.GetType(), "input");

        //    // For each property that exists in the destination object, is there a property with the same name in the source object?
        //    var destinationProperties = typeof(TOutput)
        //        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        //        .Where(prop => prop.CanWrite);

        //    var sourceProperties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(prop => prop.CanRead);

        //    var memberBindings = sourceProperties.Join(destinationProperties,
        //        sourceProperty => sourceProperty.Name, destinationProperty => destinationProperty.Name,
        //        (sourceProperty, destinationProperty) => (MemberBinding)Expression.Bind(destinationProperty, Expression.Property(input, sourceProperty)));


        //    var body = Expression.MemberInit(Expression.New(typeof(TOutput)), memberBindings);
        //    var lambda = Expression.Lambda<Func<TOutput>>(body, input);
        //    return lambda.Compile();
        //}


        /*
         /// <summary>
         /// Returns a Boolean value indicating whether a variable is of the indicated Type
         /// </summary>
         /// <param name="obj">Required. Object variable.</param>
         /// <param name="type">The Type to check the object against.</param>
         /// <returns>Returns a Boolean value indicating whether a variable is of the indicated Type</returns>
         public static bool IsType(this object obj, Type type)
         {
             return obj.GetType().Equals(type);
         }

         /// <summary>
         /// Returns a Boolean value indicating whether a variable points to a DateTime object.
         /// </summary>
         /// <param name="obj">Required. Object variable.</param>
         /// <returns>Returns a Boolean value indicating whether a variable points to a DateTime object.</returns>
         public static bool IsDateTime(this object obj)
         {
             return obj.IsType(typeof(System.DateTime));
         }

         /// <summary>
         /// Returns a Boolean value indicating whether an expression evaluates to the DBNull class.
         /// Extension Added by dotNetExt.Object
         /// </summary>
         /// <param name="obj">Required. Object expression.</param>
         /// <returns>Returns a Boolean value indicating whether an expression evaluates to the DBNull class.</returns>
         public static bool IsDBNull(this object obj)
         {
             return obj.IsType(typeof(DBNull));
         }

         /// <summary>
         /// Returns a Boolean value indicating whether an expression evaluates to be NULL.
         /// </summary>
         /// <param name="obj">Required. Object expression.</param>
         /// <returns>Returns a Boolean value indicating whether an expression evaluates to be NULL.</returns>
         public static bool IsNull<T>(this T obj) where T : class
         {
             return obj == null;
         }

         /// <summary>
         /// Returns the First Attribute tied to the Object of the Specified Generic Type
         /// </summary>
         /// <typeparam name="A">The Type of Attribute to Get.</typeparam>
         /// <typeparam name="TAttributeType">type of the attribute </typeparam>
         /// <param name="obj">Required. The Object to get the Attribute for.</param>
         /// <returns>Returns the First Attribute tied to the Object of the Specified Generic Type</returns>
         public static TAttributeType GetAttribute<TAttributeType>(this object obj) where TAttributeType : Attribute
         {
             return obj.GetAttributes<TAttributeType>().FirstOrDefault();
         }

         /// <summary>
         /// Returns All Attributes tied to the Object of the Specified Generic Type
         /// </summary>
         /// <typeparam name="A">The Type of Attribute to Get.</typeparam>
         /// <typeparam name="TAttributeType">Type of the attribute </typeparam>
         /// <param name="obj">Required. The Object to get the Attributes for.</param>
         /// <returns>Returns All Attributes tied to the Object of the Specified Generic Type</returns>
         public static TAttributeType[] GetAttributes<TAttributeType>(this object obj) where TAttributeType : Attribute
         {
             return (TAttributeType[])obj.GetType().GetCustomAttributes(typeof(TAttributeType), true);
         }

         /// <summary>
         /// Returns a Dictionary containing Key/Value pairs that match the objects properties and their values
         /// </summary>
         /// <param name="obj"></param>
         /// <returns></returns>
         public static Dictionary<string, object> ToDictionary(this object obj)
         {
             return TypeDescriptor.GetProperties(obj).Cast<PropertyDescriptor>().ToDictionary(p => p.Name, p => p.GetValue(obj));
         }

         /// <summary>
         /// Allows you to easily pass an Anonymous type from one function to another
         /// </summary>
         /// <typeparam name="T">The Anonymous Type</typeparam>
         /// <param name="obj">The Anonymous Type</param>
         /// <param name="targetType">The Object to "Cast As" the Anonymous type</param>
         /// <returns></returns>
         public static T CastAs<T>(this Object obj, T targetType)
         {
             return (T)obj;
         }

         /// <summary>
         /// Determines whether the specified source is in given range.
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="source">The source.</param>
         /// <param name="list">The list.</param>
         /// <returns>
         ///   <c>true</c> if the specified source is in; otherwise, <c>false</c>.
         /// </returns>
         public static bool IsIn<T>(this T source, params T[] list)
         {
             Func<T, T, bool> compare = (v1, v2) => EqualityComparer<T>.Default.Equals(v1, v2);
             return list.Any(item => compare(item, source));
         }

         /// <summary>
         /// Determines whether the specified property contains attribute.
         /// </summary>
         /// <typeparam name="T">Type of attribute to check against</typeparam>
         /// <param name="property">The property.</param>
         /// <returns>
         ///   <c>true</c> if the specified property contains attribute; otherwise, <c>false</c>.
         /// </returns>
         public static bool ContainsAttribute<T>(this PropertyInfo property) where T : Attribute
         {
             return property.GetAttributes<T>().Length > 0;
             //return property.GetCustomAttributes(false).Any(a => a.GetType() == typeof(T));
         }*/
    }
}