using System;
using System.Globalization;
using Zek.Extensions;

namespace Zek.Utils
{
    /// <summary>
    /// Converts a base data type to another base data type.
    /// </summary>
    public class ConvertHelper
    {
        /// <summary>
        /// პარსავს მნიშვნელობას (მნიშვნელობა არის თი არა: string.Empty, DateTimeExtensions.MinDate, Guid.Empty).
        /// </summary>
        /// <param name="value">მნიშვნელობა რომლის გაპარსვაც გვინდა.</param>
        /// <returns></returns>
        public static object Parse(object value)
        {
            if (value.IsNullOrDefault())
                return null;

            if (value is DateTime && (DateTime)value == DateTimeExtensions.MinDate)
                return null;

            if (value is Guid && (Guid)value == Guid.Empty)
                return null;

            if (value is byte && (byte)value == 0)
                return null;

            if (value is short && (short)value == 0)
                return null;

            if (value is int && (int)value == 0)
                return null;

            if (value is long && (long)value == 0L)
                return null;

            if (value is decimal && (decimal)value == 0m)
                return null;

            if (value is double && (double)value == 0d)
                return null;

            return value;
        }


        /// <summary>
        /// აბრუნებს არგუმენტებში გადაცემულ ელემენტს ინდექსის მიხედვით.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object Choose(int? index, params object[] args)
        {
            if (index.HasValue)
            {
                if (args.Length == 0)
                {
                    return null;
                }
                if (index.Value >= 1 && index.Value <= args.Length)
                {
                    return args[index.Value - 1];
                }
            }
            return null;
        }
        /// <summary>
        /// აბრუნებს არგუმენტებში გადაცემულ ელემენტს ინდექსის მიხედვით.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object Choose(int index, params object[] args)
        {
            if ((args.Length != 0) && index >= 1 && index <= args.Length)
            {
                return args[index - 1];
            }
            return null;
        }
        /// <summary>
        /// გამოიძახეთ Switch(i == 0, A, i == 1, B, i != 20, Z.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object Switch(params object[] args)
        {
            for (var i = 0; i < args.Length; i += 2)
            {
                var condition = args[i];
                var result = args[i + 1];
                if (condition is bool && (bool)condition)
                {
                    return result;
                }
                if (condition is bool? && (bool?)condition == true)
                {
                    return result;
                }
            }
            return null;
        }

        public static int IndexOf(params bool[] conditions)
        {
            if (conditions == null)
                throw new ArgumentNullException(nameof(conditions));

            for (var i = 0; i < conditions.Length; i++)
            {
                if (conditions[i])
                    return i;
            }
            return -1;
        }

        //public static T GetTfromString<T>(string mystring)
        //{

        //    //var foo = TypeDescriptor.GetConverter(typeof(T));
        //    //return (T)(foo.ConvertFromInvariantString(mystring));
        //}

        public static T ChangeType<T>(object value)
        {
            return (T)ChangeType(value, typeof(T));
        }
        public static object ChangeType(object value, Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return value != null ? Convert.ChangeType(value, type.GetGenericArguments()[0], CultureInfo.InvariantCulture) : null;
            }


            //if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            //{
            //    return value != null ? Convert.ChangeType(value, Nullable.GetUnderlyingType(type), CultureInfo.InvariantCulture) : null;
            //}

            return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }




        public static T ConvertType<T>(object value)
        {
            return (T)ConvertType(value, typeof(T));
        }

        //public static object ConvertType(object value, Type conversionType)
        //{
        //    switch (conversionType.ToString())
        //    {
        //        case "System.String":
        //            return Convert.ToString(value);
        //        case "System.Char":
        //            return Convert.ToChar(value);

        //        case "System.DateTime":
        //            return ToDateTime(value);
        //        case "System.Nullable`1[System.DateTime]":
        //            return ToNullableDateTime(value);

        //        case "System.Guid":
        //            return ToGuid(value);
        //        case "System.Nullable`1[System.Guid]":
        //            return ToNullableGuid(value);

        //        case "System.Boolean":
        //            return ToBoolean(value);
        //        case "System.Nullable`1[System.Boolean]":
        //            return ToNullableBoolean(value);

        //        case "System.Byte":
        //            return ToByte(value);
        //        case "System.Nullable`1[System.Byte]":
        //            return ToNullableByte(value);

