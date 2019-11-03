using System;
using System.Text;

using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Zek.Data
{
    public class DbfHelper : OleDbHelper
    {
        protected DbfHelper() { }
        public static DataTable GetDataTable(string file)
        {
            using (var conn = GetOleDbConnection(Path.GetDirectoryName(file)))
            {
                return ExecuteDataTable(conn, $"SELECT * FROM [{Path.GetFileNameWithoutExtension(file)}]");
                //return GetDataTable(conn, string.Format("SELECT * FROM [{0}]", Path.GetFileNameWithoutExtension(file)));
            }
        }

        public static OleDbConnection GetOleDbConnection(string dbfDir)
        {
            return new OleDbConnection(GetOleDbConnectionString(dbfDir));
        }
        public static string GetOleDbConnectionString(string dbfDir)
        {
            var builder = new OleDbConnectionStringBuilder { Provider = "Microsoft.Jet.OLEDB.4.0", DataSource = dbfDir };
            builder.Add("Extended Properties", "dBASE IV");
            builder.Add("User ID", "Admin");
            builder.Add("Password", string.Empty);

            return builder.ConnectionString;
        }

        public static void Export(DataSet ds, string dir)
        {
            foreach (DataTable table in ds.Tables)
            {
                Export(table, dir);
            }
        }
        public static void Export(DataTable table, string dir)
        {
            using (var cmd = new OleDbCommand(CreateExportCommandText(table), GetOleDbConnection(dir)))
            {
                try
                {
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();


                    cmd.CommandText = $@"INSERT INTO [{table.TableName}] VALUES ({string.Join(", ", DbCommandHelper.GetParamNameArray(table.Columns))})";

                    foreach (DataRow row in table.Rows)
                    {
                        cmd.Parameters.Clear();
                        for (var i = 0; i < table.Columns.Count; i++)
                        {
                            cmd.Parameters.Add(DbCommandHelper.GetParamName(table.Columns[i]), GetOleDbType(table.Columns[i].DataType)).Value = row[i];
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    if (cmd.Connection != null && cmd.Connection.State == ConnectionState.Open)
                        cmd.Connection.Close();
                }
            }
        }

        private static OleDbType GetOleDbType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return OleDbType.Boolean;

                case TypeCode.DateTime:
                    return OleDbType.Date;

                case TypeCode.Decimal:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.SByte:
                    return OleDbType.Numeric;

                case TypeCode.Char:
                case TypeCode.String:
                    return OleDbType.VarChar;

                default:
                    return OleDbType.VarChar;
            }
        }
        public static string CreateExportCommandText(DataTable table)
        {
            var createStatement = "CREATE TABLE " + table.TableName + " ( ";

            for (var i = 0; i < table.Columns.Count; i++)
            {
                var tmp = table.Columns[i].ColumnName;


                switch (Type.GetTypeCode(table.Columns[i].DataType))
                {
                    case TypeCode.Boolean:
                        tmp += " LOGICAL";
                        break;

                    case TypeCode.DateTime:
                        tmp += " DATE";
                        break;

                    case TypeCode.Decimal:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.SByte:
                        tmp += " NUMERIC";
                        break;

                    case TypeCode.Char:
                    case TypeCode.String:
                        if (table.Columns[i].MaxLength > 254)
                            tmp += " MEMO";
                        else
                            tmp += $" CHAR({(table.Columns[i].MaxLength == -1 ? 254 : table.Columns[i].MaxLength)})";
                        break;

                    default:
                        tmp += " VARCHAR(254)";
                        break;
                }



                if (i > 0 && tmp.Length > 0)
                    createStatement += ", ";
                createStatement += tmp;

            }
            createStatement += ")";

            return createStatement;
        }

        /// <summary>
        /// კოდირება (როცა DBF-ში ვწერთ მაშინ საჭიროა ეს მეთოდი გამოვიძახოთ).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encode(string value)
        {
            if (value == null) return null;

            var bytes = Encoding.Default.GetBytes(value);
            value = Encoding.GetEncoding(850).GetString(bytes);
            return value;
        }
        /// <summary>
        /// კოდირება (როცა DBF-ში ვწერთ მაშინ საჭიროა ეს მეთოდი გამოვიძახოთ).
        /// </summary>
        /// <param name="table"></param>
        public static void Encode(DataTable table)
        {
            foreach (DataColumn col in table.Columns)
            {
                if (col.DataType != typeof(string)) continue;

                foreach (DataRow row in table.Rows)
                {
                    if (row[col.ColumnName] != DBNull.Value)
                    {
                        row[col.ColumnName] = Encode((string)row[col.ColumnName]);
                    }
                }
            }

            //table.AcceptChanges();
        }


        /// <summary>
        /// აკეთებს დეკოდირებას ტექსტის.
        /// სტანდარტულად DBF ფაილში კოდირება 850 არის და .NET -ის კი UTF-8 ამიტომაც საჭიროა დეკოდირება
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Decode(string value)
        {
            if (value == null) return null;
            
            var bytes = Encoding.GetEncoding(850).GetBytes(value);
            value = Encoding.Default.GetString(bytes);
            return value;
        }
        /// <summary>
        /// აკეთებს დეკოდირებსას ცხრილის.
        /// სტანდარტულად DBF ფაილში კოდირება 850 არის და .NET -ის კი UTF-8 ამიტომაც საჭიროა დეკოდირება
        /// </summary>
        /// <param name="table"></param>
        public static void Decode(DataTable table)
        {
            foreach (DataColumn col in table.Columns)
            {
                if (col.DataType != typeof(string)) continue;

                foreach (DataRow row in table.Rows)
                {
                    if (row[col.ColumnName] != DBNull.Value)
                    {
                        row[col.ColumnName] = Decode((string)row[col.ColumnName]);
                    }
                }
            }

            //table.AcceptChanges();
        }

    }
}
