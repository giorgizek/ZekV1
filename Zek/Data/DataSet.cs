using System;
using System.Collections.Generic;

using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using Zek.IO;
using Zek.Extensions;

namespace Zek.Data
{
    [Serializable]
    public enum SortOrder
    {
        Ascending = 0,
        Descending = 1,
        Unspecified = -1
    }


    public static class DataSetExtensions
    {
        #region Column Array
        /// <summary>
        /// DataColumn[]-ბიდან იღებს დასახელებებს.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string[] GetColumnNameArray(this DataTable table)
        {
            return GetColumnNameArray(table.Columns);
        }
        /// <summary>
        /// DataColumn[]-ბიდან იღებს დასახელებებს.
        /// </summary>
        /// <param name="columnCollection"></param>
        /// <returns></returns>
        public static string[] GetColumnNameArray(this DataColumnCollection columnCollection)
        {
            var fieldNames = new string[columnCollection.Count];
            for (var i = 0; i < columnCollection.Count; i++)
                fieldNames[i] = columnCollection[i].ColumnName;
            return fieldNames;
        }
        /// <summary>
        /// DataColumn[]-ბიდან იღებს დასახელებებს.
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static string[] GetColumnNameArray(this DataColumn[] columns)
        {
            var fieldNames = new string[columns.Length];
            for (var i = 0; i < columns.Length; i++)
                fieldNames[i] = columns[i].ColumnName;
            return fieldNames;
        }
        /// <summary>
        /// DataColumnCollection-იდან გადაიყვანს მასივში DataColumn[]
        /// </summary>
        /// <param name="columnCollection"></param>
        /// <returns></returns>
        public static DataColumn[] GetColumnArray(this DataColumnCollection columnCollection)
        {
            var columns = new DataColumn[columnCollection.Count];
            for (var i = 0; i < columnCollection.Count; i++)
                columns[i] = columnCollection[i];
            return columns;
        }
        /// <summary>
        /// DataTable-იდან იღებს კოლუმების მასივს.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataColumn[] GetColumnArray(this DataTable table)
        {
            return GetColumnArray(table.Columns);
        }
        #endregion


        /// <summary>
        /// ამოწმებს DataRow წაშლილია თუ არა.
        /// </summary>
        /// <param name="row"></param>
        /// <returns>აბრუნებს true-ს როცა DataRow.RowState ტოლია Deleted ან Detached.</returns>
        public static bool IsDataRowDeleted(this DataRow row)
        {
            switch (row.RowState)
            {
                case DataRowState.Deleted:
                case DataRowState.Detached:
                    return true;
            }

            return false;
        }




        #region SelectDistinct, SelectSumGroupByDate
        public static DataTable SelectDistinct(this DataTable table, params DataColumn[] columns)
        {
            return SelectDistinct(table, false, columns);
        }
        public static DataTable SelectDistinct(this DataTable table, bool setdataTableName, params DataColumn[] columns)
        {
            return SelectDistinct(table, setdataTableName, GetColumnNameArray(columns));
        }
        public static DataTable SelectDistinct(this DataTable table, params string[] fieldNames)
        {
            return SelectDistinct(table, false, fieldNames);
        }
        public static DataTable SelectDistinct(this DataTable table, bool setdataTableName, params string[] fieldNames)
        {
            return SelectDistinct(table, setdataTableName, fieldNames, fieldNames);
        }

        public static DataTable SelectDistinct(this DataTable dataTable, bool setdataTableName, DataColumnCollection addColumns, params DataColumn[] columns)
        {
            return SelectDistinct(dataTable, setdataTableName, addColumns, GetColumnNameArray(columns));
        }
        public static DataTable SelectDistinct(this DataTable dataTable, bool setdataTableName, DataColumnCollection addColumns, params string[] fieldNames)
        {
            return SelectDistinct(dataTable, setdataTableName, GetColumnArray(addColumns), fieldNames);
        }
        public static DataTable SelectDistinct(this DataTable dataTable, bool setdataTableName, DataColumn[] addColumns, params DataColumn[] columns)
        {
            return SelectDistinct(dataTable, setdataTableName, addColumns, GetColumnNameArray(columns));
        }
        public static DataTable SelectDistinct(this DataTable dataTable, bool setdataTableName, DataColumn[] addColumns, params string[] fieldNames)
        {
            return SelectDistinct(dataTable, setdataTableName, GetColumnNameArray(addColumns), fieldNames);
        }
        public static DataTable SelectDistinct(this DataTable dataTable, bool setdataTableName, string[] addColumns, params string[] fieldNames)
        {
            var newTable = new DataTable();

            if (addColumns == null || addColumns.Length == 0)
                throw new ArgumentNullException(nameof(addColumns));
            if (fieldNames == null || fieldNames.Length == 0)
                throw new ArgumentNullException(nameof(fieldNames));

            var lastValues = new object[fieldNames.Length];
            if (setdataTableName)
                newTable.TableName = dataTable.TableName;

            foreach (var fieldName in addColumns)
            {
                if (fieldName == null || fieldName.Trim().Length == 0) continue;
                newTable.Columns.Add(fieldName, dataTable.Columns[fieldName].DataType);
            }

            var orderedRows = dataTable.Select("", string.Join(", ", fieldNames));

            foreach (var row in orderedRows)
            {
                if (!FieldValuesAreEqual(lastValues, row, fieldNames))
                {
                    newTable.Rows.Add(CreateRowClone(row, newTable.NewRow(), addColumns));
                    SetLastValues(lastValues, row, fieldNames);
                }
            }

            return newTable;
        }

        public static DataTable SelectSumGroupByDate(this DataTable table, DataColumn dateColumn, DataColumn decimalColumn)
        {
            return SelectSumGroupByDate(table, dateColumn.ColumnName, decimalColumn.ColumnName);
        }
        public static DataTable SelectSumGroupByDate(this DataTable table, bool setdataTableName, DataColumn dateColumn, DataColumn decimalColumn)
        {
            return SelectSumGroupByDate(table, setdataTableName, dateColumn.ColumnName, decimalColumn.ColumnName);
        }
        public static DataTable SelectSumGroupByDate(this DataTable table, bool setdataTableName, bool roundDate, DataColumn dateColumn, DataColumn decimalColumn)
        {
            return SelectSumGroupByDate(table, setdataTableName, roundDate, dateColumn.ColumnName, decimalColumn.ColumnName);
        }
        public static DataTable SelectSumGroupByDate(this DataTable table, string dateFieldName, string decimalFieldName)
        {
            return SelectSumGroupByDate(table, false, dateFieldName, decimalFieldName);
        }
        public static DataTable SelectSumGroupByDate(this DataTable table, bool setdataTableName, string dateFieldName, string decimalFieldName)
        {
            return SelectSumGroupByDate(table, setdataTableName, false, dateFieldName, decimalFieldName);
        }
        public static DataTable SelectSumGroupByDate(this DataTable table, bool setdataTableName, bool roundDate, string dateFieldName, string decimalFieldName)
        {
            var dict = new Dictionary<DateTime, decimal>();
            foreach (DataRow row in table.Rows)
            {
                if (row.IsDataRowDeleted()) continue;

                var date = roundDate ? ((DateTime)row[dateFieldName]).Date : (DateTime)row[dateFieldName];

                if (dict.ContainsKey(date))
                    dict[date] += (decimal)row[decimalFieldName];
                else
                    dict.Add(date, (decimal)row[decimalFieldName]);
            }

            var result = new DataTable();
            if (setdataTableName)
                result.TableName = table.TableName;
            result.Columns.Add(dateFieldName, typeof(DateTime));
            result.Columns.Add(decimalFieldName, typeof(decimal));

            foreach (var item in dict)
            {
                result.Rows.Add(item.Key, item.Value);
            }

            return result;
        }
        #endregion



