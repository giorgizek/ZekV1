using System.Configuration;
using Zek.Extensions;
using Zek.Data;
using Zek.Security;

namespace Zek.Configuration
{
    public class BaseOfflineAppConfig : AppConfigHelper
    {
        #region Crypto
        public static string Salt = string.Empty;
        public static string Key = string.Empty;
        public static string IV = string.Empty;
        #endregion

        #region Online
        private static string _onlineServerName = string.Empty;
        public static string OnlineServerName
        {
            get { return _onlineServerName; }
            set { _onlineServerName = value.IfNullEmpty(); }
        }

        private static string _onlineDatabaseName = string.Empty;
        public static string OnlineDatabaseName
        {
            get { return _onlineDatabaseName; }
            set { _onlineDatabaseName = value.IfNullEmpty(); }
        }

        private static string _onlineUserName = string.Empty;
        public static string OnlineUserName
        {
            get { return _onlineUserName; }
            set { _onlineUserName = value.IfNullEmpty(); }
        }

        private static string _onlinePassword = string.Empty;
        public static string OnlinePassword
        {
            get { return _onlinePassword; }
            set { _onlinePassword = value.IfNullEmpty(); }
        }

        private static int _onlineConnectionTimeout = 15;
        public static int OnlineConnectionTimeout
        {
            get { return _onlineConnectionTimeout; }
            set
            {
                if (value <= 0) value = 15;
                _onlineConnectionTimeout = value;
            }
        }

        private static int _onlineCommandTimeout = 30;
        public static int OnlineCommandTimeout
        {
            get { return _onlineCommandTimeout; }
            set
            {
                if (value <= 0) value = 30;
                _onlineCommandTimeout = value;
            }
        }

        /// <summary>
        /// მონაცემთა ბაზის ოფლაინ ConnectionString.
        /// </summary>
        public static string OnlineConnectionString { get; set; }
        #endregion

        #region Offline
        public static bool IsOffline { get; set; }

        private static string _offlineServerName = string.Empty;
        public static string OfflineServerName
        {
            get { return _offlineServerName; }
            set { _offlineServerName = value.IfNullEmpty(); }
        }

        private static string _offlineDatabaseName = string.Empty;
        public static string OfflineDatabaseName
        {
            get { return _offlineDatabaseName; }
            set { _offlineDatabaseName = value.IfNullEmpty(); }
        }

        private static string _offlineUserName = string.Empty;
        public static string OfflineUserName
        {
            get { return _offlineUserName; }
            set { _offlineUserName = value.IfNullEmpty(); }
        }

        private static string _offlinePassword = string.Empty;
        public static string OfflinePassword
        {
            get { return _offlinePassword; }
            set { _offlinePassword = value.IfNullEmpty(); }
        }

        private static int _offlineConnectionTimeout = 15;
        public static int OfflineConnectionTimeout
        {
            get { return _offlineConnectionTimeout; }
            set
            {
                if (value <= 0) value = 15;
                _offlineConnectionTimeout = value;
            }
        }

        private static int _offlineCommandTimeout = 30;
        public static int OfflineCommandTimeout
        {
            get { return _offlineCommandTimeout; }
            set
            {
                if (value <= 0) value = 30;
                _offlineCommandTimeout = value;
            }
        }

        /// <summary>
        /// მონაცემთა ბაზის ოფლაინ ConnectionString.
        /// </summary>
        public static string OfflineConnectionString { get; set; }
        #endregion

        #region Dynamic
        private static bool _isOnline = true;
        /// <summary>
        /// როცა true ვიძხებთ OnlineConnectionString-ს, როცა false - OfflineConnectionString-ს.
        /// </summary>
        public static bool IsOnline { get { return _isOnline; } set { _isOnline = value; } }

        /// <summary>
        /// კომანდის თაიმ აუთი.
        /// </summary>
        public static int CommandTimeout => IsOnline ? OnlineCommandTimeout : OfflineCommandTimeout;

        /// <summary>
        /// როცა IsOnline = true აბრუნებს OnlineConnectionString-ს, როცა false მაშინ აბრუნებს OfflineConnectionString-ს.
        /// </summary>
        public static string ConnectionString => _isOnline ? OnlineConnectionString : OfflineConnectionString;

        #endregion

        #region Login
        private static string _loginUserName = string.Empty;
        /// <summary>
        /// მომხმარებელი.
        /// </summary>
        public static string LoginUserName
        {
            get { return _loginUserName; }
            set { _loginUserName = value.IfNullEmpty(); }
        }

        private static string _loginPassword = string.Empty;
        /// <summary>
        /// მომხმარებლის პაროლი.
        /// </summary>
        public static string LoginPassword
        {
            get { return _loginPassword; }
            set { _loginPassword = value.IfNullEmpty(); }
        }

