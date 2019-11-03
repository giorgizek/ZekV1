using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace Zek.Data
{
    public class OleDbParameterCache
    {
        #region private methods, variables, and constructors

        //Since this class provides only static methods, make the default constructor private to prevent 
        //instances from being created with "new OleDbParameterCache()"
        private OleDbParameterCache() { }

        private static readonly Hashtable ParamCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Resolve at run time the appropriate set of OleDbParameters for a stored procedure
        /// </summary>
        /// <param name="connection">A valid OleDbConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">Whether or not to include their return value parameter</param>
        /// <returns>The parameter array discovered.</returns>
        private static OleDbParameter[] DiscoverSpParameterSet(OleDbConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            var cmd = new OleDbCommand(spName, connection) { CommandType = CommandType.StoredProcedure };

            connection.Open();
            OleDbCommandBuilder.DeriveParameters(cmd);
            connection.Close();

            if (!includeReturnValueParameter)
            {
                cmd.Parameters.RemoveAt(0);
            }

            var discoveredParameters = new OleDbParameter[cmd.Parameters.Count];

            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // Init the parameters with a DBNull value
            foreach (var discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }
            return discoveredParameters;
        }

        /// <summary>
        /// Deep copy of cached OleDbParameter array
        /// </summary>
        /// <param name="originalParameters"></param>
        /// <returns></returns>
        private static OleDbParameter[] CloneParameters(OleDbParameter[] originalParameters)
        {
            var clonedParameters = new OleDbParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (OleDbParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion private methods, variables, and constructors

        #region caching functions

        /// <summary>
        /// Add parameter array to the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParamters to be cached</param>
        public static void CacheParameterSet(string connectionString, string commandText, params OleDbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText));

            var hashKey = connectionString + ":" + commandText;

            ParamCache[hashKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An array of SqlParamters</returns>
        public static OleDbParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText));

            var hashKey = connectionString + ":" + commandText;

            var cachedParameters = ParamCache[hashKey] as OleDbParameter[];
            if (cachedParameters == null)
            {
                return null;
            }
            return CloneParameters(cachedParameters);
        }

        #endregion caching functions

        #region Parameter Discovery Functions


        /// <summary>
        /// Retrieves the set of OleDbParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a OleDbConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
        /// <returns>An array of OleDbParameters</returns>
        public static OleDbParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter = false)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            using (var connection = new OleDbConnection(connectionString))
            {
                return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
        }


        /// <summary>
        /// Retrieves the set of OleDbParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connection">A valid OleDbConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
        /// <returns>An array of OleDbParameters</returns>
        internal static OleDbParameter[] GetSpParameterSet(OleDbConnection connection, string spName, bool includeReturnValueParameter = false)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            using (var clonedConnection = (OleDbConnection)((ICloneable)connection).Clone())
            {
                return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>
        /// Retrieves the set of OleDbParameters appropriate for the stored procedure
        /// </summary>
        /// <param name="connection">A valid OleDbConnection object</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
        /// <returns>An array of OleDbParameters</returns>
        private static OleDbParameter[] GetSpParameterSetInternal(OleDbConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

            var hashKey = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            var cachedParameters = ParamCache[hashKey] as OleDbParameter[];
            if (cachedParameters == null)
            {
                var spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                ParamCache[hashKey] = spParameters;
                cachedParameters = spParameters;
            }

            return CloneParameters(cachedParameters);
        }

        #endregion Parameter Discovery Functions
    }
}
