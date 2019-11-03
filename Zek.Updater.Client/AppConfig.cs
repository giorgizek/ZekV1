using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace Zek.Updater.Client
{
    public static class AppConfig
    {
        static AppConfig()
        {
            Load();
        }

        private static string _appFolderName = string.Empty;
        public static string AppFolderName
        {
            get { return _appFolderName; }
            set
            {
                if (value == null) value = string.Empty;
                _appFolderName = value;
            }
        }

        private static string _appExeName = string.Empty;
        public static string AppExeName
        {
            get { return _appExeName; }
            set
            {
                if (value == null) value = string.Empty;
                _appExeName = value;
            }
        }

        public static string AppExePath
        {
            get { return Path.Combine(AppExeFolder, AppExeName); }
        }
        public static string AppExeFolder
        {
            get { return Path.Combine(ExecutableDir, AppFolderName); }
        }
        public static string ExecutableDir
        {
            get { return Path.GetDirectoryName(Application.ExecutablePath); }
        }






        private static bool _useCurrencProxy = true;
        public static bool Proxy
        {
            get { return _useCurrencProxy; }
            set { _useCurrencProxy = value; }
        }

        #region Update
        private static string _updateUrl = string.Empty;
        public static string UpdateUrl
        {
            get { return _updateUrl; }
            set
            {
                if (value == null) value = string.Empty;
                _updateUrl = value;

                UpdateIsFile = Ext.IsFile(value);
                UpdateDirSeperator = UpdateIsFile ? '\\' : '/';
                UpdateUrlDir = value.Length > 0
                    ? value.Substring(0, value.LastIndexOf(UpdateDirSeperator))
                    : string.Empty;
            }
        }

        public static bool UpdateIsFile { get; private set; }
        public static char UpdateDirSeperator { get; private set; }
        public static string UpdateUrlDir { get; private set; }

        #endregion

        #region Updater
        private static string _updaterUrl = string.Empty;
        public static string UpdaterUrl
        {
            get { return _updaterUrl; }
            set
            {
                if (value == null) value = string.Empty;
                _updaterUrl = value;

                UpdaterIsFile = Ext.IsFile(value);
                UpdaterDirSeperator = UpdaterIsFile ? '\\' : '/';
                UpdaterUrlDir = value.Length > 0
                    ? value.Substring(0, value.LastIndexOf(UpdaterDirSeperator))
                    : string.Empty;
            }
        }
        public static bool UpdaterIsFile { get; private set; }
        public static char UpdaterDirSeperator { get; private set; }
        public static string UpdaterUrlDir { get; private set; }
        #endregion

        private static string _proxyUrl = string.Empty;
        public static string ProxyUrl
        {
            get { return _proxyUrl; }
            set
            {
                if (value == null) value = string.Empty;
                _proxyUrl = value;
            }
        }


        private static string _proxyUserName = string.Empty;
        public static string ProxyUserName
        {
            get { return _proxyUserName; }
            set
            {
                if (value == null) value = string.Empty;
                _proxyUserName = value;
            }
        }


        private static string _proxyPassword = string.Empty;
        public static string ProxyPassword
        {
            get { return _proxyPassword; }
            set
            {
                if (value == null) value = string.Empty;
                _proxyPassword = value;
            }
        }

        public static void Load()
        {
            AppFolderName = Ext.GetConfigString("AppFolderName");
            AppExeName = Ext.GetConfigString("AppExeName");

            UpdateUrl = Ext.GetConfigString("UpdateUrl");
            UpdaterUrl = Ext.GetConfigString("UpdaterUrl");

            Proxy = Ext.GetConfigBool("UseCurrentProxy");
            ProxyUrl = Ext.GetConfigString("ProxyUrl");
            ProxyUserName = Ext.GetConfigString("ProxyUserName");
            ProxyPassword = Ext.GetConfigString("ProxyPassword");
        }
        public static void Save()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            Ext.SetConfig(config, "AppFolderName", AppFolderName);
            Ext.SetConfig(config, "AppExeName", AppExeName);

            Ext.SetConfig(config, "UpdateUrl", UpdateUrl);
            Ext.SetConfig(config, "UpdaterUrl", UpdaterUrl);

            Ext.SetConfig(config, "Proxy", Proxy);
            Ext.SetConfig(config, "ProxyUrl", ProxyUrl);
            Ext.SetConfig(config, "ProxyUserName", ProxyUserName);
            Ext.SetConfig(config, "ProxyPassword", ProxyPassword);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
