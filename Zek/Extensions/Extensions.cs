using System;
using System.Collections.Generic;
using System.Globalization;
using System.Collections;

namespace Zek.Extensions
{
    //[DebuggerStepThrough]
    public static class Extensions
    {
        public const string UniversalDateFormat = "yyyy-MM-dd";

        public const string UniversalDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public const string UniversalDateTimeMillisecondFormat = "yyyy-MM-dd HH:mm:ss.fff";

        /// <summary>
        /// Checks if object is null or DBNull.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null || obj == DBNull.Value;
        }
        /// <summary>
        /// Checks if object is not null and DBNull.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return !IsNull(obj);
        }

        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, DBNull, string.Empty)
        /// </summary>
        /// <param name="obj">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this object obj)
        {
            return (obj == null) ||
                   (obj == DBNull.Value) ||
                   (obj is string && ((string)obj).Length == 0) ||

                   (obj is byte && (byte)obj == 0) ||
                   (obj is short && (short)obj == 0) ||
                   (obj is int && (int)obj == 0) ||
                   (obj is long && (long)obj == 0L) ||

                   (obj is decimal && (decimal)obj == 0M) ||
                   (obj is double && (double)obj == 0D) ||
                   (obj is float && (float)obj == 0F) ||

                   (obj is sbyte && (sbyte)obj == 0) ||
                   (obj is ushort && (ushort)obj == 0) ||
                   (obj is uint && (uint)obj == 0U) ||
                   (obj is ulong && (ulong)obj == 0UL) ||

                   (obj is Guid && (Guid)obj == Guid.Empty) ||
                   (obj is Array && ((Array)obj).Length == 0) ||
                   (obj is IList && ((IList)obj).Count == 0);
        }
        public static bool IsNotNullAndDefault(this object obj)
        {
            return !IsNullOrDefault(obj);
        }

        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 0)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this byte? value)
        {
            return value == null || value == 0;
        }
        public static bool IsNotNullAndDefault(this byte? value)
        {
            return !IsNullOrDefault(value);
        }

        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 0)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this short? value)
        {
            return value == null || value == 0;
        }
        public static bool IsNotNullAndDefault(this short? value)
        {
            return !IsNullOrDefault(value);
        }

        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 0)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this int? value)
        {
            return value == null || value == 0;
        }
        public static bool IsNotNullAndDefault(this int? value)
        {
            return !IsNullOrDefault(value);
        }
        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 0)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this long? value)
        {
            return value == null || value == 0L;
        }
        public static bool IsNotNullAndDefault(this long? value)
        {
            return !IsNullOrDefault(value);
        }
        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 0)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this decimal? value)
        {
            return value == null || value == 0M;
        }
        public static bool IsNotNullAndDefault(this decimal? value)
        {
            return !IsNullOrDefault(value);
        }
        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 0)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this double? value)
        {
            return value == null || value == 0D;
        }
        public static bool IsNotNullAndDefault(this double? value)
        {
            return !IsNullOrDefault(value);
        }
        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 0)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this float? value)
        {
            return value == null || value == 0F;
        }
        public static bool IsNotNullAndDefault(this float? value)
        {
            return !IsNullOrDefault(value);
        }
        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 0)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this sbyte? value)
        {
            return value == null || value == 0;
        }
        public static bool IsNotNullAndDefault(this sbyte? value)
        {
            return !IsNullOrDefault(value);
        }
        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 0)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this ushort? value)
        {
            return value == null || value == 0;
        }
        public static bool IsNotNullAndDefault(this ushort? value)
        {
            return !IsNullOrDefault(value);
        }
        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 0)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this uint? value)
        {
            return value == null || value == 0;
        }
        public static bool IsNotNullAndDefault(this uint? value)
        {
            return !IsNullOrDefault(value);
        }
        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 0)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this ulong? value)
        {
            return value == null || value == 0L;
        }
        public static bool IsNotNullAndDefault(this ulong? value)
        {
            return !IsNullOrDefault(value);
        }
        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 1900-01-01)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this DateTime? value)
        {
            return value == null || value == DateTimeExtensions.MinDate;
        }
        public static bool IsNotNullAndDefault(this DateTime? value)
        {
            return !IsNullOrDefault(value);
        }
        /// <summary>
        /// ამოწმებს მნიშვნელობა არის თუ არა ცარიელი. (null, 00000000-0000-0000-0000-000000000000)
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრი ცარიელია.</returns>
        public static bool IsNullOrDefault(this Guid? value)
        {
            return value == null || value == Guid.Empty;
        }
        public static bool IsNotNullAndDefault(this Guid? value)
        {
            return !IsNullOrDefault(value);
        }

        /// <summary>
        /// Checks if value is null or default (0, string.Empty, Guid.Empty...)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object NullIfDefault(this object obj)
        {
            return IsNullOrDefault(obj) ? null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static object NullIfDefault(this object obj, object defaultValue)
        {
            return obj.IsNull() || obj == defaultValue ? null : obj;
        }



        /// <summary>
        /// Checks if value is defaultValue and returns new value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ValueIfDefault(this byte obj, byte value, byte defaultValue = 0)
        {
            return obj == defaultValue ? value : obj;
        }
        /// <summary>
        /// Checks if value is defaultValue and returns new value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static sbyte ValueIfDefault(this sbyte obj, sbyte value, sbyte defaultValue = 0)
        {
            return obj == defaultValue ? value : obj;
        }
        /// <summary>
        /// Checks if value is defaultValue and returns new value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ValueIfDefault(this short obj, short value, short defaultValue = 0)
        {
            return obj == defaultValue ? value : obj;
        }
        /// <summary>
        /// Checks if value is defaultValue and returns new value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ushort ValueIfDefault(this ushort obj, ushort value, ushort defaultValue = 0)
        {
            return obj == defaultValue ? value : obj;
        }
        /// <summary>
        /// Checks if value is defaultValue and returns new value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ValueIfDefault(this int obj, int value, int defaultValue = 0)
        {
            return obj == defaultValue ? value : obj;
        }
        /// <summary>
        /// Checks if value is defaultValue and returns new value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ValueIfDefault(this uint obj, uint value, uint defaultValue = 0U)
        {
            return obj == defaultValue ? value : obj;
        }
        /// <summary>
        /// Checks if value is defaultValue and returns new value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ValueIfDefault(this long obj, long value, long defaultValue = 0L)
        {
            return obj == defaultValue ? value : obj;
        }
        /// <summary>
        /// Checks if value is defaultValue and returns new value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ValueIfDefault(this ulong obj, ulong value, ulong defaultValue = 0UL)
        {
            return obj == defaultValue ? value : obj;
        }
        /// <summary>
        /// Checks if value is defaultValue and returns new value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ValueIfDefault(this decimal obj, decimal value, decimal defaultValue = 0M)
        {
            return obj == defaultValue ? value : obj;
        }
        /// <summary>
        /// Checks if value is defaultValue and returns new value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ValueIfDefault(this double obj, double value, double defaultValue = 0D)
        {
            return obj == defaultValue ? value : obj;
        }
        /// <summary>
        /// Checks if value is defaultValue and returns new value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ValueIfDefault(this float obj, float value, float defaultValue = 0F)
        {
            return obj == defaultValue ? value : obj;
        }



        public static string NullIfDefault(this string obj, string defaultValue = "")
        {
            return obj == defaultValue ? null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte? NullIfDefault(this byte obj, byte defaultValue = 0)
        {
            return obj == defaultValue ? (byte?)null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short? NullIfDefault(this short obj, short defaultValue = 0)
        {
            return obj == defaultValue ? (short?)null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int? NullIfDefault(this int obj, int defaultValue = 0)
        {
            return obj == defaultValue ? (int?)null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long? NullIfDefault(this long obj, long defaultValue = 0L)
        {
            return obj == defaultValue ? (long?)null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static sbyte? NullIfDefault(this sbyte obj, sbyte defaultValue = 0)
        {
            return obj == defaultValue ? (sbyte?)null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ushort? NullIfDefault(this ushort obj, ushort defaultValue = 0)
        {
            return obj == defaultValue ? (ushort?)null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint? NullIfDefault(this uint obj, uint defaultValue = 0U)
        {
            return obj == defaultValue ? (uint?)null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong? NullIfDefault(this ulong obj, ulong defaultValue = 0UL)
        {
            return obj == defaultValue ? (ulong?)null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal? NullIfDefault(this decimal obj, decimal defaultValue = 0M)
        {
            return obj == defaultValue ? (decimal?)null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double? NullIfDefault(this double obj, double defaultValue = 0D)
        {
            return obj == defaultValue ? (double?)null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float? NullIfDefault(this float obj, float defaultValue = 0F)
        {
            return obj == defaultValue ? (float?)null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte? NullIfDefault(this byte? obj, byte defaultValue = 0)
        {
            return obj == null || obj == defaultValue ? null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short? NullIfDefault(this short? obj, short defaultValue = 0)
        {
            return obj == null || obj == defaultValue ? null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int? NullIfDefault(this int? obj, int defaultValue = 0)
        {
            return obj == null || obj == defaultValue ? null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long? NullIfDefault(this long? obj, long defaultValue = 0L)
        {
            return obj == null || obj == defaultValue ? null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static sbyte? NullIfDefault(this sbyte? obj, sbyte defaultValue = 0)
        {
            return obj == null || obj == defaultValue ? null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ushort? NullIfDefault(this ushort? obj, ushort defaultValue = 0)
        {
            return obj == null || obj == defaultValue ? null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint? NullIfDefault(this uint? obj, uint defaultValue = 0U)
        {
            return obj == null || obj == defaultValue ? null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong? NullIfDefault(this ulong? obj, ulong defaultValue = 0UL)
        {
            return obj == null || obj == defaultValue ? null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal? NullIfDefault(this decimal? obj, decimal defaultValue = 0M)
        {
            return obj == null || obj == defaultValue ? null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double? NullIfDefault(this double? obj, double defaultValue = 0D)
        {
            return obj == null || obj == defaultValue ? null : obj;
        }
        /// <summary>
        /// Checks if value is null or defaultValue
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float? NullIfDefault(this float? obj, float defaultValue = 0F)
        {
            return obj == null || obj == defaultValue ? null : obj;
        }

        public static bool DefaultIfNull(this bool? obj, bool defaultValue = false)
        {
            return obj ?? defaultValue;
        }
        public static byte DefaultIfNull(this byte? obj, byte defaultValue = 0)
        {
            return obj ?? defaultValue;
        }
        public static short DefaultIfNull(this short? obj, short defaultValue = 0)
        {
            return obj ?? defaultValue;
        }
        public static int DefaultIfNull(this int? obj, int defaultValue = 0)
        {
            return obj ?? defaultValue;
        }
        public static long DefaultIfNull(this long? obj, long defaultValue = 0L)
        {
            return obj ?? defaultValue;
        }
        public static sbyte DefaultIfNull(this sbyte? obj, sbyte defaultValue = 0)
        {
            return obj ?? defaultValue;
        }
        public static ushort DefaultIfNull(this ushort? obj, ushort defaultValue = 0)
        {
            return obj ?? defaultValue;
        }
        public static uint DefaultIfNull(this uint? obj, uint defaultValue = 0U)
        {
            return obj ?? defaultValue;
        }
        public static ulong DefaultIfNull(this ulong? obj, ulong defaultValue = 0L)
        {
            return obj ?? defaultValue;
        }
        public static decimal DefaultIfNull(this decimal? obj, decimal defaultValue = 0M)
        {
            return obj ?? defaultValue;
        }
        public static double DefaultIfNull(this double? obj, double defaultValue = 0D)
        {
            return obj ?? defaultValue;
        }
        public static float DefaultIfNull(this float? obj, float defaultValue = 0F)
        {
            return obj ?? defaultValue;
        }
        public static DateTime DefaultIfNull(this DateTime? obj, DateTime defaultValue = default(DateTime))
        {
            return obj ?? defaultValue;
        }







        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToIfNullEmptyString(this object obj)
        {
            return obj?.ToString() ?? string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToNullableString(this object obj)
        {
            return obj?.ToString();
        }




        /// <summary>
        /// იღებს Bool-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            switch (str.ToUpperInvariant())
            {
                case "TRUE":
                case "Y":
                case "YES":
                case "1":
                case "ON":
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// იღებს Byte-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(this string str, byte defaultValue = 0)
        {
            //str = str.IfNullEmpty();
            byte result;
            return byte.TryParse(str, out result) ? result : defaultValue;
        }
        /// <summary>
        /// იღებს Int16-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ToInt16(this string str, short defaultValue = 0)
        {
            //str = str.IfNullEmpty();
            short result;
            return short.TryParse(str, out result) ? result : defaultValue;
        }
        /// <summary>
        /// იღებს Int32-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt32(this string str, int defaultValue = 0)
        {
            //str = str.IfNullEmpty();
            int result;
            return int.TryParse(str, out result) ? result : defaultValue;
        }
        /// <summary>
        /// იღებს Int64-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToInt64(this string str, long defaultValue = 0)
        {
            //str = str.IfNullEmpty();
            long result;
            return long.TryParse(str, out result) ? result : defaultValue;
        }
        /// <summary>
        /// იღებს ToDecimal-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defaultValue = 0M)
        {
            //str = str.IfNullEmpty();
            decimal result;
            return decimal.TryParse(str, out result) ? result : defaultValue;
        }
        /// <summary>
        /// იღებს Double-ს ტექსტიდან
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defaultValue = 0D)
        {
            //str = str.IfNullEmpty();
            double result;
            return double.TryParse(str, out result) ? result : defaultValue;
        }

        /// <summary>
        /// Gets GUID value from string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string str, Guid defaultValue = default(Guid))
        {
            //str = str.IfNullEmpty();
            Guid result;
            return Guid.TryParse(str, out result) ? result : defaultValue;
            //var result = Guid.Empty;
            //try
            //{
            //    result = new Guid(str);
            //}
            //catch { }
            //return result;
        }
        /// <summary>
        ///  Gets DateTime value from string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, DateTime? defaultValue = null)
        {
            if (defaultValue == null)
                defaultValue = DateTimeExtensions.MinDate;
            //str = str.IfNullEmpty();
            DateTime result;
            return DateTime.TryParse(str, out result) ? result : defaultValue.Value;
        }


        public static bool ToBoolean(this string str, string error)
        {
            var result = ToNullableBoolean(str);
            if (result.HasValue) return result.Value;
            throw new FormatException(error);
        }
        public static byte ToByte(this string str, string error)
        {
            byte result;
            if (byte.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static short ToInt16(this string str, string error)
        {
            short result;
            if (short.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static int ToInt32(this string str, string error)
        {
            int result;
            if (int.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static long ToInt64(this string str, string error)
        {
            long result;
            if (long.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static float ToSingle(this string str, string error)
        {
            float result;
            if (float.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static double ToDouble(this string str, string error)
        {
            double result;
            if (double.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static decimal ToDecimal(this string str, string error)
        {
            decimal result;
            if (decimal.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static Guid ToGuid(this string str, string error)
        {
            str = str.IfNullEmpty();
            Guid result;

            if (Guid.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }
        public static DateTime ToDateTime(this string str, string error)
        {
            str = str.IfNullEmpty();

            DateTime result;
            if (DateTime.TryParse(str, out result))
                return result;

            throw new FormatException(error);
        }


        public static bool? ToNullableBoolean(this string str)
        {
            str = str.IfNullEmpty().ToUpperInvariant();
            switch (str)
            {
                case "TRUE":
                case "YES":
                case "1":
                case "ON":
                    return true;

                case "FALSE":
                case "NO":
                case "0":
                case "OFF":
                    return false;
            }
            return null;
        }
        public static byte? ToNullableByte(this string str)
        {
            byte result;
            return byte.TryParse(str, out result) ? (byte?)result : null;
        }
        public static short? ToNullableInt16(this string str)
        {
            short result;
            return short.TryParse(str, out result) ? (short?)result : null;
        }
        public static int? ToNullableInt32(this string str)
        {
            int result;
            return int.TryParse(str, out result) ? (int?)result : null;
        }
        public static long? ToNullableInt64(this string str)
        {
            long result;
            return long.TryParse(str, out result) ? (long?)result : null;
        }
        public static float? ToNullableSingle(this string str)
        {
            float result;
            return float.TryParse(str, out result) ? (float?)result : null;
        }
        public static double? ToNullableDouble(this string str)
        {
            double result;
            return double.TryParse(str, out result) ? (double?)result : null;
        }
        public static decimal? ToNullableDecimal(this string str)
        {
            decimal result;
            return decimal.TryParse(str, out result) ? (decimal?)result : null;
        }
        public static Guid? ToNullableGuid(this string str)
        {
            Guid result;
            return Guid.TryParse(str, out result) ? (Guid?)result : null;
        }
        public static DateTime? ToNullableDateTime(this string str)
        {
            DateTime result;
            return DateTime.TryParse(str, out result) ? (DateTime?)result : null;
        }


        /// <summary>
        /// იღებს GroupSymbol=მძიმე და DecimalSymbol-ს Replace-ს უკეთებს წერტილზე.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string PrepareNumber(this string str)
        {
            str = str.Replace(" ", string.Empty).Replace(" ", string.Empty);//Remove chars: 32 and 160
            return str.Contains(",") && !str.Contains(".") ? str.Replace(",", ".") : str;
        }
        /// <summary>
        /// Trying to parse text to decimal in.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ParseDecimal(this string str, decimal defaultValue = 0M)
        {
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;

            decimal result;
            str = str.PrepareNumber();

            // Try parsing using the user's culture
            if (decimal.TryParse(str, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result))
            {
            }
            // Parse using an invariant culture
            else if (decimal.TryParse(str, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out result))
            {
            }
            else
            {
                return defaultValue;
            }

            return result;
        }
        /// <summary>
        /// Trying to parse text to double in.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ParseDouble(this string str, double defaultValue = 0D)
        {
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;

            double result;
            str = str.PrepareNumber();

            // Try parsing using the user's culture
            if (double.TryParse(str, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result))
            {
            }
            // Parse using an invariant culture
            else if (double.TryParse(str, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out result))
            {
            }
            else
            {
                return defaultValue;
            }

            return result;
        }
        /// <summary>
        /// Trying to parse text to float in.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ParseSingle(this string str, float defaultValue = 0F)
        {
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;

            float result;
            str = str.PrepareNumber();
            // Try parsing using the user's culture
            if (float.TryParse(str, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result))
            {
            }
            // Parse using an invariant culture
            else if (float.TryParse(str, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out result))
            {
            }
            else
            {
                return defaultValue;
            }

            return result;
        }

        /// <summary>
        /// Trying to parse text to DateTime in.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(this string str, DateTime defaultValue = new DateTime())
        {
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;

            DateTime result;
            // Try parsing using the user's culture
            if (DateTime.TryParse(str, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result))
            {
            }
            // Parse using an invariant culture
            else if (DateTime.TryParse(str, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out result))
            {
            }
            else
            {
                return defaultValue;
            }

            return result;
        }
        /// <summary>
        /// Trying to parse uiversal text to DateTime in.zzzz
        /// </summary>
        /// <param name="str">yyyy-MM-dd HH:mm:ss.fff, yyyy-MM-dd HH:mm:ss, yyyy-MM-dd HH:mm, yyyy-MM-dd</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ParseUniversalDateTime(this string str, DateTime defaultValue = new DateTime())
        {
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;

            DateTime date;
            if (DateTime.TryParseExact(str, new[] { UniversalDateTimeMillisecondFormat, UniversalDateTimeFormat, "yyyy-MM-dd HH:mm", UniversalDateFormat }, null, DateTimeStyles.None, out date))
                return date;

            return defaultValue;
        }
        /// <summary>
        /// Trying to parse uiversal text to DateTime in.zzzz
        /// </summary>
        /// <param name="str">yyyy-MM-dd HH:mm:ss.fff, yyyy-MM-dd HH:mm:ss, yyyy-MM-dd HH:mm, yyyy-MM-dd</param>
        /// <returns></returns>
        public static DateTime? ParseUniversalNullableDateTime(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;

            DateTime date;
            if (DateTime.TryParseExact(str, new[] { UniversalDateTimeMillisecondFormat, UniversalDateTimeFormat, "yyyy-MM-dd HH:mm", UniversalDateFormat }, null, DateTimeStyles.None, out date))
                return date;

            return null;
        }


        public static string ToUniversalDateTimeMillisecondString(this DateTime? date)
        {
            return date != null ? ToUniversalDateTimeMillisecondString(date.Value) : null;
        }
        public static string ToUniversalDateTimeMillisecondString(this DateTime date)
        {
            return date.ToString(UniversalDateTimeMillisecondFormat);
        }
        public static string ToUniversalDateTimeString(this DateTime? date)
        {
            return date != null ? ToUniversalDateTimeString(date.Value) : null;
        }
        public static string ToUniversalDateTimeString(this DateTime date)
        {
            return date.ToString(UniversalDateTimeFormat);
        }
        public static string ToUniversalDateString(this DateTime? date)
        {
            return date != null ? ToUniversalDateString(date.Value) : null;
        }
        public static string ToUniversalDateString(this DateTime date)
        {
            return date.ToString(UniversalDateFormat);
        }


        public static T? DefaultToNull<T>(this T value) where T : struct
        {
            return EqualityComparer<T>.Default.Equals(default(T), value) ? (T?)null : value;
        }

        public static T? DefaultToNull<T>(this T value, T defaultValue) where T : struct
        {
            return EqualityComparer<T>.Default.Equals(defaultValue, value) ? (T?)null : value;
        }

        public static int? NotFoundToNull(this int value)
        {
            return value == -1 ? null : (int?)value;
        }

        public static int NotFound(this int value, int defaultValue)
        {
            return value == -1 ? defaultValue : value;
        }

        public static T ThrowIfNull<T>(this T? t, string message) where T : struct
        {
            if (t == null)
                throw new NullReferenceException(message);
            return t.Value;
        }

        public static T ThrowIfNull<T>(this T t, string message) where T : class
        {
            if (t == null)
                throw new NullReferenceException(message);
            return t;
        }
        //public static T ThrowIfNullStruct<T>(this T? t, string message) where T : struct
        //{
        //    if (t == null)
        //        throw new NullReferenceException(message);
        //    return t.Value;
        //}

        //public static T ThrowIfNullClass<T>(this T t, string message) where T : class
        //{
        //    if (t == null)
        //        throw new NullReferenceException(message);
        //    return t;
        //}

        public static string TryToString(this object obj)
        {
            return obj?.ToString();
        }

        public static string TryToString(this IFormattable obj, string format)
        {
            return obj?.ToString(format, CultureInfo.CurrentCulture);
        }

        /*
        #region Map Try Do TryDo
        public static R Map<T, R>(this T t, Func<T, R> func)
        {
            return func(t);
        }

        public static R TryCC<T, R>(this T t, Func<T, R> func)
            where T : class
            where R : class
        {
            if (t == null) return null;
            return func(t);
        }

        public static R? TryCS<T, R>(this T t, Func<T, R> func)
            where T : class
            where R : struct
        {
            if (t == null) return null;
            return func(t);
        }

        public static R? TryCS<T, R>(this T t, Func<T, R?> func)
            where T : class
            where R : struct
        {
            if (t == null) return null;
            return func(t);
        }

        public static R TrySC<T, R>(this T? t, Func<T, R> func)
            where T : struct
            where R : class
        {
            if (t == null) return null;
            return func(t.Value);
        }

        public static R? TrySS<T, R>(this T? t, Func<T, R> func)
            where T : struct
            where R : struct
        {
            if (t == null) return null;
            return func(t.Value);
        }

        public static R? TrySS<T, R>(this T? t, Func<T, R?> func)
            where T : struct
            where R : struct
        {
            if (t == null) return null;
            return func(t.Value);
        }


        public static T Do<T>(this T t, Action<T> action)
        {
            action(t);
            return t;
        }

        public static T TryDoC<T>(this T t, Action<T> action) where T : class
        {
            if (t != null)
                action(t);
            return t;
        }

        public static T? TryDoS<T>(this T? t, Action<T> action) where T : struct
        {
            if (t != null)
                action(t.Value);
            return t;
        }
        #endregion

        */
    }
}
