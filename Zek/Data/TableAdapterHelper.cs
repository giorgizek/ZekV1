using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using Zek.Configuration;

namespace Zek.Data
{
    public class TableAdapterHelper
    {
        public static SqlDataAdapter GetAdapter(object tableAdapter)
        {
            var tableAdapterType = tableAdapter.GetType();
            object propertyInfo = tableAdapterType.GetProperty("Adapter", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (propertyInfo == null) return null;

            var adapter = (SqlDataAdapter)((PropertyInfo)propertyInfo).GetValue(tableAdapter, null);
            return adapter;
        }
        public static IDbCommand[] GetCommandCollection(object tableAdapter)
        {
            var tableAdapterType = tableAdapter.GetType();
            var propertyInfo = tableAdapterType.GetProperty("CommandCollection", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (propertyInfo == null) return null;

            var commandCollection = (IDbCommand[])propertyInfo.GetValue(tableAdapter, null);
            return commandCollection;
        }

        public static void SetTransaction(object tableAdapter, SqlTransaction tran)
        {
            var adapter = GetAdapter(tableAdapter);
            SetTransaction(adapter, tran);

            var commandCollection = GetCommandCollection(tableAdapter);
            SetTransaction(commandCollection, tran);
        }
        public static void SetTransaction(SqlDataAdapter adapter, SqlTransaction tran)
        {
            if (adapter == null) return;
            if (adapter.InsertCommand != null)
            {
                adapter.InsertCommand.Connection = tran.Connection;
                adapter.InsertCommand.Transaction = tran;
            }

            if (adapter.UpdateCommand != null)
            {
                adapter.UpdateCommand.Connection = tran.Connection;
                adapter.UpdateCommand.Transaction = tran;
            }

            if (adapter.DeleteCommand != null)
            {
                adapter.DeleteCommand.Connection = tran.Connection;
                adapter.DeleteCommand.Transaction = tran;
            }
        }
        public static void SetTransaction(IDbCommand[] commandCollection, SqlTransaction tran)
        {
            if (commandCollection == null) return;

            foreach (var cmd in commandCollection)
            {
                cmd.Transaction = tran;
                cmd.Connection = tran.Connection;
            }
        }

        public static void SetConnection(object tableAdapter, SqlConnection conn)
        {
            var adapter = GetAdapter(tableAdapter);
            SetConnection(adapter, conn);

            var commandCollection = GetCommandCollection(tableAdapter);
            SetConnection(commandCollection, conn);
        }
        public static void SetConnection(SqlDataAdapter adapter, SqlConnection conn)
        {
            if (adapter == null) return;

            if (adapter.InsertCommand != null)
            {
                adapter.InsertCommand.Connection = conn;
            }

            if (adapter.UpdateCommand != null)
            {
                adapter.UpdateCommand.Connection = conn;
            }

            if (adapter.DeleteCommand != null)
            {
                adapter.DeleteCommand.Connection = conn;
            }
        }
        public static void SetConnection(IDbCommand[] commandCollection, SqlConnection conn)
        {
            if (commandCollection == null) return;

            foreach (var cmd in commandCollection)
            {
                cmd.Connection = conn;
            }
        }

        public static void SetConnectionString(object tableAdapter, string connectionString)
        {
            var adapter = GetAdapter(tableAdapter);
            SetConnectionString(adapter, connectionString);

            var commandCollection = GetCommandCollection(tableAdapter);
            SetConnectionString(commandCollection, connectionString);
        }
        public static void SetConnectionString(SqlDataAdapter adapter, string connectionString)
        {
            if (adapter == null) return;

            if (adapter.InsertCommand != null)
            {
                adapter.InsertCommand.Connection.ConnectionString = connectionString;
            }

            if (adapter.UpdateCommand != null)
            {
                adapter.UpdateCommand.Connection.ConnectionString = connectionString;
            }

            if (adapter.DeleteCommand != null)
            {
                adapter.DeleteCommand.Connection.ConnectionString = connectionString;
            }
        }
        public static void SetConnectionString(IDbCommand[] commandCollection, string connectionString)
        {
            if (commandCollection == null) return;

            foreach (var cmd in commandCollection)
            {
                cmd.Connection.ConnectionString = connectionString;
            }
        }


        /// <summary>
        /// უკეთებს ინციალიზაციას commandCollection-ს (ანიჭებს ConnectionString, CommandTimeout...).
        /// </summary>
        /// <param name="tableAdapter"></param>
        /// <param name="connectionString"></param>
        /// <param name="commandTimeout"></param>
        public static void CreateInstance(object tableAdapter, string connectionString = null, int? commandTimeout = null)
        {
            var commandCollection = GetCommandCollection(tableAdapter);
            CreateInstance(commandCollection, connectionString, commandTimeout);
        }

        /// <summary>
        /// უკეთებს ინციალიზაციას commandCollection-ს (ანიჭებს ConnectionString, CommandTimeout...).
        /// DatabaseHelper.ConnectionString
        /// </summary>
        /// <param name="commandCollection"></param>
        public static void CreateInstance(IDbCommand[] commandCollection)
        {
            CreateInstance(commandCollection, BaseAppConfig.ConnectionString);
        }

        /// <summary>
        /// უკეთებს ინციალიზაციას commandCollection-ს (ანიჭებს ConnectionString, CommandTimeout...).
        /// </summary>
        /// <param name="commandCollection"></param>
        /// <param name="connectionString"></param>
        /// <param name="commandTimeout"></param>
        public static void CreateInstance(IDbCommand[] commandCollection, string connectionString, int? commandTimeout = null)
        {
            if (commandCollection == null || commandCollection.Length == 0) return;

            var isNullOrWhiteSpace = string.IsNullOrWhiteSpace(connectionString);
            foreach (var cmd in commandCollection)
            {
                if (!isNullOrWhiteSpace)
                    cmd.Connection.ConnectionString = connectionString;

                if (commandTimeout != null)
                    cmd.CommandTimeout = commandTimeout.Value;
            }
        }

        /// <summary>
        /// უკეთებს ინციალიზაციას commandCollection-ს (ანიჭებს ConnectionString, CommandTimeout...).
        /// </summary>
        /// <param name="commandCollection"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        public static void CreateInstance(IDbCommand[] commandCollection, SqlTransaction tran, int? commandTimeout)
        {
            SetTransaction(commandCollection, tran);
            if (commandTimeout != null)
            {
                foreach (var cmd in commandCollection)
                {
                    cmd.CommandTimeout = commandTimeout.Value;
                }
            }
        }
    }
}
