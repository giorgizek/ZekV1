using System;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;

namespace Zek.Data
{
    public class OleDbHelper : DbHelper
    {

        public static Type GetTypeFromOleDbType(OleDbType oleDbType)
        {
            switch (oleDbType)
            {
                case OleDbType.Empty: return typeof(Nullable);
                case OleDbType.SmallInt: return typeof(Int16);
                case OleDbType.Integer: return typeof(Int32);
                case OleDbType.Single: return typeof(Single);
                case OleDbType.Double: return typeof(Double);
                case OleDbType.Currency: return typeof(Decimal);
                case OleDbType.Date: return typeof(DateTime);
                case OleDbType.BSTR: return typeof(String);
                case OleDbType.IDispatch: return typeof(Object);
                case OleDbType.Error: return typeof(Exception);
                case OleDbType.Boolean: return typeof(Boolean);
                case OleDbType.Variant: return typeof(Object);
                case OleDbType.IUnknown: return typeof(Object);
                case OleDbType.Decimal: return typeof(Decimal);
                case OleDbType.TinyInt: return typeof(SByte);
                case OleDbType.UnsignedTinyInt: return typeof(Byte);
                case OleDbType.UnsignedSmallInt: return typeof(UInt16);
                case OleDbType.UnsignedInt: return typeof(UInt32);
                case OleDbType.BigInt: return typeof(Int64);
                case OleDbType.UnsignedBigInt: return typeof(UInt64);
                case OleDbType.Filetime: return typeof(DateTime);
                case OleDbType.Guid: return typeof(Guid);
                case OleDbType.Binary: return typeof(Byte[]);
                case OleDbType.Char: return typeof(String);
                case OleDbType.WChar: return typeof(String);
                case OleDbType.Numeric: return typeof(Decimal);
                case OleDbType.DBDate: return typeof(DateTime);
                case OleDbType.DBTime: return typeof(TimeSpan);
                case OleDbType.DBTimeStamp: return typeof(DateTime);
                case OleDbType.PropVariant: return typeof(Object);
                case OleDbType.VarNumeric: return typeof(Decimal);
                case OleDbType.VarChar: return typeof(String);
                case OleDbType.LongVarChar: return typeof(String);
                case OleDbType.VarWChar: return typeof(String);
                case OleDbType.LongVarWChar: return typeof(String);
                case OleDbType.VarBinary: return typeof(Byte[]);
                case OleDbType.LongVarBinary: return typeof(Byte[]);
            }
            throw new Exception("DataType Not Supported");
        }


        //public static DataTable GetDataTable(string connectionString, string selectCommand)
        //{
        //    return GetDataTable(new OleDbConnection(connectionString), selectCommand);
        //}
        //public static DataTable GetDataTable(OleDbConnection conn, string selectCommand)
        //{
        //    try
        //    {
        //        if (conn.State == ConnectionState.Closed)
        //            conn.Open();


        //        return GetDataTable(new OleDbCommand(selectCommand, conn));
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
        //public static DataTable GetDataTable(string connectionString, OleDbCommand cmd)
        //{
        //    cmd.Connection = new OleDbConnection(connectionString);

        //    return GetDataTable(cmd);
        //}
        //public static DataTable GetDataTable(OleDbCommand cmd)
        //{
        //    var adapter = new OleDbDataAdapter(cmd);
        //    var table = new DataTable();
        //    adapter.Fill(table);
        //    return table;
        //}


        //public static void ExecuteNonQuery(string connectionString, string sql)
        //{
        //    ExecuteNonQuery(connectionString, new OleDbCommand(sql));
        //}
        //public static void ExecuteNonQuery(string connectionString, OleDbCommand cmd)
        //{
        //    using (var conn = new OleDbConnection(connectionString))
        //    {
        //        try
        //        {
        //            ExecuteNonQuery(conn, cmd);
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //}
        //public static void ExecuteNonQuery(OleDbConnection conn, string sql)
        //{
        //    ExecuteNonQuery(conn, new OleDbCommand(sql));
        //}
        //public static void ExecuteNonQuery(OleDbConnection conn, OleDbCommand cmd)
        //{
        //    cmd.Connection = conn;

        //    if (conn.State == ConnectionState.Closed)
        //        conn.Open();

        //    cmd.ExecuteNonQuery();
        //}





















        #region private utility methods & constructors

