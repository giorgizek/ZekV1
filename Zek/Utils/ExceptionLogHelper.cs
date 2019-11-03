using System;
using System.IO;
using System.Web;
using System.Windows.Forms;
using Zek.Extensions;

namespace Zek.Utils
{
    public class ExceptionLogHelper
    {
        public static void WriteConsole(Exception ex, string fileName = null)
        {
            ConsoleHelper.WriteException(ex);
            WriteWin(ex, fileName);
        }
        public static void Write(Exception ex, string fileName = null)
        {
            try
            {
                //bool isWebApp = System.Web.HttpRuntime.AppDomainId != null;
                if (HttpContext.Current == null)
                    WriteWin(ex, fileName);
                else
                    WriteWeb(ex, fileName);
            }
            catch { }
        }
       
        public static void WriteWin(Exception ex, string fileName = null)
        {
            try
            {
                var path = Path.Combine(Application.StartupPath, "Error", DateTime.Today.ToString("yyyy-MM-dd") + (!string.IsNullOrWhiteSpace(fileName) ? " " + fileName.Trim() : string.Empty) + ".txt");
                var isNew = CreateLogFile(path);

                using (var writer = File.AppendText(path))
                {
                    if (!isNew)
                    {
                        writer.WriteLine();
                        writer.WriteLine();
                    }

                    writer.WriteLine(ex.ToExceptionString());
                    writer.WriteLine("----------------------------");
                    //writer.Flush();
                    //writer.Close();
                }

            }
            catch { }
        }
        public static void WriteWeb(Exception ex, string fileName = null)
        {
            try
            {
                var path = System.Web.Hosting.HostingEnvironment.MapPath($"~/Error/{DateTime.Today:yyyy-MM-dd}{(!string.IsNullOrWhiteSpace(fileName) ? " " + fileName.Trim() : string.Empty)}.txt");
                //var path = HttpContext.Current.Server.MapPath($"~/Error/{DateTime.Today:yyyy-MM-dd}{(!string.IsNullOrWhiteSpace(fileName) ? " " + fileName.Trim() : string.Empty)}.txt");
                var isNew = CreateLogFile(path);

                using (var writer = File.AppendText(path))
                {
                    if (!isNew)
                        writer.WriteLine();

                    writer.WriteLine(ex.ToExceptionString());
                    writer.WriteLine("----------------------------");
                    //writer.Flush();
                    //writer.Close();
                }
            }
            catch { }
        }

        /// <summary>
        /// Creates empty file.
        /// </summary>
        /// <param name="path">Path of file.</param>
        /// <returns>Returns true if new file. Otherwise returns false.</returns>
        private static bool CreateLogFile(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(path))
            {
                File.Create(path).Close();
                return true;
            }

            return false;
        }
    }
}