        #region Sort
        /// <summary>
        /// ცხრილის დასორტირება.
        /// </summary>
        /// <typeparam name="T">ტიპიზირებული DataTable.</typeparam>
        /// <param name="dataTable">ცხრილი, რომლის დასორტირებაც გვინდა.</param>
        /// <param name="column">სვეტი, რის მიხედვითაც ვასორტირებთ.</param>
        /// <returns>აბრუნებს დასორტირებულ ცხრილს.</returns>
        public static T SortDataTable<T>(this T dataTable, DataColumn column) where T : DataTable
        {
            return SortDataTable(dataTable, column, SortOrder.Ascending);
        }
        /// <summary>
        /// ცხრილის დასორტირება.
        /// </summary>
        /// <typeparam name="T">ტიპიზირებული DataTable.</typeparam>
        /// <param name="dataTable">ცხრილი, რომლის დასორტირებაც გვინდა.</param>
        /// <param name="column">სვეტი, რის მიხედვითაც ვასორტირებთ.</param>
        /// <param name="sortOrder">დალაგების მეთოდი, ანბანზე ან უკუღმა.</param>
        /// <returns>აბრუნებს დასორტირებულ ცხრილს.</returns>
        public static T SortDataTable<T>(this T dataTable, DataColumn column, SortOrder sortOrder) where T : DataTable
        {
            return SortDataTable(dataTable, column.ColumnName, sortOrder);
        }
        /// <summary>
        /// ცხრილის დასორტირება.
        /// </summary>
        /// <typeparam name="T">ტიპიზირებული DataTable.</typeparam>
        /// <param name="dataTable">ცხრილი, რომლის დასორტირებაც გვინდა.</param>
        /// <param name="fieldName">სვეტის სახელი, რის მიხედვითაც ვასორტირებთ.</param>
        /// <returns>აბრუნებს დასორტირებულ ცხრილს.</returns>
        public static T SortDataTable<T>(this T dataTable, string fieldName) where T : DataTable
        {
            return SortDataTable(dataTable, fieldName, SortOrder.Ascending);
        }
        /// <summary>
        /// ცხრილის დასორტირება.
        /// </summary>
        /// <typeparam name="T">ტიპიზირებული DataTable.</typeparam>
        /// <param name="dataTable">ცხრილი, რომლის დასორტირებაც გვინდა.</param>
        /// <param name="fieldName">სვეტის სახელი, რის მიხედვითაც ვასორტირებთ.</param>
        /// <param name="sortOrder">დალაგების მეთოდი, ანბანზე ან უკუღმა.</param>
        /// <returns>აბრუნებს დასორტირებულ ცხრილს.</returns>
        public static T SortDataTable<T>(this T dataTable, string fieldName, SortOrder sortOrder) where T : DataTable
        {
            return SortDataTable(dataTable, fieldName, GetSortDirection(sortOrder));
        }
        /// <summary>
        /// ცხრილის დასორტირება.
        /// </summary>
        /// <typeparam name="T">ტიპიზირებული DataTable.</typeparam>
        /// <param name="dataTable">ცხრილი, რომლის დასორტირებაც გვინდა.</param>
        /// <param name="fieldName">სვეტის სახელი, რის მიხედვითაც ვასორტირებთ.</param>
        /// <param name="sortOrder">დალაგების მეთოდი, ანბანზე ან უკუღმა.</param>
        /// <returns>აბრუნებს დასორტირებულ ცხრილს.</returns>
        public static T SortDataTable<T>(this T dataTable, string fieldName, string sortOrder) where T : DataTable
        {
            var dataView = dataTable.DefaultView;
            dataView.Sort = $"[{fieldName}] {sortOrder}";

            var result = (T)dataTable.Clone();
            foreach (DataRowView row in dataView)
            {
                if (row.Row.IsDataRowDeleted()) continue;
                result.Rows.Add(row.Row.ItemArray);
            }
            return result;
        }



        /// <summary>
        /// ცხრილის დასორტირება.
        /// </summary>
        /// <param name="dataTable">ცხრილი, რომლის დასორტირებაც გვინდა.</param>
        /// <param name="column">სვეტი, რის მიხედვითაც ვასორტირებთ.</param>
        public static DataTable SortDataTable(this DataTable dataTable, DataColumn column)
        {
            return SortDataTable(dataTable, column, SortOrder.Ascending);
        }
        /// <summary>
        /// ცხრილის დასორტირება.
        /// </summary>
        /// <param name="dataTable">ცხრილი, რომლის დასორტირებაც გვინდა.</param>
        /// <param name="column">სვეტი, რის მიხედვითაც ვასორტირებთ.</param>
        /// <param name="sortOrder">დალაგების მეთოდი, ანბანზე ან უკუღმა.</param>
        public static DataTable SortDataTable(this DataTable dataTable, DataColumn column, SortOrder sortOrder)
        {
            return SortDataTable(dataTable, column.ColumnName, sortOrder);
        }
        /// <summary>
        /// ცხრილის დასორტირება.
        /// </summary>
        /// <param name="dataTable">ცხრილი, რომლის დასორტირებაც გვინდა.</param>
        /// <param name="fieldName">სვეტის სახელი, რის მიხედვითაც ვასორტირებთ.</param>
        public static DataTable SortDataTable(this DataTable dataTable, string fieldName)
        {
            return SortDataTable(dataTable, fieldName, SortOrder.Ascending);
        }
        /// <summary>
        /// ცხრილის დასორტირება.
        /// </summary>
        /// <param name="dataTable">ცხრილი, რომლის დასორტირებაც გვინდა.</param>
        /// <param name="fieldName">სვეტის სახელი, რის მიხედვითაც ვასორტირებთ.</param>
        /// <param name="sortOrder">დალაგების მეთოდი, ანბანზე ან უკუღმა.</param>
        public static DataTable SortDataTable(this DataTable dataTable, string fieldName, SortOrder sortOrder)
        {
            return SortDataTable(dataTable, fieldName, GetSortDirection(sortOrder));
        }
        /// <summary>
        /// ცხრილის დასორტირება.
        /// </summary>
        /// <param name="dataTable">ცხრილი, რომლის დასორტირებაც გვინდა.</param>
        /// <param name="fieldName">სვეტის სახელი, რის მიხედვითაც ვასორტირებთ.</param>
        /// <param name="sortOrder">დალაგების მეთოდი, ანბანზე ან უკუღმა.</param>
        public static DataTable SortDataTable(this DataTable dataTable, string fieldName, string sortOrder)
        {
            var dataView = dataTable.DefaultView;
            dataView.Sort = $"[{fieldName}] {sortOrder}";
            return dataView.ToTable();
        }
        /// <summary>
        /// აბრუნებს ASC ან DESC.
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        private static string GetSortDirection(SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    return "ASC";
                case SortOrder.Descending:
                    return "DESC";
                default:
                    return "ASC";
            }
        }
        #endregion


        #region Search

        /// <summary>
        /// მნიშვნელობის პოვნა DataTable-ში.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="sortColumn"></param>
        /// <param name="key">საძიებო მნიშვნელობა</param>
        /// <param name="col"></param>
        /// <returns>აბრუნებს ნაპოვნ მნიშვნელობას, თუ ვერ იმოვა მაშინ null-ს.</returns>
        public static object FindValueByValue(this DataTable dataTable, DataColumn sortColumn, object key, DataColumn col)
        {
            return FindValueByValue(dataTable, sortColumn.ColumnName, key, col.ColumnName);
        }
        /// <summary>
        /// მნიშვნელობის პოვნა DataTable-ში.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="sort">სვეტის სახელი, რა სვეტშიც გვინდა რო მოვძებნოთ.</param>
        /// <param name="key">საძიებო მნიშვნელობა</param>
        /// <param name="columnName">სვეტის დასახელბა, საიდანაც უნდა ამოიღოს მნიშვნელობა.</param>
        /// <returns>აბრუნებს ნაპოვნ მნიშვნელობას, თუ ვერ იმოვა მაშინ null-ს.</returns>
        public static object FindValueByValue(this DataTable dataTable, string sort, object key, string columnName)
        {
            var view = new DataView(dataTable) { Sort = sort };

            var i = view.Find(key);