        // Since this class provides only static methods, make the default constructor private to prevent 
        // instances from being created with "new Sql()"
        protected OleDbHelper() { }

        /// <summary>
        /// This method is used to attach array of OleDbParameters to a OleDbCommand.
        /// 
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.  
        /// 
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">An array of OleDbParameters to be added to command</param>
        private static void AttachParameters(OleDbCommand command, IEnumerable<OleDbParameter> commandParameters)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (commandParameters != null)
            {
                foreach (var p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        ///// <summary>
        ///// This method assigns dataRow column values to an array of OleDbParameters
        ///// </summary>
        ///// <param name="commandParameters">Array of OleDbParameters to be assigned values</param>
        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values</param>
        //private static void AssignParameterValues(OleDbParameter[] commandParameters, DataRow dataRow)
        //{
        //    if ((commandParameters == null) || (dataRow == null))
        //    {
        //        // Do nothing if we get no data
        //        return;
        //    }

        //    var i = 0;
        //    // Set the parameters values
        //    foreach (var commandParameter in commandParameters)
        //    {
        //        // Check the parameter name
        //        if (commandParameter.ParameterName == null || commandParameter.ParameterName.Length <= 1)
        //            throw new Exception(string.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.", i, commandParameter.ParameterName));
        //        if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
        //            commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
        //        i++;
        //    }
        //}

        /// <summary>
        /// This method assigns an array of values to an array of OleDbParameters
        /// </summary>
        /// <param name="commandParameters">Array of OleDbParameters to be assigned values</param>
        /// <param name="parameterValues">Array of objects holding the values to be assigned</param>
        private static void AssignParameterValues(OleDbParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                // Do nothing if we get no data
                return;
            }

            // We must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            // Iterate through the OleDbParameters, assigning the values from the corresponding position in the 
            // value array
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                // If the current array value derives from IDbDataParameter, then assign its Value property
                var dbDataParameter = parameterValues[i] as IDbDataParameter;
                if (dbDataParameter != null)
                {
                    var paramInstance = dbDataParameter;
                    commandParameters[i].Value = paramInstance.Value ?? DBNull.Value;
                }
                else if (parameterValues[i] == null)
                {
                    commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    commandParameters[i].Value = parameterValues[i];
                }
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
        /// to the provided command
        /// </summary>
        /// <param name="command">The OleDbCommand to be prepared</param>
        /// <param name="connection">A valid OleDbConnection, on which to execute this command</param>
        /// <param name="transaction">A valid OleDbTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of OleDbParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
        private static void PrepareCommand(OleDbCommand command, OleDbConnection connection, OleDbTransaction transaction, CommandType commandType, string commandText, int? commandTimeout, OleDbParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText));

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            if (commandTimeout != null && commandTimeout.Value > 0)
                command.CommandTimeout = commandTimeout.Value;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        #endregion private utility methods & constructors

        #region ExecuteNonQuery
        /// <summary>
        /// Execute a OleDbCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            // Create & open a OleDbConnection, and dispose of it after we are done
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteNonQuery(connection, commandType, commandText, commandTimeout, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns no resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="spName">The name of the stored prcedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OleDbParameters
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        }


        /// <summary>
        /// Execute a OleDbCommand (that returns no resultset) against the specified OleDbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(OleDbConnection connection, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            // Create a command and prepare it for execution
            var cmd = new OleDbCommand();
            bool mustCloseConnection;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandTimeout, commandParameters, out mustCloseConnection);

            // Finally, execute the command
            var result = cmd.ExecuteNonQuery();

            // Detach the OleDbParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return result;
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns no resultset) against the specified OleDbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(OleDbConnection connection, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OleDbParameters
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
        }


        /// <summary>
        /// Execute a OleDbCommand (that returns no resultset) against the specified OleDbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid OleDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(OleDbTransaction transaction, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));

