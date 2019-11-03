using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace Zek.Extensions
{
    public static class DBExtensions
    {
        /// <summary>
        /// აკონვერტირებს მნიშვნელობას პარამეტრისთვის გადასაცემ მნიშვნელობად.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ToDBValue(this object value)
        {
            return value ?? DBNull.Value;
        }

        public static string ToSQLValue(this DBNull value)
        {
            return "NULL";
        }

        public static string ToSQLValue(this DateTime? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this DateTime value)
        {
            return "'" + value.ToString("yyyy-MM-ddTHH:mm:ss.fff") + "'";
        }

        public static string ToSQLDateValue(this DateTime? value)
        {
            return value == null ? "NULL" : ToSQLDateValue(value.Value);
        }
        public static string ToSQLDateValue(this DateTime value)
        {
            return "'" + value.ToString("yyyy-MM-dd") + "'";
        }
        public static string ToSQLDateTime2Value(this DateTime? value)
        {
            return value == null ? "NULL" : ToSQLDateTime2Value(value.Value);
        }
        public static string ToSQLDateTime2Value(this DateTime value)
        {
            return "'" + value.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
        }

        public static string ToSQLValue(this char? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this char value)
        {
            return "N'" + value.ToString(CultureInfo.InvariantCulture).Replace("'", "''") + "'";
        }
        public static string ToSQLValue(this string value)
        {
            return value == null ? "NULL" : "N'" + value.Replace("'", "''") + "'";
        }

        public static string ToSQLValue(this Guid? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this Guid value)
        {
            return "'" + value + "'";
        }

        public static string ToSQLValue(this bool? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this bool value)
        {
            return value ? "1" : "0";
        }

        public static string ToSQLValue(this byte? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this byte value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSQLValue(this sbyte? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this sbyte value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSQLValue(this short? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this short value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSQLValue(this ushort? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this ushort value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSQLValue(this int? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this int value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSQLValue(this uint? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this uint value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSQLValue(this long? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this long value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSQLValue(this ulong? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this ulong value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSQLValue(this decimal? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this decimal value)
        {
            return value.ToString(null, NumberFormatInfo.InvariantInfo);
        }
        public static string ToSQLValue(this double? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this double value)
        {
            return value.ToString("R", NumberFormatInfo.InvariantInfo);
        }
        public static string ToSQLValue(this float? value)
        {
            return value == null ? "NULL" : ToSQLValue(value.Value);
        }
        public static string ToSQLValue(this float value)
        {
            return value.ToString("R", NumberFormatInfo.InvariantInfo);
        }



        /// <summary>
        /// აფორმატირებს მნიშვნელობას String-ში, ისე რომ Sql-თან არ შეიქმნას პრობლემა.
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის დაფორმატირებაც გვინდა.</param>
        /// <returns>დაფორმატირებული String.</returns>
        public static string ToSQLValue(this object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return "NULL";
            }

            switch (value.GetType().Name)
            {
                case "String":
                    return ToSQLValue((string)value);

                case "Char":
                    return ToSQLValue((char)value);

                case "DateTime":
                    return ToSQLValue((DateTime)value);

                case "Guid":
                    return ToSQLValue((Guid)value);

                case "DBNull":
                    return "NULL";

                case "Boolean":
                    return ToSQLValue((bool)value);

                    //case "SqlLiteral":
                    //    return ((Zek.Data.QueryBuilder.SqlLiteral)value).Value;
                    //    break;
                case "Byte":
                    return ToSQLValue((byte)value);

                case "SByte":
                    return ToSQLValue((sbyte)value);

                case "Int16":
                    return ToSQLValue((short)value);
                
                case "UInt16":
                    return ToSQLValue((ushort)value);

                case "Int32":
                    return ToSQLValue((int)value);

                case "UInt32":
                    return ToSQLValue((uint)value);

                case "Int64":
                    return ToSQLValue((long)value);

                case "UInt64":
                    return ToSQLValue((ulong)value);

                case "Decimal":
                    return ToSQLValue((decimal)value);

                case "Double":
                    return ToSQLValue((double)value);

                case "Single":
                    return ToSQLValue((float) value);

                case "Object[]":
                case "String[]":
                case "DateTime[]":
                case "Guid[]":
                case "Boolean[]":
                case "SqlLiteral[]":
                case "Decimal[]":
                case "Double[]":
                case "Single[]":
                case "Byte[]":
                case "Int16[]":
                case "Int32[]":
                case "Int64[]":
                case "Nullable`1[]":
                    var sbArray = new StringBuilder();
                    foreach (var item in (Array)value)
                    {
                        sbArray.Append(ToSQLValue(item) + ", ");
                    }
                    return sbArray.Remove(sbArray.Length - 2, 2).ToString();

                case "List`1":
                    var sbList = new StringBuilder();
                    foreach (var item in (IList)value)
                    {
                        sbList.Append(ToSQLValue(item) + ", ");
                    }
                    return sbList.Remove(sbList.Length - 2, 2).ToString();

                default:
                    return value.ToString();
            }
        }
        /// <summary>
        /// აფორმატირებს მნიშვნელობას String-ში, ისე რომ Sql-თან არ შეიქმნას პრობლემა.
        /// </summary>
        /// <param name="value">მნიშვნელობა, რომლის დაფორმატირებაც გვინდა.</param>
        /// <returns>დაფორმატირებული String.</returns>
        public static string ToEntitySQLValue(this object value)
        {
            var formattedValue = string.Empty;
            //string StringType = Type.GetType("string").Name;
            //string DateTimeType = Type.GetType("DateTime").Name;

            if (value == null)
            {
                formattedValue = "NULL";
            }
            else
            {
                switch (value.GetType().Name)
                {
                    case "String":
                    case "Char":
                        formattedValue = "N'" + ((string)value).Replace("'", "''") + "'";
                        break;

                    case "DateTime":
                        formattedValue = "DATETIME'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                        break;

                    case "Guid":
                        formattedValue = "GUID'" + value + "'";
                        break;

                    case "DBNull":
                        formattedValue = "NULL";
                        break;

                    case "Boolean":
                        formattedValue = (bool)value ? "1" : "0";
                        break;

                    case "Byte":
                        formattedValue = ((byte)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;
                    case "SByte":
                        formattedValue = ((sbyte)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;

                    case "Int16":
                        formattedValue = ((short)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;
                    case "UInt16":
                        formattedValue = ((ushort)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;

                    case "Int32":
                        formattedValue = ((int)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;
                    case "UInt32":
                        formattedValue = ((uint)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;

                    case "Int64":
                        formattedValue = ((long)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;
                    case "UInt64":
                        formattedValue = ((ulong)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;

                    //case "SqlLiteral":
                    //    formattedValue = ((Zek.Data.QueryBuilder.SqlLiteral)value).Value;
                    //    break;

                    case "Decimal":
                        formattedValue = ((decimal)value).ToString(null, NumberFormatInfo.InvariantInfo);
                        break;
                    case "Double":
                        formattedValue = ((double)value).ToString("R", NumberFormatInfo.InvariantInfo);
                        break;
                    case "Single":
                        formattedValue = ((float)value).ToString("R", NumberFormatInfo.InvariantInfo);
                        break;

                    case "Object[]":
                    case "String[]":
                    case "DateTime[]":
                    case "Guid[]":
                    case "Boolean[]":
                    case "SqlLiteral[]":
                    case "Decimal[]":
                    case "Double[]":
                    case "Single[]":
                    case "Byte[]":
                    case "Int16[]":
                    case "Int32[]":
                    case "Int64[]":
                        foreach (var item in (Array)value)
                        {
                            formattedValue += ToEntitySQLValue(item) + ", ";
                        }
                        formattedValue = formattedValue.Remove(formattedValue.Length - ", ".Length);
                        break;

                    default:
                        formattedValue = value.ToString();
                        break;
                }
            }
            return formattedValue;
        }

    }
}
