using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace Zek.Extensions.Data
{
    public static class DataReaderExtensions
    {
        /// <summary>
        /// Checks if a column's value is DBNull
        /// </summary>
        /// <param name="dataReader">The data reader</param>
        /// <param name="columnName">The column name</param>
        /// <returns>A bool indicating if the column's value is DBNull</returns>
        public static bool IsDBNull(this IDataReader dataReader, string columnName)
        {
            return dataReader[columnName] == DBNull.Value;
        }

        /// <summary>
        /// Checks if a column exists in a data reader
        /// </summary>
        /// <param name="dataReader">The data reader</param>
        /// <param name="columnName">The column name</param>
        /// <returns>A bool indicating the column exists</returns>
        public static bool ContainsColumn(this IDataReader dataReader, string columnName)
        {
            try
            {
                return dataReader.GetOrdinal(columnName) >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public static IEnumerable<string> GetColumnNames(this IDataReader reader)
        {
            for (var i = 0; i < reader.FieldCount; i++)
                yield return reader.GetName(i);
        }
        
        /// <summary>
        /// Returns string from given column name, or null if DbNull.
        /// </summary>
        public static string GetString(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            if (columnValue == DBNull.Value)
            {
                return null;
            }
            return (string)columnValue;
        }


        /// <summary>
        /// Returns DateTime from given column name.
        /// </summary>
        public static DateTime GetDateTime(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            return Convert.ToDateTime(columnValue, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Returns DateTime from given column name, or null if DbNull.
        /// </summary>
        public static DateTime? GetNullableDateTime(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            if (columnValue == DBNull.Value)
            {
                return null;
            }
            return Convert.ToDateTime(columnValue, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns Guid from given column name.
        /// </summary>
        public static Guid GetGuid(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            return (Guid)columnValue;
        }
        /// <summary>
        /// Returns Guid from given column name, or null if DbNull.
        /// </summary>
        public static Guid? GetNullableGuid(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            if (columnValue == DBNull.Value)
            {
                return null;
            }
            return (Guid)columnValue;
        }

        /// <summary>
        /// Returns byte from given column name.
        /// </summary>
        public static byte GetByte(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            return Convert.ToByte(columnValue, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns byte from given column name, or null if DbNull.
        /// </summary>
        public static byte? GetNullableByte(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            if (columnValue == DBNull.Value)
            {
                return null;
            }
            return Convert.ToByte(columnValue, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns short from given column name.
        /// </summary>
        public static short GetInt16(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            return Convert.ToInt16(columnValue, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns short from given column name, or null if DbNull.
        /// </summary>
        public static short? GetNullableInt16(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            if (columnValue == DBNull.Value)
            {
                return null;
            }
            return Convert.ToInt16(columnValue, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns int from given column name.
        /// </summary>
        public static int GetInt32(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            return Convert.ToInt32(columnValue, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns int from given column name, or null if DbNull.
        /// </summary>
        public static int? GetNullableInt32(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            if (columnValue == DBNull.Value)
            {
                return null;
            }
            return Convert.ToInt32(columnValue, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns long from given column name.
        /// </summary>
        public static long GetInt64(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            return Convert.ToInt64(columnValue, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns long from given column name, or null if DbNull.
        /// </summary>
        public static long? GetNullableInt64(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            if (columnValue == DBNull.Value)
            {
                return null;
            }
            return Convert.ToInt64(columnValue, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns decimal from given column name.
        /// </summary>
        public static decimal GetDecimal(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            return Convert.ToDecimal(columnValue, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns decimal from given column name, or null if DbNull.
        /// </summary>
        public static decimal? GetNullableDecimal(this IDataReader reader, string columnName)
        {
            var columnValue = reader[columnName];
            if (columnValue == DBNull.Value)
            {
                return null;
            }
            return Convert.ToDecimal(columnValue, CultureInfo.InvariantCulture);
        }



        public static T GetValue<T>(this IDataReader dr, int columnIndex)
        {
            // Read the value out of the reader by string (column name); returns object
            var value = dr.GetValue(columnIndex);

            // Cast to the generic type applied to this method (i.e. int?)
            var theValueType = typeof(T);

            // Check for null value from the database
            if (DBNull.Value != value)
            {
                // We have a null, do we have a nullable type for T?
                if (!theValueType.IsNullableType())
                {
                    // No, this is not a nullable type so just change the value's type from object to T
                    return (T)Convert.ChangeType(value, theValueType);
                }

                // Yes, this is a nullable type so change the value's type from object to the underlying type of T
                return (T)Convert.ChangeType(value, new NullableConverter(theValueType).UnderlyingType);
            }

            // The value was null in the database, so return the default value for T; this will vary based on what T is (i.e. int has a default of 0)
            return default(T);
        }
        /// <summary>
        /// Returns the value, of type T, from the SqlDataReader, accounting for both generic and non-generic types.
        /// </summary>
        /// <typeparam name="T">T, type applied</typeparam>
        /// <param name="dr">The SqlDataReader object that queried the database</param>
        /// <param name="columnName">The column of data to retrieve a value from</param>
        /// <returns>T, type applied; default value of type if database value is null</returns>
        public static T GetValue<T>(this IDataReader dr, string columnName)
        {
            // Read the value out of the reader by string (column name); returns object
            var value = dr[columnName];

            // Cast to the generic type applied to this method (i.e. int?)
            var theValueType = typeof(T);

            // Check for null value from the database
            if (DBNull.Value != value)
            {
                // We have a null, do we have a nullable type for T?
                if (!theValueType.IsNullableType())
                {
                    // No, this is not a nullable type so just change the value's type from object to T
                    return (T)Convert.ChangeType(value, theValueType);
                }

                // Yes, this is a nullable type so change the value's type from object to the underlying type of T
                return (T)Convert.ChangeType(value, new NullableConverter(theValueType).UnderlyingType);
            }

            // The value was null in the database, so return the default value for T; this will vary based on what T is (i.e. int has a default of 0)
            return default(T);
        }
    }
}