        ///// <summary>
        ///// ავტომატურად ამატებს ახალ ჩანაწერს.
        ///// </summary>
        //public static bool AutoNewRow { get; set; }

        //private static string _SkinName = "Caramel";
        ///// <summary>
        ///// სკინის დასახელება.
        ///// </summary>
        //public static string SkinName
        //{
        //    get { return _SkinName; }
        //    set
        //    {
        //        if (value == null) value = "Caramel";
        //        _SkinName = value;
        //    }
        //}

        ///// <summary>
        ///// ფორმებზე სკინის ჩართვა.
        ///// </summary>
        //public static bool AllowFormSkins { get; set; }
        #endregion

        /// <summary>
        /// კონფიგურაციის ჩატვირთვა.
        /// </summary>
        public static void BaseLoad()
        {
            OnlineServerName = GetString("OnlineServerName");
            OnlineDatabaseName = GetString("OnlineDatabaseName");
            OnlineUserName = GetString("OnlineUserName");
            if (GetString("OnlinePassword").Length > 0)
                OnlinePassword = SymCryptoHelper.TripleDESDecrypt(GetString("OnlinePassword"), Salt, Key, IV);
            OnlineConnectionTimeout = GetInt32("OnlineConnectionTimeout");
            OnlineCommandTimeout = GetInt32("OnlineCommandTimeout");

            IsOffline = GetBool("IsOffline");
            IsOnline = !IsOffline;

            OfflineServerName = GetString("OfflineServerName");
            OfflineDatabaseName = GetString("OfflineDatabaseName");
            OfflineUserName = GetString("OfflineUserName");
            if (GetString("OfflinePassword").Length > 0)
                OfflinePassword = SymCryptoHelper.TripleDESDecrypt(GetString("OfflinePassword"), Salt, Key, IV);
            OfflineConnectionTimeout = GetInt32("OfflineConnectionTimeout");
            OfflineCommandTimeout = GetInt32("OfflineCommandTimeout");

            LoginUserName = GetString("LoginUserName");
            LoginPassword = GetString("LoginPassword");

            //AutoNewRow = GetBool("AutoNewRow");
            //SkinName = GetString("SkinName");
            //AllowFormSkins = GetBool("AllowFormSkins");

            BaseBuilderConnectionStrings();
        }
        /// <summary>
        /// კონფიგურაციის დამახსოვრება.
        /// </summary>
        public static void BaseSave()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            Set(config, "OnlineServerName", OnlineServerName);
            Set(config, "OnlineDatabaseName", OnlineDatabaseName);
            Set(config, "OnlineUserName", OnlineUserName);
            Set(config, "OnlineConnectionTimeout", OnlineConnectionTimeout);
            Set(config, "OnlineCommandTimeout", OnlineCommandTimeout);
            Set(config, "OnlinePassword", OnlinePassword.Length > 0 ? SymCryptoHelper.TripleDESEncrypt(OnlinePassword, Salt, Key, IV) : string.Empty);

            Set(config, "IsOffline", IsOffline);
            if (IsOffline)
            {
                Set(config, "OfflineServerName", OfflineServerName);
                Set(config, "OfflineDatabaseName", OfflineDatabaseName);
                Set(config, "OfflineUserName", OfflineUserName);
                Set(config, "OfflineConnectionTimeout", OfflineConnectionTimeout);
                Set(config, "OfflineCommandTimeout", OfflineCommandTimeout);
                Set(config, "OfflinePassword", OfflinePassword.Length > 0 ? SymCryptoHelper.TripleDESEncrypt(OfflinePassword, Salt, Key, IV) : string.Empty);
            }


            Set(config, "LoginUserName", LoginUserName);
            Set(config, "LoginPassword", LoginPassword);

            //Set(config, "AutoNewRow", AutoNewRow);
            //Set(config, "SkinName", SkinName);
            //Set(config, "AllowFormSkins", AllowFormSkins);

            config.Save();
            BaseBuilderConnectionStrings();
        }

        /// <summary>
        /// ბაზის კონექშენების აწყობა.
        /// </summary>
        public static void BaseBuilderConnectionStrings()
        {
            OnlineConnectionString = SqlConnectionStringHelper.GetConnectionString(OnlineServerName, OnlineDatabaseName, OnlineUserName, OnlinePassword, null, true, OnlineConnectionTimeout);
            OfflineConnectionString = SqlConnectionStringHelper.GetConnectionString(OfflineServerName, OfflineDatabaseName, OfflineUserName, OfflinePassword, null, true, OfflineConnectionTimeout);
        }
    }
}
