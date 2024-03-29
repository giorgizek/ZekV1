﻿using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Zek.Extensions
{
    public static class ReflectionExtensions
    {
        public static Type UnNullify(this Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        public static Type Nullify(this Type type)
        {
            return type.IsClass || type.IsInterface || type.IsNullable() ? type : typeof(Nullable<>).MakeGenericType(type);
        }

        public static bool IsNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        public static Type ReturningType(this MemberInfo m)
        {
            return m is PropertyInfo ? ((PropertyInfo)m).PropertyType :
                m is FieldInfo ? ((FieldInfo)m).FieldType :
                m is MethodInfo ? ((MethodInfo)m).ReturnType :
                m is ConstructorInfo ? m.DeclaringType :
                m is EventInfo ? ((EventInfo)m).EventHandlerType : null;
        }

        public static bool HasAttribute<T>(this ICustomAttributeProvider mi) where T : Attribute
        {
            return mi.IsDefined(typeof(T), false);
        }

        public static bool HasAttributeInherit<T>(this ICustomAttributeProvider mi) where T : Attribute
        {
            return mi.IsDefined(typeof(T), true);
        }

        //public static T SingleAttribute<T>(this ICustomAttributeProvider mi) where T : Attribute
        //{
        //    return mi.GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefaultEx();
        //}

        //public static T SingleAttributeInherit<T>(this ICustomAttributeProvider mi) where T : Attribute
        //{
        //    return mi.GetCustomAttributes(typeof(T), true).Cast<T>().SingleOrDefaultEx();
        //}

        public static bool IsInstantiationOf(this Type type, Type genericTypeDefinition)
        {
            if (!genericTypeDefinition.IsGenericTypeDefinition)
                throw new ArgumentException("Parameter 'genericTypeDefinition' should be a generic type definition");

            return type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition;
        }

        //public static bool IsInstantiationOf(this MethodInfo method, MethodInfo genericMethodDefinition)
        //{
        //    if (!genericMethodDefinition.IsGenericMethodDefinition)
        //        throw new ArgumentException("Parameter 'genericMethodDefinition' should be a generic method definition");

        //    return method.IsGenericMethod && ReflectionTools.MethodEqual(method.GetGenericMethodDefinition(), genericMethodDefinition);
        //}

        //public static IEnumerable<Type> GetGenericInterfaces(this Type type, Type genericInterfaceDefinition)
        //{
        //    return type.GetInterfaces().PreAnd(type).Where(i => i.IsInstantiationOf(genericInterfaceDefinition));
        //}

        //public static bool FieldEquals<S, T>(this FieldInfo fi, Expression<Func<S, T>> field)
        //{
        //    return ReflectionTools.FieldEquals(ReflectionTools.BaseFieldInfo(field), fi);
        //}

        //public static bool PropertyEquals<S, T>(this PropertyInfo pi, Expression<Func<S, T>> property)
        //{
        //    return ReflectionTools.PropertyEquals(ReflectionTools.BasePropertyInfo(property), pi);
        //}

        public static bool IsReadOnly(this PropertyInfo pi)
        {
            var mi = pi.GetSetMethod();

            return mi == null || !mi.IsPublic;
        }

        //public static Type ElementType(this Type ft)
        //{
        //    if (!typeof(IEnumerable).IsAssignableFrom(ft))
        //        return null;

        //    return ft.GetInterfaces().PreAnd(ft)
        //        .SingleOrDefaultEx(ti => ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        //        .TryCC(ti => ti.GetGenericArguments()[0]);
        //}

        public static bool IsExtensionMethod(this MethodInfo m)
        {
            return m.IsStatic && m.HasAttribute<ExtensionAttribute>();
        }

        public static PropertyInfo GetBaseDefinition(this PropertyInfo propertyInfo)
        {
            var method = propertyInfo.GetAccessors(true)[0];
            if (method == null)
                return null;

            var baseMethod = method.GetBaseDefinition();

            if (baseMethod == method)
                return propertyInfo;

            var arguments = propertyInfo.GetIndexParameters().Select(p => p.ParameterType).ToArray();

            return baseMethod.DeclaringType.GetProperty(propertyInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, propertyInfo.PropertyType, arguments, null);
        }
    }
}