            // Create a command and prepare it for execution
            var cmd = new OleDbCommand();
            bool mustCloseConnection;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandTimeout, commandParameters, out mustCloseConnection);

            // Finally, execute the command
            var retval = cmd.ExecuteNonQuery();

            // Detach the OleDbParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns no resultset) against the specified 
        /// OleDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, trans, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid OleDbTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(OleDbTransaction transaction, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OleDbParameters
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
        }

        #endregion ExecuteNonQuery

        #region ExecuteDataTable
        /// <summary>
        /// Execute a OleDbCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataTable ds = ExecuteDataTable(connString, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A DataTable containing the resultset generated by the command</returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            // Create & open a OleDbConnection, and dispose of it after we are done
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteDataTable(connection, commandType, commandText, commandTimeout, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataTable ds = ExecuteDataTable(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DataTable containing the resultset generated by the command</returns>
        public static DataTable ExecuteDataTable(string connectionString, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OleDbParameters
                return ExecuteDataTable(connectionString, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteDataTable(connectionString, CommandType.StoredProcedure, spName);
        }


        /// <summary>
        /// Execute a OleDbCommand (that returns a resultset) against the specified OleDbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataTable ds = ExecuteDataTable(conn, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A DataTable containing the resultset generated by the command</returns>
        public static DataTable ExecuteDataTable(OleDbConnection connection, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            // Create a command and prepare it for execution
            var cmd = new OleDbCommand();
            bool mustCloseConnection;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandTimeout, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataTable
            using (var da = new OleDbDataAdapter(cmd))
            {
                var ds = new DataTable();

                // Fill the DataTable using default values for DataTable names, etc
                da.Fill(ds);

                // Detach the OleDbParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                if (mustCloseConnection)
                    connection.Close();

                // Return the DataTable
                return ds;
            }
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns a resultset) against the specified OleDbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataTable ds = ExecuteDataTable(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DataTable containing the resultset generated by the command</returns>
        public static DataTable ExecuteDataTable(OleDbConnection connection, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OleDbParameters
                return ExecuteDataTable(connection, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteDataTable(connection, CommandType.StoredProcedure, spName);
        }


        /// <summary>
        /// Execute a OleDbCommand (that returns a resultset) against the specified OleDbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataTable ds = ExecuteDataTable(trans, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid OleDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A DataTable containing the resultset generated by the command</returns>
        public static DataTable ExecuteDataTable(OleDbTransaction transaction, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));

            // Create a command and prepare it for execution
            var cmd = new OleDbCommand();
            bool mustCloseConnection;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandTimeout, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataTable
            using (var da = new OleDbDataAdapter(cmd))
            {
                var table = new DataTable();

                // Fill the DataTable using default values for DataTable names, etc
                da.Fill(table);

                // Detach the OleDbParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                // Return the DataTable
                return table;
            }
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns a resultset) against the specified 
        /// OleDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataTable ds = ExecuteDataTable(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid OleDbTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DataTable containing the resultset generated by the command</returns>
        public static DataTable ExecuteDataTable(OleDbTransaction transaction, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OleDbParameters
                return ExecuteDataTable(transaction, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteDataTable(transaction, CommandType.StoredProcedure, spName);
        }

        #endregion ExecuteDataSet

        #region ExecuteDataSet


        /// <summary>
        /// Execute a OleDbCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataSet(connString, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            // Create & open a OleDbConnection, and dispose of it after we are done
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteDataSet(connection, commandType, commandText, commandTimeout, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataSet(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(string connectionString, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OleDbParameters
                return ExecuteDataSet(connectionString, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteDataSet(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// Execute a OleDbCommand (that returns a resultset) against the specified OleDbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataSet(conn, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(OleDbConnection connection, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            // Create a command and prepare it for execution
            var cmd = new OleDbCommand();
            bool mustCloseConnection;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandTimeout, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (var da = new OleDbDataAdapter(cmd))
            {
                var ds = new DataSet();

                // Fill the DataSet using default values for DataTable names, etc
                da.Fill(ds);

                // Detach the OleDbParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                if (mustCloseConnection)
                    connection.Close();

                // Return the dataset
                return ds;
            }
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns a resultset) against the specified OleDbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataSet(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(OleDbConnection connection, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OleDbParameters
                return ExecuteDataSet(connection, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteDataSet(connection, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// Execute a OleDbCommand (that returns a resultset) against the specified OleDbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataSet(trans, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid OleDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(OleDbTransaction transaction, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));

            // Create a command and prepare it for execution
            var cmd = new OleDbCommand();
            bool mustCloseConnection;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandTimeout, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (var da = new OleDbDataAdapter(cmd))
            {
                var ds = new DataSet();

                // Fill the DataSet using default values for DataTable names, etc
                da.Fill(ds);

                // Detach the OleDbParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                // Return the dataset
                return ds;
            }
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns a resultset) against the specified 
        /// OleDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataSet(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid OleDbTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(OleDbTransaction transaction, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OleDbParameters
                return ExecuteDataSet(transaction, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteDataSet(transaction, CommandType.StoredProcedure, spName);
        }

        #endregion ExecuteDataSet

        #region ExecuteReader

        /// <summary>
        /// This enum is used to indicate whether the connection was provided by the caller, or created by Sql, so that
        /// we can set the appropriate CommandBehavior when calling ExecuteReader()
        /// </summary>
        [Serializable]
        private enum OleDbConnectionOwnership
        {
            /// <summary>Connection is owned and managed by Sql</summary>
            Internal,
            /// <summary>Connection is owned and managed by the caller</summary>
            External
        }

        /// <summary>
        /// Create and prepare a OleDbCommand, and call ExecuteReader with the appropriate CommandBehavior.
        /// </summary>
        /// <remarks>
        /// If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
        /// 
        /// If the caller provided the connection, we want to leave it to them to manage.
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection, on which to execute this command</param>
        /// <param name="transaction">A valid OleDbTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of OleDbParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="connectionOwnership">Indicates whether the connection parameter was provided by the caller, or created by Sql</param>
        /// <returns>OleDbDataReader containing the results of the command</returns>
        private static OleDbDataReader ExecuteReader(OleDbConnection connection, OleDbTransaction transaction, CommandType commandType, string commandText, int? commandTimeout, OleDbParameter[] commandParameters, OleDbConnectionOwnership connectionOwnership)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            var mustCloseConnection = false;
            // Create a command and prepare it for execution
            var cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, connection, transaction, commandType, commandText, commandTimeout, commandParameters, out mustCloseConnection);

                // Create a reader

                // Call ExecuteReader with the appropriate CommandBehavior
                var dataReader = connectionOwnership == OleDbConnectionOwnership.External
                    ? cmd.ExecuteReader()
                    : cmd.ExecuteReader(CommandBehavior.CloseConnection);

                // Detach the OleDbParameters from the command object, so they can be used again.
                // HACK: There is a problem here, the output parameter values are fletched 
                // when the reader is closed, so if the parameters are detached from the command
                // then the SqlReader canґt set its values. 
                // When this happen, the parameters canґt be used again in other command.
                var canClear = true;
                foreach (OleDbParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                        canClear = false;
                }

                if (canClear)
                {
                    cmd.Parameters.Clear();
                }

                return dataReader;
            }
            catch
            {
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
        }


        /// <summary>
        /// Execute a OleDbCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  OleDbDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A OleDbDataReader containing the resultset generated by the command</returns>
        public static OleDbDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            OleDbConnection connection = null;
            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();

                // Call the private overload that takes an internally owned connection in place of the connection string
                return ExecuteReader(connection, null, commandType, commandText, commandTimeout, commandParameters, OleDbConnectionOwnership.Internal);
            }
            catch
            {
                // If we fail to return the SqlDatReader, we need to close the connection ourselves
                if (connection != null) connection.Close();
                throw;
            }

        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  OleDbDataReader dr = ExecuteReader(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A OleDbDataReader containing the resultset generated by the command</returns>
        public static OleDbDataReader ExecuteReader(string connectionString, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                var commandParameters = OleDbParameterCache.GetSpParameterSet(connectionString, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// Execute a OleDbCommand (that returns a resultset) against the specified OleDbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  OleDbDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A OleDbDataReader containing the resultset generated by the command</returns>
        public static OleDbDataReader ExecuteReader(OleDbConnection connection, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            // Pass through the call to the private overload using a null transaction value and an externally owned connection
            return ExecuteReader(connection, null, commandType, commandText, commandTimeout, commandParameters, OleDbConnectionOwnership.External);
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns a resultset) against the specified OleDbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  OleDbDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A OleDbDataReader containing the resultset generated by the command</returns>
        public static OleDbDataReader ExecuteReader(OleDbConnection connection, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                var commandParameters = OleDbParameterCache.GetSpParameterSet(connection, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteReader(connection, CommandType.StoredProcedure, spName);
        }



        /// <summary>
        /// Execute a OleDbCommand (that returns a resultset) against the specified OleDbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   OleDbDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid OleDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>A OleDbDataReader containing the resultset generated by the command</returns>
        public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));

            // Pass through to private overload, indicating that the connection is owned by the caller
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandTimeout, commandParameters, OleDbConnectionOwnership.External);
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns a resultset) against the specified
        /// OleDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  OleDbDataReader dr = ExecuteReader(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid OleDbTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A OleDbDataReader containing the resultset generated by the command</returns>
        public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                var commandParameters = OleDbParameterCache.GetSpParameterSet(transaction.Connection, spName);

                AssignParameterValues(commandParameters, parameterValues);

                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
        }

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// Execute a OleDbCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            // Create & open a OleDbConnection, and dispose of it after we are done
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteScalar(connection, commandType, commandText, commandTimeout, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns a 1x1 resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(connectionString, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OleDbParameters
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// Execute a OleDbCommand (that returns a 1x1 resultset) against the specified OleDbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(OleDbConnection connection, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));

            // Create a command and prepare it for execution
            var cmd = new OleDbCommand();

            bool mustCloseConnection;
            PrepareCommand(cmd, connection, null, commandType, commandText, commandTimeout, commandParameters, out mustCloseConnection);

            // Execute the command & return the results
            var retval = cmd.ExecuteScalar();

            // Detach the OleDbParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            if (mustCloseConnection)
                connection.Close();

            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns a 1x1 resultset) against the specified OleDbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(OleDbConnection connection, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OleDbParameters
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
        }


        /// <summary>
        /// Execute a OleDbCommand (that returns a 1x1 resultset) against the specified OleDbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new OleDbParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid OleDbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandTimeout">The stored procedure or T-SQL command timeout</param>
        /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(OleDbTransaction transaction, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));

            // Create a command and prepare it for execution
            var cmd = new OleDbCommand();
            bool mustCloseConnection;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandTimeout, commandParameters, out mustCloseConnection);

            // Execute the command & return the results
            var retval = cmd.ExecuteScalar();

            // Detach the OleDbParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a OleDbCommand (that returns a 1x1 resultset) against the specified
        /// OleDbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid OleDbTransaction</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="commandTimeout">The stored procedure timeout</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(OleDbTransaction transaction, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != null && transaction.Connection == null) throw new ArgumentException(@"The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // PPull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(transaction.Connection, spName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of OleDbParameters
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandTimeout, commandParameters);
            }
            // Otherwise we can just call the SP without params
            return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
        }

        #endregion ExecuteScalar
        
       
        #region UpdateDataSet
        /// <summary>
        /// Executes the respective command for each inserted, updated, or deleted row in the DataSet.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  UpdateDataSet(conn, insertCommand, deleteCommand, updateCommand, dataSet, "Order");
        /// </remarks>
        /// <param name="insertCommand">A valid transact-SQL statement or stored procedure to insert new records into the data source</param>
        /// <param name="deleteCommand">A valid transact-SQL statement or stored procedure to delete records from the data source</param>
        /// <param name="updateCommand">A valid transact-SQL statement or stored procedure used to update records in the data source</param>
        /// <param name="dataSet">The DataSet used to update the data source</param>
        /// <param name="tableName">The DataTable used to update the data source.</param>
        public static void UpdateDataSet(OleDbCommand insertCommand, OleDbCommand deleteCommand, OleDbCommand updateCommand, DataSet dataSet, string tableName)
        {
            if (insertCommand == null) throw new ArgumentNullException(nameof(insertCommand));
            if (deleteCommand == null) throw new ArgumentNullException(nameof(deleteCommand));
            if (updateCommand == null) throw new ArgumentNullException(nameof(updateCommand));
            if (string.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName));

            // Create a OleDbDataAdapter, and dispose of it after we are done
            using (var dataAdapter = new OleDbDataAdapter())
            {
                // Set the data adapter commands
                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.InsertCommand = insertCommand;
                dataAdapter.DeleteCommand = deleteCommand;

                // Update the dataset changes in the data source
                dataAdapter.Update(dataSet, tableName);

                // Commit all the changes made to the DataSet
                dataSet.AcceptChanges();
            }
        }
        #endregion

        #region CreateCommand
        /// <summary>
        /// Simplify the creation of a Sql command object by allowing
        /// a stored procedure and optional parameters to be provided
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  OleDbCommand command = CreateCommand(conn, "AddCustomer", "CustomerID", "CustomerName");
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="sourceColumns">An array of string to be assigned as the source columns of the stored procedure parameters</param>
        /// <returns>A valid OleDbCommand object</returns>
        public static OleDbCommand CreateCommand(OleDbConnection connection, string spName, params string[] sourceColumns)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            // Create a OleDbCommand
            var cmd = new OleDbCommand(spName, connection) { CommandType = CommandType.StoredProcedure };

            // If we receive parameter values, we need to figure out where they go
            if ((sourceColumns != null) && (sourceColumns.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                var commandParameters = OleDbParameterCache.GetSpParameterSet(connection, spName);

                // Assign the provided source columns to these parameters based on parameter order
                for (var index = 0; index < sourceColumns.Length; index++)
                    commandParameters[index].SourceColumn = sourceColumns[index];

                // Attach the discovered parameters to the OleDbCommand object
                AttachParameters(cmd, commandParameters);
            }

            return cmd;
        }
        #endregion


        #region ExecuteList & DataReaderMapToList
        public static List<T> ExecuteList<T>(string connectionString, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            return DataReaderMapToList<T>(ExecuteReader(connectionString, commandType, commandText, commandTimeout, commandParameters));
        }
        public static List<T> ExecuteList<T>(string connectionString, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            return DataReaderMapToList<T>(ExecuteReader(connectionString, spName, commandTimeout, parameterValues));
        }
        public static List<T> ExecuteList<T>(OleDbConnection connection, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            return DataReaderMapToList<T>(ExecuteReader(connection, commandType, commandText, commandTimeout, commandParameters));
        }
        public static List<T> ExecuteList<T>(OleDbConnection connection, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            return DataReaderMapToList<T>(ExecuteReader(connection, spName, commandTimeout, parameterValues));
        }
        public static List<T> ExecuteList<T>(OleDbTransaction transaction, CommandType commandType, string commandText, int? commandTimeout = null, params OleDbParameter[] commandParameters)
        {
            return DataReaderMapToList<T>(ExecuteReader(transaction, commandType, commandText, commandTimeout, commandParameters));
        }
        public static List<T> ExecuteList<T>(OleDbTransaction transaction, string spName, int? commandTimeout = null, params object[] parameterValues)
        {
            return DataReaderMapToList<T>(ExecuteReader(transaction, spName, commandTimeout, parameterValues));
        }
        #endregion



        /// <summary>
        /// ქმნის ახალ ტრანსაქციას.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static OleDbTransaction BeginTransaction(string connectionString)
        {
            var conn = new OleDbConnection(connectionString);
            conn.Open();
            return conn.BeginTransaction();
        }
        /// <summary>
        /// აქომიტებს და ხურავს კავშირს ბაზასთან.
        /// </summary>
        /// <param name="tran"></param>
        public static void Commit(OleDbTransaction tran)
        {
            tran.Commit();
            CloseConnection(tran);
        }
        /// <summary>
        /// ხურავს კავშირს ბაზასთან.
        /// </summary>
        /// <param name="tran"></param>
        public static void CloseConnection(OleDbTransaction tran)
        {
            if (tran.Connection != null) tran.Connection.Close();
        }
        /// <summary>
        /// ხურავს კავშირს ბაზასთან.
        /// </summary>
        /// <param name="cmd"></param>
        public static void CloseConnection(OleDbCommand cmd)
        {
            if (cmd.Connection != null) cmd.Connection.Close();
        }

     #region Filter
        /// <summary>
        /// ქმნის დაცულ პარამეტრს (მაგ. თუ თარიღი ძალიან დაბალი ან მაქსიმალურია...).
        /// </summary>
        /// <param name="dbParameter">Sql პარამეტრი.</param>
        public static void DoSafeParameter(ref OleDbParameter dbParameter)
        {
            if (dbParameter.Direction == ParameterDirection.Input || dbParameter.Direction == ParameterDirection.InputOutput)
            {
                if (dbParameter.Value == null)
                {
                    dbParameter.Value = DBNull.Value;
                }
                //else if (dbParameter.OleDbType == OleDbType.Date || dbParameter.OleDbType == OleDbType.DBDate)
                //{
                //    if (((DateTime)dbParameter.Value) < System.Data.SqlTypes.SqlDateTime.MinValue.Value)
                //    {
                //        dbParameter.Value = SqlDateTime.MinValue.Value;
                //    }
                //    else if (((DateTime)dbParameter.Value) > SqlDateTime.MaxValue.Value)
                //    {
                //        dbParameter.Value = SqlDateTime.MaxValue.Value;
                //    }
                //}
            }
        }
        #endregion
    }

}
