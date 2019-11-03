using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Zek.Updater.Client
{
    static class Program
    {
        #region Fields
        private static XmlUpdate _xmlUpdate;
        private static bool _debug;
        #endregion

        [STAThread]
        static void Main(string[] args)
        {
            //File.Copy(Application.ExecutablePath, @"\\192.168.0.97\c$\TBC\Updater\Files\Zaa344f7a3b53a790219766f291fdfb93.zip", true);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UpdateForm frm = null;
            try
            {
                if (Ext.IsAlreadyRunning())
                    throw new Exception("You can only run one copy of this application at a time.");

                if (string.IsNullOrWhiteSpace(AppConfig.AppExeName))
                {
                    AppConfig.Save();
                    
                    throw new Exception("Error: Error in configuration file (AppExeName is null or whitespace)." + Environment.NewLine + Environment.NewLine
                        + "EXE: " + Application.ExecutablePath + Environment.NewLine
                        + "Args: " + ((args != null && args.Length > 0) ? string.Join(" ", args) : string.Empty));
                }
                if (File.Exists(AppConfig.AppExePath) && IsRunningExeApp())
                {
                    throw new Exception("Application is running. You must close the application before proceeding.\n" + AppConfig.AppExePath);
                }

                if (args.Contains("debug", StringComparer.InvariantCultureIgnoreCase))
                    _debug = true;

                if (CheckForNewUpdater())
                {
                    frm = new UpdateForm(_xmlUpdate, true);
                    Application.Run(frm);
                    frm = null;

                    var tmpExeFileName = Ext.GetNewVersionExeFileName();
                    var info = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = string.Format("/C ping 1.1.1.1 -n 1 -w 3000 > nul & move /Y \"{0}\" \"{1}\" ", tmpExeFileName, Application.ExecutablePath),
                        //Arguments = "/C ping 1.1.1.1 -n 1 -w 3000 > nul & del \"" + Application.ExecutablePath + "\"",
                        //Arguments = "/C choice /C Y /N /D Y /T 3 & del " + Application.ExecutablePath,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    Process.Start(info).Dispose();
                    Environment.Exit(0);
                }

                if (CheckForUpdates())
                {
                    frm = new UpdateForm(_xmlUpdate, false);
                    Application.Run(frm);
                }
            }
            catch (Exception ex)
            {
                HandleTerminalError(ex);
            }


            if (frm != null && frm.DialogResult != DialogResult.OK)
            {
                return;
            }

            if (_xmlUpdate != null && _xmlUpdate.AppExeName != AppConfig.AppExeName)
            {
                AppConfig.AppExeName = _xmlUpdate.AppExeName;
                AppConfig.Save();
            }

            if (!File.Exists(AppConfig.AppExePath))
            {
                MessageBox.Show(string.Format("AppExeName file not found ({0}).", AppConfig.AppExePath), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsRunningExeApp())
                StartAppProcess(args);
        }




        /// <summary>
        /// Handle error
        /// </summary>
        /// <param name="ex">Exception</param>
        private static void HandleTerminalError(Exception ex)
        {
            MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Checks if new update is available.
        /// </summary>
        /// <returns>returns true if new new update is available.</returns>
        private static bool CheckForUpdates()
        {
            if (string.IsNullOrWhiteSpace(AppConfig.UpdateUrl))
                return false;

            var errorText = string.Empty;
            try
            {
                //var randUrl = AppConfig.UpdateUrl + (AppConfig.UpdateUrl.Contains('?') ? "&" : "?") + "rand=" + Guid.NewGuid().ToString();
                //var xml = Ext.ReadFile() Ext.DownloadString(randUrl, AppConfig.Proxy, AppConfig.ProxyUrl, AppConfig.ProxyUserName, AppConfig.ProxyPassword);
                var xml = Ext.ReadUpdateUrlFile();
                if (_debug)
                    ShowDebugForm(AppConfig.UpdateUrl, xml);

                errorText = "Error in xml file structure which was downloaded from server.\n";
                _xmlUpdate = Ext.DeserializeXml<XmlUpdate>(xml);

                foreach (var file in _xmlUpdate.Files)
                {
                    var tmpFile = Path.Combine(AppConfig.AppExeFolder, file.File);
                    if (!File.Exists(tmpFile))
                        return true;

                    var hash = Ext.MD5HexFile(tmpFile);
                    if (file.Hash != hash)
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0}{1}\nUrl: {2}\n", errorText, ex.Message, AppConfig.UpdateUrl), ex);
            }

            return false;
        }

        private static void ShowDebugForm(string caption, string text)
        {
            //MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            new DebugForm(text) { Text = caption }.ShowDialog();
        }

        /// <summary>
        /// Checks if new update is available.
        /// </summary>
        /// <returns>returns true if new new update is available.</returns>
        private static bool CheckForNewUpdater()
        {
            if (string.IsNullOrWhiteSpace(AppConfig.UpdaterUrl) || string.IsNullOrWhiteSpace(Ext.ExeMD5Hex))
                return false;
            var errorText = string.Empty;
            try
            {
                var xml = Ext.ReadUpdaterUrlFile();
                if (_debug)
                    ShowDebugForm(AppConfig.UpdaterUrl, xml);

                errorText = "Error in xml file structure which was downloaded from server.\n";
                _xmlUpdate = Ext.DeserializeXml<XmlUpdate>(xml);

                var updaterFile = _xmlUpdate.Files.FirstOrDefault(x => x.File == _xmlUpdate.AppExeName);
                return (updaterFile != null && updaterFile.Hash != Ext.ExeMD5Hex);


                //foreach (var file in XmlUpdate.Files)
                //{
                //    var tmpFile = Path.Combine(AppConfig.AppExeFolder, file.File);
                //    if (!File.Exists(tmpFile))
                //        return true;

                //    var hash = Ext.MD5HexFile(tmpFile);
                //    if (file.Hash != hash)
                //        return true;
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0}{1}\nUrl: {2}\n", errorText, ex.Message, AppConfig.UpdaterUrl), ex);
            }
        }

        /// <summary>
        /// Checks if exe file is running.
        /// </summary>
        /// <returns>Returns true if it's running.</returns>
        private static bool IsRunningExeApp()
        {
            string appExePath = AppConfig.AppExePath;

            var processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                try
                {
                    if (appExePath == process.MainModule.FileName)
                    {
                        return true;
                    }
                }
                catch { }
            }


            return false;
        }

        /// <summary>
        /// Starts exe file.
        /// </summary>
        private static void StartAppProcess(string[] args)
        {
            try
            {
                args = args.Where(x => !x.StartsWith("update=", StringComparison.InvariantCultureIgnoreCase) && !x.StartsWith("delete=", StringComparison.InvariantCultureIgnoreCase)).ToArray();
                var processInfo = new ProcessStartInfo
                {
                    FileName = AppConfig.AppExePath,
                    Arguments = (args.Length > 0) ? string.Join(" ", args) : string.Empty,
                    WorkingDirectory = Path.GetDirectoryName(AppConfig.AppExePath)
                };
                var process = Process.Start(processInfo);
                if (process != null)
                    process.WaitForExit();
            }
            catch (Exception ex)
            {
                HandleTerminalError(ex);
            }
        }
    }
}