            return i != -1 ? view[i][columnName] : null;
        }
        /// <summary>
        /// მნიშვნელობების პოვნა DataTable-ში.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="sort">სვეტის სახელი, რა სვეტშიც გვინდა რო მოვძებნოთ.</param>
        /// <param name="key">საძიებო მნიშვნელობა</param>
        /// <param name="columnName">სვეტის დასახელბა, საიდანაც უნდა ამოიღოს მნიშვნელობები.</param>
        /// <returns>აბრუნებს ნაპოვნ მნიშვნელობებს, თუ ვერ იმოვა მაშინ null-ს.</returns>
        public static object[] FindValuesByValue(this DataTable dataTable, string sort, object key, string columnName)
        {
            var rowViews = FindRowsByValue(dataTable, sort, key);
            if (rowViews == null || rowViews.Length == 0) return null;

            var values = new object[rowViews.Length];
            for (var i = 0; i < rowViews.Length; i++)
            {
                values[i] = rowViews[i][columnName];
            }

            return values;
        }
        /// <summary>
        /// DataRowView[]-ბის პოვნა DataTable-ში.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="sort">სვეტი, რა სვეტშიც გვინდა რო მოვძებნოთ.</param>
        /// <param name="key">საძიებო მნიშვნელობა</param>
        /// <returns>აბრუნებს ნაპოვნ DataRowView[], თუ ვერ იმოვა მაშინ DataRowView[0]-ს.</returns>
        public static DataRowView[] FindRowsByValue(this DataTable dataTable, DataColumn sort, object key)
        {
            return FindRowsByValue(dataTable, sort.ColumnName, key);
        }
        /// <summary>
        /// DataRowView[]-ბის პოვნა DataTable-ში.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="sort">სვეტის სახელი, რა სვეტშიც გვინდა რო მოვძებნოთ.</param>
        /// <param name="key">საძიებო მნიშვნელობა</param>
        /// <returns>აბრუნებს ნაპოვნ DataRowView[], თუ ვერ იმოვა მაშინ DataRowView[0]-ს.</returns>
        public static DataRowView[] FindRowsByValue(this DataTable dataTable, string sort, object key)
        {
            var view = new DataView(dataTable) { Sort = sort };

            return view.FindRows(key);
        }

