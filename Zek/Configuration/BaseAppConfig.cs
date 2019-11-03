using System.Configuration;
using Zek.Data;
using Zek.Security;

namespace Zek.Configuration
{
    public class BaseAppConfig : AppConfigHelper
    {
        #region Crypto
        public static string Salt = string.Empty;
        public static string Key = string.Empty;
        public static string IV = string.Empty;
        #endregion

        #region DB
        public static string Server { get; set; }

        public static string Database { get; set; }

        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static int? ConnectionTimeout { get; set; }

        public static int? CommandTimeout { get; set; }

        /// <summary>
        /// მონაცემთა ბაზის ოფლაინ ConnectionString.
        /// </summary>
        public static string ConnectionString { get; set; }
        #endregion


        #region Login
        /// <summary>
        /// მომხმარებელი.
        /// </summary>
        public static string LoginUserName { get; set; }

        /// <summary>
        /// მომხმარებლის პაროლი.
        /// </summary>
        public static string LoginPassword { get; set; }
        #endregion

        /// <summary>
        /// კონფიგურაციის ჩატვირთვა.
        /// </summary>
        protected static void BaseLoad()
        {
            Server = GetNullableString("Server");
            Database = GetNullableString("Database");
            UserName = GetNullableString("UserName");
            if (!string.IsNullOrWhiteSpace(GetNullableString("Password")))
                Password = SymCryptoHelper.TripleDESDecrypt(GetNullableString("Password"), Salt, Key, IV);
            ConnectionTimeout = GetNullableInt32("ConnectionTimeout");
            CommandTimeout = GetNullableInt32("CommandTimeout");

            LoginUserName = GetNullableString("LoginUserName");
            LoginPassword = GetNullableString("LoginPassword");

            BaseBuilderConnectionStrings();
        }

        /// <summary>
        /// კონფიგურაციის დამახსოვრება.
        /// </summary>
        protected static void BaseSave()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            Set(config, "Server", Server);
            Set(config, "Database", Database);
            Set(config, "UserName", UserName);
            Set(config, "Password", !string.IsNullOrWhiteSpace(Password) ? SymCryptoHelper.TripleDESEncrypt(Password, Salt, Key, IV) : null);
            Set(config, "ConnectionTimeout", ConnectionTimeout);
            Set(config, "CommandTimeout", CommandTimeout);

            Set(config, "LoginUserName", LoginUserName);
            Set(config, "LoginPassword", LoginPassword);

            config.Save();
            BaseBuilderConnectionStrings();
        }

        /// <summary>
        /// ბაზის კონექშენების აწყობა.
        /// </summary>
        protected static void BaseBuilderConnectionStrings()
        {
            ConnectionString = SqlConnectionStringHelper.GetConnectionString(Server, Database, UserName, Password, null, true, ConnectionTimeout);
        }
    }
}