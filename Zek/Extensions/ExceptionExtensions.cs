using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Zek.Core;

namespace Zek.Extensions
{
    public static class ExceptionExtensions
    {
        [Obsolete("Use ToExceptionString", false)]
        public static string GetExceptionString(this Exception ex, string title = null, string customerExplanation = null, DateTime? date = null, bool throwException = false)
        {
            return ToExceptionString(ex, title, customerExplanation, date, throwException);
        }

        public static string ToExceptionString(this Exception ex, string title = null, string customerExplanation = null, DateTime? date = null, bool throwException = false)
        {
            try
            {
                DateTime? buildDate = null;
                try { buildDate = AssemblyHelper.LastWriteTime; }
                catch { }

                if (date == null)
                    date = DateTime.Now;

                var sb = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(customerExplanation))
                    sb.AppendLine("[Customer Explanation]")
                        .AppendLine(customerExplanation)
                        .AppendLine();


                sb.AppendLine("[General Info]")
                    .AppendLine("Date:       \t" + date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                    .AppendLine("Application:\t" + Application.ProductName)
                    .AppendLine("Framework:  \t" + RuntimeEnvironment.GetSystemVersion())
                    .AppendLine("Version:    \t" + (AssemblyHelper.AssemblyVersion != null ? AssemblyHelper.AssemblyVersion.ToString() : string.Empty) + " from " + (buildDate != null ? buildDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty))
                    .AppendLine("MachineName:\t" + Environment.MachineName)
                    .AppendLine("OSVersion:  \t" + Environment.OSVersion.VersionString)
                    .AppendLine("UserName:   \t" + Environment.UserName);

                if (HttpContext.Current != null)
                {
                    sb.AppendLine()
                        .AppendLine("[Web Info]")
                        .AppendLine("Form:       \t" + HttpContext.Current.Request.Form)
                        .AppendLine("QueryString:\t" + HttpContext.Current.Request.Url)
                        .AppendLine("Referer:    \t" + HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]);
                }

                sb.AppendLine()
                    .AppendLine("[Exception Info]");
                if (!string.IsNullOrWhiteSpace(title))
                    sb.AppendLine("Title:      \t" + title);

                AppendExceptionString(ex, sb, string.Empty);

                sb.AppendLine()
                    .AppendLine("[Assemblies]");
                if (AssemblyHelper.EntryAssembly != null)
                {
                    var referencedAssemblies = AssemblyHelper.EntryAssembly.GetReferencedAssemblies().OrderBy(x => x.Name);
                    foreach (var name in referencedAssemblies)
                    {
                        sb.AppendLine($"{name.Name}, Version = {name.Version}");
                    }
                }
                return sb.ToString();
            }
            catch
            {
                if (throwException)
                    throw;

                return null;
            }
        }
        public static string ToGeneralExceptionString(this Exception ex, string title = null, string customerExplanation = null, DateTime? date = null, bool throwException = false)
        {
            try
            {
                if (date == null)
                    date = DateTime.Now;

                var sb = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(customerExplanation))
                    sb.AppendLine("[Customer Explanation]")
                        .AppendLine(customerExplanation)
                        .AppendLine();


                sb.AppendLine("[General Info]")
                    .AppendLine("Date:       \t" + date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                    //.AppendLine("Application:\t" + Application.ProductName)
                    //.AppendLine("Framework:  \t" + RuntimeEnvironment.GetSystemVersion())
                    //.AppendLine("Version:    \t" + (AssemblyHelper.AssemblyVersion != null ? AssemblyHelper.AssemblyVersion.ToString() : string.Empty))
                    //.AppendLine("MachineName:\t" + Environment.MachineName)
                    //.AppendLine("OSVersion:  \t" + Environment.OSVersion.VersionString)
                    //.AppendLine("UserName:   \t" + Environment.UserName)
                    ;

                //if (HttpContext.Current != null)
                //{
                //    sb.AppendLine()
                //        .AppendLine("[Web Info]")
                //        .AppendLine("Form:       \t" + HttpContext.Current.Request.Form)
                //        .AppendLine("QueryString:\t" + HttpContext.Current.Request.Url)
                //        .AppendLine("Referer:    \t" + HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]);
                //}

                //sb.AppendLine()
                sb.AppendLine("[Exception Info]");
                if (!string.IsNullOrWhiteSpace(title))
                    sb.AppendLine("Title:      \t" + title);

                AppendExceptionString(ex, sb, string.Empty);

                //sb.AppendLine()
                //    .AppendLine("[Assemblies]");
                //var referencedAssemblies = AssemblyHelper.EntryAssembly.GetReferencedAssemblies().OrderBy(x => x.Name);
                //foreach (var name in referencedAssemblies)
                //{
                //    sb.AppendLine(string.Format("{0}, Version = {1}", name.Name, name.Version));
                //}
                return sb.ToString();
            }
            catch
            {
                if (throwException)
                    throw;

                return null;
            }
        }


        public static string ToWebExceptionString(this Exception ex, string title = null, string customerExplanation = null, DateTime? date = null, bool throwException = false)
        {
            return ToGeneralExceptionString(ex, title, customerExplanation, date, throwException);
        }
        private static void AppendExceptionString(Exception ex, StringBuilder sb, string indent = "")
        {
            if (indent == null)
                indent = string.Empty;
            else if (indent.Length > 0)
            {
                sb.AppendLine(indent + "[Inner Exception]");
                sb.AppendLine(indent + "Type: " + ex.GetType().FullName);
                sb.AppendLine(indent + "Message: " + ex.Message);
                sb.AppendLine(indent + "Source: " + ex.Source);
                sb.AppendLine(indent + "TargetSite: " + ex.TargetSite);
                sb.AppendLine(indent + "StackTrace: " + ex.StackTrace);
            }

            if (indent.Length == 0)
            {
                sb.AppendLine(indent + "Type:       \t" + ex.GetType().FullName);
                sb.AppendLine(indent + "Message:    \t" + ex.Message);
                sb.AppendLine(indent + "Source:     \t" + ex.Source);
                sb.AppendLine(indent + "TargetSite: \t" + ex.TargetSite);
                sb.AppendLine(indent + "StackTrace: \t" + ex.StackTrace);
            }

            if (ex.InnerException != null)
            {
                sb.AppendLine();
                AppendExceptionString(ex.InnerException, sb, indent + "  ");
            }
        }
    }
}
