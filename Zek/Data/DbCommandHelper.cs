using System.Data;

namespace Zek.Data
{
    public class DbCommandHelper
    {

        public static string[] GetParamNameArray(DataTable table)
        {
            return GetParamNameArray(table.Columns);
        }
        public static string[] GetParamNameArray(DataColumnCollection columnCollection)
        {
            var fieldNames = new string[columnCollection.Count];
            for (var i = 0; i < columnCollection.Count; i++)
                fieldNames[i] = GetParamName(columnCollection[i]);
            return fieldNames;
        }
        public static string GetParamName(DataColumn column)
        {
            return "@" + column.ColumnName;
        }
    }
}