        //        case "System.SByte":
        //            return ToSByte(value);
        //        case "System.Nullable`1[System.SByte]":
        //            return ToNullableSByte(value);

        //        case "System.Int16":
        //            return ToInt16(value);
        //        case "System.Nullable`1[System.Int16]":
        //            return ToNullableInt16(value);

        //        case "System.UInt16":
        //            return ToUInt16(value);
        //        case "System.Nullable`1[System.UInt16]":
        //            return ToNullableUInt16(value);

        //        case "System.Int32":
        //            return ToInt32(value);
        //        case "System.Nullable`1[System.Int32]":
        //            return ToNullableInt32(value);

        //        case "System.Nullable`1[System.UInt32]":
        //            return ToNullableUInt32(value);
        //        case "System.UInt32":
        //            return ToUInt32(value);

        //        case "System.Int64":
        //            return ToInt64(value);
        //        case "System.Nullable`1[System.Int64]":
        //            return ToNullableInt64(value);

        //        case "System.UInt64":
        //            return ToUInt64(value);
        //        case "System.Nullable`1[System.UInt64]":
        //            return ToNullableUInt64(value);

        //        case "System.Decimal":
        //            return ToDecimal(value);
        //        case "System.Nullable`1[System.Decimal]":
        //            return ToNullableDecimal(value);

        //        case "System.Double":
        //            return ToDouble(value);
        //        case "System.Nullable`1[System.Double]":
        //            return ToNullableDouble(value);

        //        case "System.Single":
        //            return ToSingle(value);
        //        case "System.Nullable`1[System.Single]":
        //            return ToNullableSingle(value);

        //        default:
        //            throw new ArgumentException(@"Can't find type in switch statement", "conversionType");
        //    }
        //}

        public static object ConvertType(object value, Type conversionType)
        {
            if (conversionType == typeof(string))
                return Convert.ToString(value);

            if (conversionType == typeof(char))
                return ToChar(value);
            if (conversionType == typeof(char?))
                return ToNullableChar(value);

            if (conversionType == typeof(DateTime))
                return ToDateTime(value);
            if (conversionType == typeof(DateTime?))
                return ToNullableDateTime(value);

            if (conversionType == typeof(bool))
                return ToBoolean(value);
            if (conversionType == typeof(bool?))
                return ToNullableBoolean(value);

            if (conversionType == typeof(byte))
                return ToByte(value);
            if (conversionType == typeof(byte?))
                return ToNullableByte(value);

            if (conversionType == typeof(sbyte))
                return ToSByte(value);
            if (conversionType == typeof(sbyte?))
                return ToNullableSByte(value);

            if (conversionType == typeof(short))
                return ToInt16(value);
            if (conversionType == typeof(short?))
                return ToNullableInt16(value);

            if (conversionType == typeof(ushort))
                return ToUInt16(value);
            if (conversionType == typeof(ushort?))
                return ToNullableUInt16(value);

            if (conversionType == typeof(int))
                return ToInt32(value);
            if (conversionType == typeof(int?))
                return ToNullableInt32(value);

            if (conversionType == typeof(uint))
                return ToUInt32(value);
            if (conversionType == typeof(uint?))
                return ToNullableUInt32(value);

            if (conversionType == typeof(long))
                return ToInt64(value);
            if (conversionType == typeof(long?))
                return ToNullableInt64(value);

            if (conversionType == typeof(ulong))
                return ToUInt64(value);
            if (conversionType == typeof(ulong?))
                return ToNullableUInt64(value);

            if (conversionType == typeof(decimal))
                return ToDecimal(value);
            if (conversionType == typeof(decimal?))
                return ToNullableDecimal(value);

            if (conversionType == typeof(double))
                return ToDouble(value);
            if (conversionType == typeof(double?))
                return ToNullableDouble(value);

            if (conversionType == typeof(float))
                return ToSingle(value);
            if (conversionType == typeof(float?))
                return ToNullableSingle(value);


            throw new ArgumentException(@"Can't find type in switch statement", nameof(conversionType));

        }






        public static char? ToNullableChar(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return ToChar(value);
        }
        public static char ToChar(object value)
        {
            return value.IsNullOrDefault() ? char.MinValue : Convert.ToChar(value);
        }

