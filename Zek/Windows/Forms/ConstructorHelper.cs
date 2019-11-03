using System;
using System.Reflection;

namespace Zek.Windows.Forms
{
    /// <summary>
    /// კონსტრუქტორის დამხმარე კლასი (ამ კლასის მეშვეობით შეგვიძლია შევქმნათ ფორმები...).
    /// </summary>
    public class ConstructorHelper
    {
        /*
        /// <summary>
        /// ქმნის მითითებული კლასის მომწოდებლის ახალ ობიექტს.
        /// </summary>
        /// <param name="providerName">მომწოდებლის სახელი.</param>
        /// <param name="providerType">მომწოდებლის ტიპის სახელი.</param>
        /// <returns>აბრუნებს მითითებული კლასის მომწოდებლის ახალ ობიექტს.</returns>
        /// <exception cref="ArgumentException">მომწოდებლის სახელის ნულოვნობის ან ცარიელობის შემთხვევაში.</exception>
        /// <exception cref="ArgumentException">მომწოდებლის ტიპის სახელის ნულოვნობის ან ცარიელობის შემთხვევაში.</exception>
        /// <exception cref="OrionException">თუ ვერ მოხერხდა მომწოდებლის შექმნა.</exception>
        public static object CreateInstance(string formName, string formType)
        {

            Type type = Type.GetType(formType);

            if (type == null)
            {
            }

            ConstructorInfo constructor = type.GetConstructor(new Type[] { });//Type.EmptyTypes
            if (constructor == null)
            {
                throw new Exception("There is No Constructor for Type " + formType);
            }
            object createdObject = null;
            try
            {
                createdObject = constructor.Invoke(null);
            }
            catch (MethodAccessException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (TargetInvocationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (TargetParameterCountException ex)
            {
                throw new Exception(ex.Message);
            }
            return createdObject;
        }*/

        /// <summary>
        /// ცვლადის შექმნა
        /// </summary>
        /// <typeparam name="T">ტიპი - თუ რატიპის ცვლადის შექმნა გვინდა.</typeparam>
        /// <returns>აბრუნებს კონსტრუქტორით შექმნილ კლასსის ცვლადს.</returns>
        public static T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
        }
        public static object CreateInstance(Type type)
        {
            var constructor = type.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                throw new Exception("There is No Constructor for Type " + type);
            }
            object createdObject;
            try
            {
                createdObject = constructor.Invoke(null);
            }
            catch (MethodAccessException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (TargetInvocationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (TargetParameterCountException ex)
            {
                throw new Exception(ex.Message);
            }
            return createdObject;
        }

        public static T CreateInstance<T>(Type[] constructorTypes, object[] constructorParameters)
        {
            return (T)CreateInstance(typeof(T), constructorTypes, constructorParameters);
        }
        public static object CreateInstance(Type type, Type[] constructorTypes, object[] constructorParameters)
        {
            var constructor = type.GetConstructor(constructorTypes);
            if (constructor == null)
            {
                throw new Exception("There is No Constructor for Type " + type);
            }
            var createdObject = constructor.Invoke(constructorParameters);
            return createdObject;
        }

        ///// <summary>
        ///// აკლონირებს ობიექტს (მისი Property-ებიანად).
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static object CloneObject(object obj)
        //{
        //    Type t = obj.GetType();
        //    PropertyInfo[] properties = t.GetProperties();

        //    object p = t.InvokeMember(string.Empty, System.Reflection.BindingFlags.CreateInstance, null, obj, null);

        //    foreach (PropertyInfo pi in properties)
        //    {
        //        if (pi.CanWrite)
        //        {
        //            pi.SetValue(p, pi.GetValue(obj, null), null);
        //        }
        //    }

        //    return p;
        //}
    }
}