        /// <summary>
        /// ინიციალიზაციას უკეთებს ცხრილ რო ადვილად მოიძებნოს მაში მნიშვნელობები.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="column"></param>
        public static void InitDataTableForSearch(this DataTable dataTable, DataColumn column)
        {
            InitDataTableForSearch(dataTable, column.ColumnName);
        }
        public static void InitDataTableForSearch(this DataTable dataTable, DataColumn column, bool lower)
        {
            InitDataTableForSearch(dataTable, column.ColumnName, lower);
        }
        public static void InitDataTableForSearch(this DataTable dataTable, DataColumn column, bool lower, bool removeSpaces)
        {
            InitDataTableForSearch(dataTable, column.ColumnName, lower, removeSpaces);
        }
        public static void InitDataTableForSearch(this DataTable dataTable, DataColumn column, bool lower, bool removeSpaces, bool trim)
        {
            InitDataTableForSearch(dataTable, column.ColumnName, lower, removeSpaces, trim);
        }
        public static void InitDataTableForSearch(this DataTable dataTable, string fieldName)
        {
            InitDataTableForSearch(dataTable, fieldName, true);
        }
        public static void InitDataTableForSearch(this DataTable dataTable, string fieldName, bool lower)
        {
            InitDataTableForSearch(dataTable, fieldName, lower, true);
        }
        public static void InitDataTableForSearch(this DataTable dataTable, string fieldName, bool lower, bool removeSpaces)
        {
            InitDataTableForSearch(dataTable, fieldName, lower, removeSpaces, true);
        }
        public static void InitDataTableForSearch(this DataTable dataTable, string fieldName, bool lower, bool removeSpaces, bool trim)
        {
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                var value = dataTable.Rows[i][fieldName].ToString();
                if (lower) value = value.ToLowerInvariant();
                if (removeSpaces) value = value.Replace(" ", string.Empty);
                if (trim) value = value.Trim();

                dataTable.Rows[i][fieldName] = value;
            }
        }
        #endregion

        #region Upper Lower
        public static void Lower(this DataTable dataTable, DataColumn column)
        {
            Lower(dataTable, column.ColumnName);
        }
        public static void Lower(this DataTable dataTable, string fieldName)
        {
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                dataTable.Rows[i][fieldName] = dataTable.Rows[i][fieldName].ToString().ToLowerInvariant();
            }
        }
        public static void Upper(this DataTable dataTable, DataColumn column)
        {
            Upper(dataTable, column.ColumnName);
        }
        public static void Upper(this DataTable dataTable, string fieldName)
        {
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                dataTable.Rows[i][fieldName] = dataTable.Rows[i][fieldName].ToString().ToUpperInvariant();
            }
        }
        #endregion



        /// <summary>
        /// აჯაბემბს მთლიან სვეტს.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static decimal SelectSum(this DataTable dataTable, DataColumn column)
        {
            return SelectSum(dataTable, column.ColumnName);
        }
        /// <summary>
        /// აჯამებს მთლიან სვეტს.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static decimal SelectSum(this DataTable dataTable, string fieldName)
        {
            var result = 0m;
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i].IsDataRowDeleted()) continue;
                result += dataTable.Rows[i][fieldName] != null && dataTable.Rows[i][fieldName] != DBNull.Value ? Convert.ToDecimal(dataTable.Rows[i][fieldName]) : 0m;
            }

            return result;
        }
        /// <summary>
        /// იღებს ცხრილიდან Row-ების რაოდენობას.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static int SelectCount(this DataTable dataTable)
        {
            var result = 0;
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i].IsDataRowDeleted()) continue;
                result++;
            }

            return result;
        }


        public static DataTable SumDataTable(this DataTable dataTable)
        {
            return SumDataTable(dataTable, false);
        }
        public static DataTable SumDataTable(this DataTable dataTable, bool setdataTableName)
        {
            var table = new DataTable();
            if (setdataTableName)
                table.TableName = dataTable.TableName;

            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Sum", typeof(decimal));

            foreach (DataColumn col in dataTable.Columns)
            {
                if (!col.DataType.IsEnum)
                {
                    switch (Type.GetTypeCode(col.DataType))
                    {
                        case TypeCode.SByte:
                        case TypeCode.Byte:
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            var row = table.NewRow();
                            row["Name"] = col.ColumnName;
                            row["Sum"] = SelectSum(dataTable, col);
                            table.Rows.Add(row);
                            break;
                    }
                }
            }

            return table;
        }


        /// <summary>
        /// ნიჭებს DataSet-ში ყველა DataTable-ს სვეტებს AllowDBNull-ს.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="allowDBNull"></param>
        public static void SetAllowDBNull(this DataSet ds, bool allowDBNull)
        {
            foreach (DataTable table in ds.Tables)
            {
                SetAllowDBNull(table, allowDBNull);
            }
        }
        /// <summary>
        /// ნიჭებს DataTable-ს სვეტებს AllowDBNull-ს.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="allowDBNull"></param>
        public static void SetAllowDBNull(this DataTable table, bool allowDBNull)
        {
            foreach (DataColumn col in table.Columns)
                col.AllowDBNull = allowDBNull;
        }

        /// <summary>
        /// ამოწმებს DataRow-ში ემთხვევა თუ არა მნიშვნელობები lastValues-ს.
        /// </summary>
        /// <param name="lastValues"></param>
        /// <param name="currentRow"></param>
        /// <param name="columns">DataColumn მასივი, რომლის მიხედვითაც მოხდება შემოწმება.</param>
        /// <returns>აბრუნებს true-ს მაშინ, როცა ყველა ემთხვევა, სხვა შემთხვევაში false-ს.</returns>
        public static bool FieldValuesAreEqual(object[] lastValues, DataRow currentRow, DataColumn[] columns)
        {
            return FieldValuesAreEqual(lastValues, currentRow, GetColumnNameArray(columns));
        }
        /// <summary>
        /// ამოწმებს DataRow-ში ემთხვევა თუ არა მნიშვნელობები lastValues-ს.
        /// </summary>
        /// <param name="lastValues"></param>
        /// <param name="currentRow"></param>
        /// <param name="fieldNames">DataColumn დასახელების მასივი, რომლის მიხედვითაც მოხდება შემოწმება.</param>
        /// <returns>აბრუნებს true-ს მაშინ, როცა ყველა ემთხვევა, სხვა შემთხვევაში false-ს.</returns>
        public static bool FieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
        {
            for (var i = 0; i < fieldNames.Length; i++)
            {
                if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// აკლონირებს DataRow-ს, მითითებული DataColumn[]-ით.
        /// </summary>
        /// <param name="sourceRow"></param>
        /// <param name="newRow"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static DataRow CreateRowClone(this DataRow sourceRow, DataRow newRow, DataColumn[] columns)
        {
            return CreateRowClone(sourceRow, newRow, GetColumnNameArray(columns));
        }
        /// <summary>
        /// აკლონირებს DataRow-ს, მითითებული DataColumn[]-ით.
        /// </summary>
        /// <param name="sourceRow"></param>
        /// <param name="newRow"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public static DataRow CreateRowClone(this DataRow sourceRow, DataRow newRow, string[] fieldNames)
        {
            foreach (var field in fieldNames)
                newRow[field] = sourceRow[field];

            return newRow;
        }

        /// <summary>
        /// ანიჭებს DataRow-ს, DataColumn[]-ის მიხედვით მნიშვნელობებს.
        /// </summary>
        /// <param name="lastValues"></param>
        /// <param name="sourceRow"></param>
        /// <param name="columns"></param>
        public static void SetLastValues(object[] lastValues, DataRow sourceRow, DataColumn[] columns)
        {
            SetLastValues(lastValues, sourceRow, GetColumnNameArray(columns));
        }
        /// <summary>
        /// ანიჭებს DataRow-ს, DataColumn[]-ის მიხედვით მნიშვნელობებს.
        /// </summary>
        /// <param name="lastValues"></param>
        /// <param name="sourceRow"></param>
        /// <param name="fieldNames"></param>
        public static void SetLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
        {
            for (var i = 0; i < fieldNames.Length; i++)
                lastValues[i] = sourceRow[fieldNames[i]];
        }




        /// <summary>
        /// ამოშლის DataTable-ში DataColumn[]-ბს.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="columns"></param>
        public static void RemoveColumns(this DataTable dataTable, params DataColumn[] columns)
        {
            RemoveColumns(dataTable, false, columns);
        }
        /// <summary>
        /// ამოშლის DataTable-ში DataColumn[]-ბს.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="skip">true-ს შემთხვევაში გადმოცემული DataColumn[]-ს დატოვებს და დანარჩენს ამოშლის.</param>
        /// <param name="columns"></param>
        public static void RemoveColumns(this DataTable dataTable, bool skip, params DataColumn[] columns)
        {
            RemoveColumns(dataTable, skip, true, columns);
        }
        /// <summary>
        /// ამოშლის DataTable-ში DataColumn[]-ბს.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="skip">true-ს შემთხვევაში გადმოცემული DataColumn[]-ს დატოვებს და დანარჩენს ამოშლის.</param>
        /// <param name="removePrimaryKey"></param>
        /// <param name="columns"></param>
        public static void RemoveColumns(this DataTable dataTable, bool skip, bool removePrimaryKey, params DataColumn[] columns)
        {
            if (removePrimaryKey)
                dataTable.PrimaryKey = null;

            if (skip)
            {
                var i = 0;
                while (i < dataTable.Columns.Count)
                {
                    skip = false;
                    for (var j = 0; j < columns.Length; j++)
                    {
                        if (dataTable.Columns[i] == columns[j])
                        {
                            skip = true;
                            break;
                        }
                    }

                    if (!skip)
                        dataTable.Columns.RemoveAt(i);
                    else
                        i++;
                }

                //for (int i = 0; i < dataTable.Columns.Count; i++)
                //{
                //    skip = false;
                //    for (int j = 0; j < columns.Length; j++)
                //    {
                //        if (dataTable.Columns[i] == columns[j])
                //        {
                //            skip = true;
                //            break;
                //        }
                //    }

                //    if (!skip)
                //    {
                //        dataTable.Columns.RemoveAt(i);
                //        i--;
                //    }
                //}
            }
            else
            {
                foreach (var col in columns)
                    dataTable.Columns.Remove(col);
            }
        }

        /// <summary>
        /// SetColumnAutoIncrement(col, -1);
        /// </summary>
        /// <param name="col"></param>
        public static void SetColumnAutoIncrement(this DataColumn col)
        {
            SetColumnAutoIncrement(col, -1);
        }
        /// <summary>
        /// SetColumnAutoIncrement(col, autoIncrementSeed, -1);
        /// </summary>
        /// <param name="col"></param>
        /// <param name="autoIncrementSeed"></param>
        public static void SetColumnAutoIncrement(this DataColumn col, int autoIncrementSeed)
        {
            SetColumnAutoIncrement(col, autoIncrementSeed, -1);
        }
        /// <summary>
        /// სვეტს ანიჭებს ავტომატურად დათვლის სიდირებას.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="autoIncrementSeed"></param>
        /// <param name="autoIncrementStep"></param>
        public static void SetColumnAutoIncrement(this DataColumn col, int autoIncrementSeed, int autoIncrementStep)
        {
            if (col == null) throw new ArgumentNullException(nameof(col));
            col.AutoIncrement = true;
            col.AutoIncrementSeed = autoIncrementSeed;
            col.AutoIncrementStep = autoIncrementStep;
        }

        /// <summary>
        /// DataTable-ში ყველა ჩანაწერს ანიჭებს მოცემულ სვეტში ანიჭებს მნიშვნელობებს.
        /// </summary>
        /// <param name="col">სვეტი.</param>
        /// <param name="value">მნიშვნელობა.</param>
        public static void SetColumnValue(this DataColumn col, object value)
        {
            if (col == null) return;
            SetColumnValue(col.Table, col, value);
        }
        /// <summary>
        /// DataTable-ში ყველა ჩანაწერს ანიჭებს მოცემულ სვეტში ანიჭებს მნიშვნელობებს.
        /// </summary>
        /// <param name="dataTable">ცხრილი.</param>
        /// <param name="col">სვეტი.</param>
        /// <param name="value">მნიშვნელობა.</param>
        public static void SetColumnValue(this DataTable dataTable, DataColumn col, object value)
        {
            SetColumnValue(dataTable, col.ColumnName, value);
        }
        /// <summary>
        /// DataTable-ში ყველა ჩანაწერს ანიჭებს მოცემულ სვეტში ანიჭებს მნიშვნელობებს.
        /// </summary>
        /// <param name="dataTable">ცხრილი.</param>
        /// <param name="fieldName">სვეტის დასახელება.</param>
        /// <param name="value">მნიშვნელობა.</param>
        public static void SetColumnValue(this DataTable dataTable, string fieldName, object value)
        {
            if (dataTable == null || string.IsNullOrEmpty(fieldName)) return;
            foreach (DataRow row in dataTable.Rows)
            {
                if (row.IsDataRowDeleted()) continue;
                row[fieldName] = value;
            }
        }

        /// <summary>
        /// ასუფთავებს DataTable-ში Cell-ის შეცდომას.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="fieldName"></param>
        public static void ClearColumnError(this DataRow row, string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName)) return;

            if (row.HasErrors && (row.Table == null || row.Table.Columns.Contains(fieldName)))
                row.SetColumnError(fieldName, string.Empty);
        }


        public static void Trim(this DataTable dataTable)
        {
            Trim(dataTable, true);
        }
        public static void Trim(this DataTable dataTable, bool @string)
        {
            Trim(dataTable, @string, true);
        }
        public static void Trim(this DataTable dataTable, bool @string, bool dateTime)
        {
            Trim(dataTable, @string, dateTime, 2);
        }
        public static void Trim(this DataTable dataTable, bool @string, bool dateTime, int @decimal)
        {
            Trim(dataTable, @string, dateTime, @decimal, 2);
        }

        /// <summary>
        /// აჭრის და ამრგვალებს ცხრილში ყველა Rows-ებს
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="string"></param>
        /// <param name="dateTime"></param>
        /// <param name="decimal"></param>
        /// <param name="double"></param>
        public static void Trim(this DataTable dataTable, bool @string, bool dateTime, int @decimal, int @double)
        {
            if (dataTable == null) return;

            foreach (DataRow row in dataTable.Rows)
            {
                Trim(row, @string, dateTime, @decimal, @double);
            }
        }
        public static void Trim(this DataRow row, bool @string, bool dateTime, int @decimal, int @double)
        {
            if (row == null || row.IsDataRowDeleted()) return;
            for (var i = 0; i < row.Table.Columns.Count; i++)
            {
                if (row[i] == null || row[i] == DBNull.Value) continue;

                if (row.Table.Columns[i].DataType == typeof(string) && @string)
                    row[i] = ((string)row[i]).Trim();
                if (row.Table.Columns[i].DataType == typeof(DateTime) && dateTime)
                    row[i] = ((DateTime)row[i]).Date;
                if (row.Table.Columns[i].DataType == typeof(decimal) && @decimal != -1)
                    row[i] = Math.Round((decimal)row[i], @decimal);
                if (row.Table.Columns[i].DataType == typeof(double) && @double != -1)
                    row[i] = Math.Round((double)row[i], @double);
            }
        }

        public static void Replace(this DataTable dataTable, string columnName, string oldValue, string newValue)
        {
            if (dataTable.Columns[columnName].DataType != typeof(string))
                throw new ArgumentException("Column dataType must be type of string.", nameof(columnName));
            foreach (DataRow row in dataTable.Rows)
            {
                if (row.IsDataRowDeleted() || row[columnName] == null || row[columnName] == DBNull.Value) continue;

                row[columnName] = ((string)row[columnName]).Replace(oldValue, newValue);
            }
        }




        /// <summary>
        /// Creates a new DataTable from the rows currently active in the given view.
        /// </summary>
        /// <param name="dataView">DataView to create the table from.</param>
        /// <returns>
        /// Returns a new DataTable with the same schema as the table, 
        /// which the view is based on.
        /// </returns>
        public static DataTable CreateTableFromView(this DataView dataView)
        {
            var dataTable = dataView.Table.Clone();
            foreach (DataRowView rowView in dataView)
            {
                CopyRowInto(rowView.Row, dataTable);
            }
            return dataTable;
        }

        /// <summary>
        /// Adds an empty row to the datatable. In contrary to the default method,
        /// all members will be set to their default values except the columns
        /// allowing DBNull values, which will not be initialized.
        /// </summary>
        /// <param name="table">The DataTable to add an empty DataRow.</param>
        public static DataRow AddEmptyRow(this DataTable table)
        {
            var row = GetEmptyRow(table);
            table.Rows.Add(row);
            return row;
        }

        /// <summary>
        /// Fills a DataRow with the default values.
        /// </summary>
        /// <param name="row">The row to fill in data.</param>
        public static void FillRow(this DataRow row)
        {
            foreach (DataColumn col in row.Table.Columns)
            {
                if (!col.AllowDBNull && !col.AutoIncrement)
                {
                    switch (col.DataType.ToString())
                    {
                        case "System.Boolean":
                            row[col] = false;
                            break;

                        case "System.Byte":
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                            row[col] = 0;
                            break;

                        case "System.Single":
                            row[col] = 0f;
                            break;
                        case "System.Double":
                            row[col] = 0d;
                            break;
                        case "System.Decimal":
                            row[col] = decimal.Zero;
                            break;

                        case "System.DateTime":
                            row[col] = DateTime.Now;
                            break;

                        case "System.String":
                            row[col] = string.Empty;
                            break;

                        default:
                            throw new ArgumentOutOfRangeException("Column type", row[col], "DataSetHelper.FillRow: No default value for given datatype available. Please add code!");
                    }
                }
            }
        }


        /// <summary>
        /// შლის ყველა Rows-ებს.
        /// </summary>
        /// <param name="dataTable"></param>
        public static void DeleteAllRows(this DataTable dataTable)
        {
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                switch (dataTable.Rows[i].RowState)
                {
                    case DataRowState.Added:
                        dataTable.Rows.RemoveAt(i);
                        i--;
                        break;

                    case DataRowState.Deleted:
                        break;

                    default:
                        dataTable.Rows[i].Delete();
                        break;
                }
            }
        }


        public static void ImportRows(this DataTable table, DataTable sourceDataTable)
        {
            ImportRows(table, sourceDataTable.Rows);
        }
        /// <summary>
        /// აიმპორტირებს DataRow[]-ის მასივს.
        /// </summary>
        /// <param name="table">ცხრილი სადაც გვინდა რომ დავაიმპორტიროთ.</param>
        /// <param name="rows">DataRow-ის მასივი.</param>
        public static void ImportRows(this DataTable table, DataRowCollection rows)
        {
            foreach (DataRow row in rows)
            {
                table.ImportRow(row);
            }
        }
        /// <summary>
        /// აიმპორტირებს DataRow[]-ის მასივს.
        /// </summary>
        /// <param name="table">ცხრილი სადაც გვინდა რომ დავაიმპორტიროთ.</param>
        /// <param name="rows">DataRow-ის მასივი.</param>
        public static void ImportRows(this DataTable table, params DataRow[] rows)
        {
            foreach (var row in rows)
            {
                table.ImportRow(row);
            }
        }

        /// <summary>
        /// ამატებს DataRow[]-ის მასივს DataTable-ში.
        /// </summary>
        /// <param name="table">ცხრილი სადაც გვინდა რომ დავამატოთ.</param>
        /// <param name="rows">DataRow-ის მასივი.</param>
        public static void AddRowsRange(this DataTable table, DataRowCollection rows)
        {
            foreach (DataRow row in rows)
            {
                table.Rows.Add(row);
            }
        }
        /// <summary>
        /// ამატებს DataRow[]-ის მასივს DataTable-ში.
        /// </summary>
        /// <param name="table">ცხრილი სადაც გვინდა რომ დავამატოთ.</param>
        /// <param name="rows">DataRow-ის მასივი.</param>
        public static void AddRowsRange(this DataTable table, params DataRow[] rows)
        {
            foreach (var row in rows)
            {
                table.Rows.Add(row);
            }
        }

        /// <summary>
        /// Creates an empty row with all columns set to their default values 
        /// except the columns allowing DBNull values, which will not be 
        /// initialized.
        /// </summary>
        /// <param name="table">The DataTable to get the empty DataRow.</param>
        /// <returns>Returns the initialized DataRow.</returns>
        public static DataRow GetEmptyRow(this DataTable table)
        {
            var row = table.NewRow();
            FillRow(row);
            return row;
        }

        /// <summary>
        /// Copies a row and inserts the copy into a destination-table. 
        /// </summary>
        /// <param name="original">the orignial row to be copied</param>
        /// <param name="destination">the table to which the copy is added</param>
        /// <returns>the created row; if error occurred it's null</returns>
        public static DataRow CopyRowInto(this DataRow original, DataTable destination)
        {
            if (original == null || destination == null) return null;
            var copy = destination.NewRow();
            // set default-values
            FillRow(copy);
            // copies the values 
            FillRow(copy, original);
            // add the row
            destination.Rows.Add(copy);
            return copy;
        }

        /// <summary>
        /// Copies an entire data set and inserts the copy into a destination-data set. 
        /// A not exiting table will be created in the destination dataset
        /// </summary>
        /// <param name="original">the orignial dataset to be copied</param>
        /// <param name="destination">the dataset to which the copy is added</param>
        /// <returns></returns>
        public static void CopyDataSetInto(this DataSet original, DataSet destination)
        {
            if (original == null || destination == null) return;
            foreach (DataTable table in original.Tables)
            {
                if (!destination.Tables.Contains(table.TableName))
                {
                    destination.Tables.Add(table.Clone());
                }

                foreach (DataRow row in table.Rows)
                {
                    CopyRowInto(row, destination.Tables[table.TableName]);
                }
            }
        }

        /// <summary>
        /// Fills the default values from a template row into another row
        /// </summary>
        /// <param name="row">the row beeing initalized with the template</param>
        /// <param name="template">the template with the values</param>
        public static void FillRow(this DataRow row, DataRow template)
        {
            foreach (DataColumn templateColumn in template.Table.Columns)
            {
                var rowColumn = row.Table.Columns[templateColumn.ColumnName];
                if (rowColumn != null &&
                    rowColumn.DataType == templateColumn.DataType &&
                    rowColumn.AutoIncrement == false)
                {
                    row[rowColumn] = template[templateColumn];
                }
            }
        }

        /// <summary>
        /// Adds a filter to a DataView without overwritting an already existing filter
        /// on that view. 
        /// </summary>
        /// <param name="view">the view to add the filter on</param>
        /// <param name="filter">the filter to add</param>
        /// <returns>the old filter-criteria. You can remove a filter using DataViewRemoveFilter</returns>
        public static string DataViewAddFilter(this DataView view, string filter)
        {
            var oldFilter = view.RowFilter;
            var newFilter = oldFilter;
            newFilter = newFilter.Length > 0 ? $"{oldFilter} AND {filter}" : filter;
            view.RowFilter = newFilter;
            view.RowStateFilter = DataViewRowState.CurrentRows;
            return oldFilter;
        }

        /// <summary>
        /// Removes a filter on a DataView by setting the oldFilter. The oldFilter was retrieved by
        /// mehtode DataViewAddFilter.
        /// </summary>
        /// <param name="view">the view to set the old filter on</param>
        /// <param name="oldFilter">the old filter retrieved by DataViewAddFilter</param>
        public static void DataViewRemoveFilter(this DataView view, string oldFilter)
        {
            view.RowFilter = oldFilter;
            view.RowStateFilter = DataViewRowState.CurrentRows;
        }

        /// <summary>
        /// Creates a DataView showing the same entries as the original View
        /// </summary>
        /// <param name="originalView">the origignal view from which the new View is created</param>
        /// <returns>the created new View</returns>
        public static DataView DataViewCreateView(this DataView originalView)
        {
            var newView = new DataView(originalView.Table) { Sort = originalView.Sort, RowFilter = originalView.RowFilter, RowStateFilter = originalView.RowStateFilter };
            return newView;
        }

        /// <summary>
        /// Corrects a DataView containing a Column gueltigAb and gueltigBis. GueltigBis will be corrected watching
        /// not to have overlapping DateRanges.
        /// </summary>
        /// <param name="dataToCorrect">Dataview to cerrect</param>
        /// <param name="gueltigAbColumn">Columnname for GueltigAb</param>
        /// <param name="gueltigBisColumn">Columnname for GueltigBis</param>
        /// <returns>true if any correction was made</returns>
        public static bool CorrectDateRange(this DataView dataToCorrect, string gueltigAbColumn, string gueltigBisColumn)
        {
            DataRowView thisEntry;
            DataRowView nextEntry;
            var workView = DataViewCreateView(dataToCorrect);
            var corrected = false;

            // create a View for sorting

            //in case of a kostentraeger with same gueltigBis the not changed/inserted 
            //gueltigAb will be set to gueltigAb + 1
            bool correctedGueltigAb;
            do
            {
                correctedGueltigAb = false;
                workView.Sort = gueltigAbColumn;
                for (var i = 0; i < workView.Count; i++)
                {
                    thisEntry = workView[i];
                    if (i != workView.Count - 1) // last entry we don't touch 
                    {
                        // not last entry
                        nextEntry = workView[i + 1];
                        if (((DateTime)thisEntry[gueltigAbColumn]).Date ==
                            ((DateTime)nextEntry[gueltigAbColumn]).Date)
                        {
                            var entryToChange = thisEntry;
                            if (entryToChange.Row.RowState != DataRowState.Unchanged)
                            {
                                entryToChange = nextEntry;
                            }
                            entryToChange[gueltigAbColumn] = ((DateTime)entryToChange[gueltigAbColumn]).AddDays(1);
                            if ((DateTime)entryToChange[gueltigBisColumn] < (DateTime)entryToChange[gueltigAbColumn])
                            {
                                entryToChange[gueltigBisColumn] = entryToChange[gueltigAbColumn];
                            }
                            correctedGueltigAb = true;
                            corrected = true;
                            break;
                        }
                    }
                }
            } while (correctedGueltigAb);

            // correct gueltigBis
            for (var i = 0; i < workView.Count; i++)
            {
                thisEntry = workView[i];
                if (i != workView.Count - 1) // last entry we don't touch 
                {
                    // not last entry
                    nextEntry = workView[i + 1];
                    if ((DateTime)thisEntry[gueltigBisColumn] >= (DateTime)nextEntry[gueltigAbColumn])
                    {
                        var gueltigBis = (DateTime)nextEntry[gueltigAbColumn];
                        gueltigBis = gueltigBis.Subtract(new TimeSpan(1, 0, 0, 0)); // subtract one day
                        thisEntry[gueltigBisColumn] = gueltigBis;
                        corrected = true;
                    }
                }
            }
            return corrected;
        }

        /// <summary>
        /// Returns the Names of the Columns that were changed in the row since the last AcceptChange();
        /// respectivly since were loaded from the database last time.
        /// </summary>
        /// <param name="row">the row to be examined</param>
        /// <returns>the name of all changed columns</returns>
        public static DataColumn[] GetChangedColumns(this DataRow row)
        {
            var changedColumnsList = new ArrayList();

            if (row.RowState == DataRowState.Unchanged)
            {
                // nothing has changed
            }
            else if (row.RowState == DataRowState.Modified)
            {
                // find out what has changed
                foreach (DataColumn column in row.Table.Columns)
                {
                    var hasChanged = false;
                    var origValue = row[column, DataRowVersion.Original];
                    var currValue = row[column, DataRowVersion.Current];
                    if (origValue is DBNull)
                    {
                        if (currValue is DBNull == false)
                        {
                            hasChanged = true;
                        }
                    }
                    if (currValue is DBNull)
                    {
                        if (origValue is DBNull == false)
                        {
                            hasChanged = true;
                        }
                    }
                    else
                    {
                        if (currValue.ToString() != origValue.ToString())
                        {
                            hasChanged = true;
                        }
                    }
                    if (hasChanged)
                    {
                        changedColumnsList.Add(column);
                    }
                }
            }
            else // deleted, inserted
            {
                // everything has changed
                changedColumnsList.AddRange(row.Table.Columns);
            }
            var changedColumns = new DataColumn[changedColumnsList.Count];
            changedColumnsList.CopyTo(changedColumns);
            return changedColumns;
        }

        /// <summary>
        /// Checks if certain Columns in a row have a changed value since last AcceptChanges() on that row
        /// </summary>
        /// <param name="columns">a list of the columns to be checked</param>
        /// <param name="row">the row to be analyzed</param>
        /// <returns>true if any of that columns in the row has changed</returns>
        public static bool HasColumnChanged(this DataRow row, DataColumn[] columns)
        {
            var changedColumns = GetChangedColumns(row);
            var columnArray = new ArrayList(changedColumns);

            foreach (var dataColumn in columns)
            {
                if (columnArray.Contains(dataColumn))
                {
                    return true;
                }
            }
            return false;
        }
        #region Secure Merge DataSet
        /// <summary>
        /// Merges an array of DataRow objects into the destination DataSet, preserving or discarding changes in the 
        /// DataSet and handling an incompatible schema according the default values.
        /// </summary>
        /// <param name="destination">DataSet to merge into.</param>
        /// <param name="source">The array of DataRow objects to be merged into the DataSet.</param>
        public static void SecureMerge(DataSet destination, DataRow[] source)
        {
            SecureMerge(destination, source, false, MissingSchemaAction.AddWithKey);
        }

        /// <summary>
        /// Merges an array of DataRow objects into the destination DataSet, preserving or discarding changes in the 
        /// DataSet and handling an incompatible schema according to the given arguments.
        /// </summary>
        /// <param name="destination">DataSet to merge into.</param>
        /// <param name="source">The array of DataRow objects to be merged into the DataSet.</param>
        /// <param name="preserveChanges">true to preserve changes in the DataSet; otherwise, false.</param>
        /// <param name="missingSchemaAction">One of the MissingSchemaAction values.</param>
        public static void SecureMerge(DataSet destination, DataRow[] source, bool preserveChanges, MissingSchemaAction missingSchemaAction)
        {
            BeginLoad(destination);
            destination.Merge(source, preserveChanges, missingSchemaAction);
            EndLoad(destination);
        }

        /// <summary>
        /// Merges an array of DataRow objects into the destination DataSet, preserving or discarding changes in the 
        /// DataSet and handling an incompatible schema according the default values.
        /// </summary>
        /// <param name="destination">DataSet to merge into.</param>
        /// <param name="source">DataTable to be merged into the DataSet.</param>
        public static void SecureMerge(DataSet destination, DataTable source)
        {
            SecureMerge(destination, source, false, MissingSchemaAction.AddWithKey);
        }

        /// <summary>
        /// Merges an array of DataRow objects into the destination DataSet, preserving or discarding changes in the 
        /// DataSet and handling an incompatible schema according to the given arguments.
        /// </summary>
        /// <param name="destination">DataSet to merge into.</param>
        /// <param name="source">DataTable to be merged into the DataSet.</param>
        /// <param name="preserveChanges">true to preserve changes in the DataSet; otherwise, false.</param>
        /// <param name="missingSchemaAction">One of the MissingSchemaAction values.</param>
        public static void SecureMerge(DataSet destination, DataTable source, bool preserveChanges, MissingSchemaAction missingSchemaAction)
        {
            BeginLoad(destination);
            destination.Merge(source, preserveChanges, missingSchemaAction);
            EndLoad(destination);
        }

        /// <summary>
        /// Merges an array of DataRow objects into the destination DataSet, preserving or discarding changes in the 
        /// DataSet and handling an incompatible schema according the default values.
        /// </summary>
        /// <param name="destination">DataSet to merge into.</param>
        /// <param name="source">DataSet to be merged into the DataSet.</param>
        public static void SecureMerge(DataSet destination, DataSet source)
        {
            SecureMerge(destination, source, false);
        }

        /// <summary>
        /// Merges an array of DataRow objects into the destination DataSet, preserving or discarding changes in the 
        /// DataSet and handling an incompatible schema according to the given arguments.
        /// </summary>
        /// <param name="destination">DataSet to merge into.</param>
        /// <param name="source">DataTable to be merged into the DataSet.</param>
        /// <param name="preserveChanges">true to preserve changes in the DataSet; otherwise, false.</param>
        public static void SecureMerge(DataSet destination, DataSet source, bool preserveChanges)
        {
            SecureMerge(destination, source, preserveChanges, MissingSchemaAction.AddWithKey);
        }

        /// <summary>
        /// Merges an array of DataRow objects into the destination DataSet, preserving or discarding changes in the 
        /// DataSet and handling an incompatible schema according to the given arguments.
        /// </summary>
        /// <param name="destination">DataSet to merge into.</param>
        /// <param name="source">DataTable to be merged into the DataSet.</param>
        /// <param name="preserveChanges">true to preserve changes in the DataSet; otherwise, false.</param>
        /// <param name="missingSchemaAction">One of the MissingSchemaAction values.</param>
        public static void SecureMerge(DataSet destination, DataSet source, bool preserveChanges, MissingSchemaAction missingSchemaAction)
        {
            BeginLoad(destination);
            destination.Merge(source, preserveChanges, missingSchemaAction);
            EndLoad(destination);
        }

        private static void BeginLoad(DataSet ds)
        {
            foreach (DataTable table in ds.Tables)
            {
                table.BeginLoadData();
            }
        }
        private static void EndLoad(DataSet ds)
        {
            foreach (DataTable table in ds.Tables)
            {
                table.EndLoadData();
            }
        }
        #endregion

        /// <summary>
        /// Updates cell of destinationRow in case it is different from sourceRow 
        /// </summary>
        /// <param name="sourceRow">Row to comare with</param>
        /// <param name="destinationRow">Ro to update</param>
        /// <param name="columnsToUpdateArray">Columns to verify</param>
        public static void UpdateColumnsIfDifferent(this DataRow sourceRow, DataRow destinationRow, string[] columnsToUpdateArray)
        {
            var columnsToUpdate = new StringCollection();
            columnsToUpdate.AddRange(columnsToUpdateArray);

            foreach (DataColumn column in destinationRow.Table.Columns)
            {
                if (sourceRow.Table.Columns.Contains(column.ColumnName) && columnsToUpdate.Contains(column.ColumnName))
                {
                    if (destinationRow[column.ColumnName] != sourceRow[column.ColumnName])
                    {
                        destinationRow[column.ColumnName] = sourceRow[column.ColumnName];
                    }
                }
            }
        }

        /// <summary>
        /// Initializes all AutoIncrement columns to start with -1 with an increment of -1. 
        /// Every added column, which is not present in the Database will have a value of 
        /// below zero! These columns can be identified easier and no more accidents can 
        /// happen with wrong foreign keys!
        /// </summary>
        /// <param name="dataSet">The DataSet to initialize the AutoIncrement columns.</param>
        public static void SetAutoIncrement(this DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                table.SetAutoIncrement();
            }
        }
        public static void SetAutoIncrement(this DataTable table)
        {
            foreach (DataColumn column in table.Columns)
            {
                if (column.AutoIncrement)
                {
                    column.AutoIncrementSeed = -1;
                    column.AutoIncrementStep = -1;
                }
            }
        }





        #region DataConvertation
        //private const int ActionAdded = (int)DataRowState.Added;
        //private const int ActionModified = (int)DataRowState.Modified;
        //private const int ActionDeleted = (int)DataRowState.Deleted;
        //private const DataRowState DefaultDataRowState = DataRowState.Added | DataRowState.Modified | DataRowState.Deleted;

        public static DataSet PackAlternationDatSet<T>(this T ds) where T : DataSet
        {
            return PackAlternationDatSet(ds, MappingType.Attribute);
        }
        public static DataSet PackAlternationDatSet<T>(this T ds, MappingType map) where T : DataSet
        {
            if (ds == null)
                throw new Exception("დაშვებული შეცდომა: მონაცემის რეზერვატორი ცარიელია.");
            if (ds.Tables.Count == 0)
                throw new Exception("დაშვებულია შეცდომა, გადმოცემული მონაცემები ცარიელია, ოპერაცია ვერ განხორციელდება.");

            var root = new DataSet("root");

            foreach (DataTable table in ds.Tables)
            {
                root.Tables.Add(PackAlternationDataTable(table, map));
            }

            return root;
        }

        public static void SetColumnMappingType(this DataSet ds, MappingType mappingType = MappingType.Attribute)
        {
            foreach (DataTable table in ds.Tables)
            {
                SetColumnMappingType(table, mappingType);
            }
        }
        public static void SetColumnMappingType(this DataTable dataTable, MappingType mappingType = MappingType.Attribute)
        {
            foreach (DataColumn col in dataTable.Columns)
                col.ColumnMapping = mappingType;
        }

        public static T PackAlternationTypedDataTable<T>(this T dataTable, MappingType mappingType = MappingType.Attribute, DataRowState dataRowState = DataRowState.Added | DataRowState.Modified | DataRowState.Deleted, string actionColumnName = "Action", bool addActionColumn = true) where T : DataTable
        {
            return (T)PackAlternationDataTable(dataTable, mappingType, dataRowState, actionColumnName, addActionColumn);
        }
        public static DataTable PackAlternationDataTable(this DataTable dataTable, MappingType mappingType = MappingType.Attribute, DataRowState dataRowState = DataRowState.Added | DataRowState.Modified | DataRowState.Deleted, string actionColumnName = "Action", bool addActionColumn = true)
        {
            var newTable = dataTable.Clone();

            var actionColumnIndex = dataTable.Columns.IndexOf(actionColumnName);
            if (actionColumnIndex == -1)
            {
                if (addActionColumn)
                {
                    newTable.Columns.Add(actionColumnName, typeof(int));
                    actionColumnIndex = newTable.Columns.Count - 1;
                }
                else
                    throw new ArgumentException($"Column '{actionColumnName}' does not belong to table {dataTable.TableName}.");
            }

            SetColumnMappingType(newTable, mappingType);

            DataRow[] rows;
            if ((dataRowState & DataRowState.Added) != 0)
            {
                rows = dataTable.Select(string.Empty, string.Empty, DataViewRowState.Added);
                foreach (var row in rows)
                {
                    var r = newTable.NewRow();
                    r.ItemArray = row.ItemArray;
                    r[actionColumnIndex] = 4;
                    newTable.Rows.Add(r);
                }
            }

            if ((dataRowState & DataRowState.Modified) != 0)
            {
                rows = dataTable.Select(string.Empty, string.Empty, DataViewRowState.ModifiedOriginal);
                foreach (var row in rows)
                {
                    var r = newTable.NewRow();
                    r.ItemArray = row.ItemArray;
                    r[actionColumnIndex] = 16;
                    newTable.Rows.Add(r);
                }
            }

            if ((dataRowState & DataRowState.Deleted) != 0)
            {
                rows = dataTable.Select(string.Empty, string.Empty, DataViewRowState.Deleted);
                foreach (var row in rows)
                {
                    row.RejectChanges();
                    var r = newTable.NewRow();
                    r.ItemArray = row.ItemArray;
                    row.Delete();
                    r[actionColumnIndex] = 8;
                    newTable.Rows.Add(r);
                }
            }

            if ((dataRowState & DataRowState.Unchanged) != 0)
            {
                rows = dataTable.Select(string.Empty, string.Empty, DataViewRowState.Unchanged);
                foreach (var row in rows)
                {
                    var r = newTable.NewRow();
                    r.ItemArray = row.ItemArray;
                    r[actionColumnIndex] = 2;
                    newTable.Rows.Add(r);
                }
            }

            return newTable;
        }


        /// <summary>
        /// გადაცემული DataTable აბრუნებს მის xml->bytes წარმოსახვას(შეიცავს წაშლილ, კორექტირებულ, დამატებულ სტრიქონებს, სტატუსის მიხედვით).
        /// InnerBytesData(dataTable, true)
        /// </summary>
        /// <param name="dataTable">DataTable რომლსი xml წარმოსახვაც გვესაჭიროება.</param>
        /// <returns>ბრუნებს xml-ს ბაიტების სახით.</returns>
        public static byte[] GetBytesData(this DataTable dataTable)
        {
            return GetBytesData(dataTable, true);
        }
        /// <summary>
        /// გადაცემული DataTable აბრუნებს მის xml->bytes წარმოსახვას(შეიცავს წაშლილ, კორექტირებულ, დამატებულ სტრიქონებს, სტატუსის მიხედვით).
        /// </summary>
        /// <param name="dataTable">DataTable რომლსი xml წარმოსახვაც გვესაჭიროება.</param>
        /// <param name="compress">თუ გვინდა კომპრესაცია ბაიტების მაშინ true.</param>
        /// <returns>ბრუნებს xml-ს ბაიტების სახით.</returns>
        public static byte[] GetBytesData(this DataTable dataTable, bool compress)
        {
            using (var ds = new DataSet("root"))
            {
                ds.Tables.Add(dataTable.Copy());
                return GetBytesData(ds, compress);
            }
        }
        /// <summary>
        /// გადაცემული DataSet აბრუნებს მის xml->bytes წარმოსახვას(შეიცავს წაშლილ, კორექტირებულ, დამატებულ სტრიქონებს, სტატუსის მიხედვით).
        /// InnerBytesData(ds, true)
        /// </summary>
        /// <param name="ds">DataSet რომლსი xml წარმოსახვაც გვესაჭიროება.</param>
        /// <returns>ბრუნებს xml-ს ბაიტების სახით.</returns>
        public static byte[] GetBytesData(this DataSet ds)
        {
            return GetBytesData(ds, true);
        }
        /// <summary>
        /// გადაცემული DataSet აბრუნებს მის xml->bytes წარმოსახვას(შეიცავს წაშლილ, კორექტირებულ, დამატებულ სტრიქონებს, სტატუსის მიხედვით).
        /// </summary>
        /// <param name="ds">DataSet რომლსი xml წარმოსახვაც გვესაჭიროება.</param>
        /// <param name="compress">თუ გვინდა კომპრესაცია ბაიტების მაშინ true.</param>
        /// <returns>ბრუნებს xml-ს ბაიტების სახით.</returns>
        public static byte[] GetBytesData(this DataSet ds, bool compress)
        {
            return GetBytesData(GetXmlData(ds), compress);
        }
        public static byte[] GetBytesData(string xml)
        {
            return GetBytesData(xml, true);
        }
        public static byte[] GetBytesData(string xml, bool compress)
        {
            return compress ? GZipHelper.Compress(xml.ToUTF8Array()) : xml.ToUTF8Array();
        }

        /// <summary>
        /// გადაცემული DataTable აბრუნებს მის xml წარმოსახვას(შეიცავს წაშლილ, კორექტირებულ, დამატებულ სტრიქონებს, სტატუსის მიხედვით)
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns>აბრუნებს xml-ს სიმბოლოების სახით</returns>
        public static string GetXmlData(this DataTable dataTable)
        {
            using (var ds = new DataSet("root"))
            {
                ds.Tables.Add(dataTable.Copy());
                return GetXmlData(ds);
            }
        }
        /// <summary>
        /// გადაცემული DataSet აბრუნებს მის xml წარმოსახვას(შეიცავს წაშლილ, კორექტირებულ, დამატებულ სტრიქონებს, სტატუსის მიხედვით).
        /// </summary>
        /// <param name="ds">DataSet რომლსი xml წარმოსახვაც გვესაჭიროება.</param>
        /// <returns>აბრუნებს xml-ს სიმბოლოების სახით.</returns>
        public static string GetXmlData(this DataSet ds)
        {
            try
            {
                ds.Namespace = string.Empty;
                return ds.GetXml();
            }
            catch (Exception ex)
            {
                throw new Exception("შეცდომა: მონაცემების კონვერტირება xml ფორმატში ვერ მოხერხდა.\n" + ex.Message);
            }
        }
        /// <summary>
        /// გადაცემული ბაიტების მასივიდან აბრუნებს მის XML-ს.
        /// </summary>
        /// <param name="gzbuffer">დაკომპრესირებული ბაიტების მასივი.</param>
        /// <returns>აბრუნებს XML-ს.</returns>
        public static string GetXmlData(this byte[] gzbuffer)
        {
            return GetXmlData(gzbuffer, true);
        }
        /// <summary>
        /// გადაცემული ბაიტების მასივიდან აბრუნებს მის XML-ს.
        /// </summary>
        /// <param name="buffer">ბაიტების მასივი.</param>
        /// <param name="decompress">როცა გადმოცემული ბაიტების მასივი არის დაკომპრესირებული მაშინ გადავცემთ true-ს.</param>
        /// <returns>აბრუნებს XML-ს</returns>
        public static string GetXmlData(this byte[] buffer, bool decompress)
        {
            return (decompress ? GZipHelper.Decompress(buffer) : buffer).UTF8ArrayToString();
        }
        #endregion











        // Приватные поля, используемые для оптимизации потребления таблицей памяти и быстрого доступа к данным
        private static readonly FieldInfo StorageField = typeof(DataColumn).GetField("_storage", BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly FieldInfo ValuesField = typeof(DataTable).Assembly.GetType("System.Data.Common.StringStorage").GetField("values", BindingFlags.Instance | BindingFlags.NonPublic);
        /// <summary>
        /// Оптимизация таблицы по использованию памяти. По сути делает интернирование строк в рамках таблицы.
        /// </summary>
        /// <param name="table">Таблица.</param>
        /// <returns>Ссылка на себя.</returns>
        public static DataTable Compact(this DataTable table)
        {
            if (table.Rows.Count == 0)
                return table;

            var exclusiveStrings = new Dictionary<string, string>();
            for (int column = 0; column < table.Columns.Count; ++column)
            {
                if (table.Columns[column].DataType == typeof(string))
                {
                    // Прямой доступ к массиву (вертикальное хранилище)
                    var values = (string[])ValuesField.GetValue(StorageField.GetValue(table.Columns[column]));
                    int rowCount = table.Rows.Count;
                    for (int row = 0; row < rowCount; ++row)
                    {
                        var value = values[row];
                        if (value != null)
                        {
                            string hashed;
                            if (!exclusiveStrings.TryGetValue(value, out hashed))
                                // строка встречается впервые
                                exclusiveStrings.Add(value, value);
                            else
                                // дубликат
                                values[row] = hashed;
                        }
                    }
                    exclusiveStrings.Clear();
                }
            }
            return table;
        }
    }
}