        public static Guid? ToNullableGuid(object value)
        {
            if (value.IsNullOrDefault()) return null;

            Guid? result;
            if (value is Guid)
            {
                result = (Guid)value;
            }
            else
                result = new Guid(value.ToString());
            return result != Guid.Empty ? result : null;
        }
        public static Guid ToGuid(object value)
        {
            if (value.IsNullOrDefault()) return Guid.Empty;

            if (value is Guid)
                return (Guid)value;

            return new Guid(value.ToString());
        }

        public static DateTime? ToNullableDateTime(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return ToDateTime(value);
        }
        public static DateTime ToDateTime(object value, bool parse = false)
        {
            if (value.IsNullOrDefault()) return DateTimeExtensions.MinDate;

            if (parse && value is string)
                return ((string)value).ParseDateTime();

            var val = Convert.ToDateTime(value);
            if (val < DateTimeExtensions.MinDate) return DateTimeExtensions.MinDate;
            if (val > DateTimeExtensions.MaxDate) return DateTimeExtensions.MaxDate;

            return val;
        }


        public static string ToString(object value)
        {
            if (value.IsNullOrDefault()) return string.Empty;

            return Convert.ToString(value);
        }
        public static string ToNullableString(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return Convert.ToString(value);
        }

        public static bool? ToNullableBoolean(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return Convert.ToBoolean(value);
        }
        public static bool ToBoolean(object value)
        {
            return !value.IsNullOrDefault() && Convert.ToBoolean(value);
        }

        public static byte? ToNullableByte(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return Convert.ToByte(value);
        }
        public static byte ToByte(object value)
        {
            return value.IsNullOrDefault() ? (byte)0 : Convert.ToByte(value);
        }

        public static sbyte? ToNullableSByte(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return Convert.ToSByte(value);
        }
        public static sbyte ToSByte(object value)
        {
            return value.IsNullOrDefault() ? (sbyte)0 : Convert.ToSByte(value);
        }

        public static short? ToNullableInt16(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return Convert.ToInt16(value);
        }
        public static short ToInt16(object value)
        {
            return value.IsNullOrDefault() ? (short)0 : Convert.ToInt16(value);
        }

        public static ushort? ToNullableUInt16(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return Convert.ToUInt16(value);
        }
        public static ushort ToUInt16(object value)
        {
            return value.IsNullOrDefault() ? (ushort)0 : Convert.ToUInt16(value);
        }

        public static int? ToNullableInt32(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return Convert.ToInt32(value);
        }
        public static int ToInt32(object value)
        {
            return value.IsNullOrDefault() ? 0 : Convert.ToInt32(value);
        }

        public static uint? ToNullableUInt32(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return Convert.ToUInt32(value);
        }
        public static uint ToUInt32(object value)
        {
            return value.IsNullOrDefault() ? 0U : Convert.ToUInt32(value);
        }

        public static long? ToNullableInt64(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return Convert.ToInt64(value);
        }
        public static long ToInt64(object value)
        {
            return value.IsNullOrDefault() ? 0L : Convert.ToInt64(value);
        }


        public static ulong? ToNullableUInt64(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return Convert.ToUInt64(value);
        }
        public static ulong ToUInt64(object value)
        {
            return value.IsNullOrDefault() ? 0 : Convert.ToUInt64(value);
        }


        public static decimal? ToNullableDecimal(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return ToDecimal(value);
        }
        public static decimal ToDecimal(object value, bool parse = false)
        {
            if (value.IsNullOrDefault()) return 0m;

            if (parse && value is string)
                return ((string)value).ParseDecimal();

            return Convert.ToDecimal(value);
        }


        public static double? ToNullableDouble(object value)
        {
            if (value.IsNullOrDefault()) return null;

            return ToDouble(value);
        }
        public static double ToDouble(object value, bool parse = false)
        {
            if (value.IsNullOrDefault()) return 0d;

            if (parse && value is string)
                return ((string)value).ParseDouble();

            return Convert.ToDouble(value);
        }


        public static float? ToNullableSingle(object value)
        {
            if (value.IsNullOrDefault()) return null;
            return ToSingle(value);
        }
        public static float ToSingle(object value, bool parse = false)
        {
            if (value.IsNullOrDefault()) return 0f;

            if (parse && value is string)
                return ((string)value).ParseSingle();

            return Convert.ToSingle(value);
        }
    }
}