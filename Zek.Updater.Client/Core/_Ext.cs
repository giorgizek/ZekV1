using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Zek.Updater.Client;

namespace Zek
{
    public static partial class Ext
    {
        private static System.Threading.Mutex _mutex;
        public static bool IsAlreadyRunning()
        {
            var createdNew = false;

            var name = @"Global\";

            name += Application.ExecutablePath.Replace("\\", "/").Replace(":", "_");

            if (name.Length > 260)
                name = @"Global\" + MD5HexText(name);

            try
            {
                _mutex = new System.Threading.Mutex(true, name, out createdNew);
                return !createdNew;
            }
            finally
            {
                if (_mutex != null && createdNew)
                    _mutex.ReleaseMutex();
            }
        }

        private static  readonly object Lock = new object();
        
        private static string _exeMD5Hex;
        public static string ExeMD5Hex
        {
            get
            {
                if (_exeMD5Hex == null)
                {
                    lock (Lock)
                    {
                        if (_exeMD5Hex == null)
                        {
                            try
                            {
                                _exeMD5Hex = MD5HexFile(System.Reflection.Assembly.GetExecutingAssembly().Location);
                            }
                            catch
                            {
                                _exeMD5Hex = string.Empty;
                            }
                        }

                    }
                }

                return _exeMD5Hex;
            }
        }


        public static bool IsFile(string path)
        {
            return string.IsNullOrWhiteSpace(path)
                || (!path.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) && new Uri(path).IsFile);
        }
        public static string ReadUpdateUrlFile()
        {
            if (AppConfig.UpdateIsFile)
                return File.ReadAllText(AppConfig.UpdateUrl);

            var randUrl = AppConfig.UpdateUrl + (AppConfig.UpdateUrl.Contains("?") ? "&" : "?") + "rand=" + Guid.NewGuid();
            return DownloadString(randUrl, AppConfig.Proxy, AppConfig.ProxyUrl, AppConfig.ProxyUserName, AppConfig.ProxyPassword);
        }
        public static string ReadUpdaterUrlFile()
        {
            if (AppConfig.UpdaterIsFile)
                return File.ReadAllText(AppConfig.UpdaterUrl);

            var randUrl = AppConfig.UpdaterUrl + (AppConfig.UpdaterUrl.Contains("?") ? "&" : "?") + "rand=" + Guid.NewGuid();
            return DownloadString(randUrl, AppConfig.Proxy, AppConfig.ProxyUrl, AppConfig.ProxyUserName, AppConfig.ProxyPassword);
        }

        public static void CopyFile(string source, string dest, bool useCurrentProxy, string proxyAddress, string proxyUserName, string proxyPassword)
        {
            if (IsFile(source))
            {
                File.Copy(source, dest, true);
            }
            else
                DownloadFile(source, dest, useCurrentProxy, proxyAddress, proxyUserName, proxyPassword);
        }

        public static string GetNewVersionExeFileName()
        {
            return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "New" + Path.GetFileName(Application.ExecutablePath));
        }

        private static string DownloadString(string address, bool useCurrentProxy, string proxyAddress, string proxyUserName, string proxyPassword)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    if (useCurrentProxy)
                    {
                        if (!string.IsNullOrEmpty(proxyAddress))
                        {
                            // instanciate the webproxy class, passing the ip and port number of your proxy server
                            var webProxy = new WebProxy(proxyAddress, true);

                            // pass the webproxy the users proxy user name and password
                            if (!string.IsNullOrEmpty(proxyUserName))
                                webProxy.Credentials = new NetworkCredential(proxyUserName, proxyPassword);
                        }
                        else
                            webClient.Proxy = null;
                    }

                    return webClient.DownloadString(new Uri(address));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error while downloading string.\n{0}", ex.Message));
            }
        }
        private static void DownloadFile(string address, string fileName, bool useCurrentProxy, string proxyAddress, string proxyUserName, string proxyPassword)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    if (useCurrentProxy)
                    {
                        if (!string.IsNullOrEmpty(proxyAddress))
                        {
                            // instanciate the webproxy class, passing the ip and port number of your proxy server
                            var webProxy = new WebProxy(proxyAddress, true);

                            // pass the webproxy the users proxy user name and password
                            if (!string.IsNullOrEmpty(proxyUserName))
                                webProxy.Credentials = new NetworkCredential(proxyUserName, proxyPassword);
                        }
                        else
                            webClient.Proxy = null;
                    }

                    webClient.DownloadFile(new Uri(address), fileName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error while downloading file.\n{0}", ex.Message));
            }
        }
    }
}
