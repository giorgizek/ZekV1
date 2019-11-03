using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;

namespace Zek.Core
{
    /// <summary>
    /// ასემბლის დამხმარე კლასი.
    /// </summary>
    public static class AssemblyHelper
    {
        public static Assembly EntryAssembly
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly();
                return assembly ?? WebEntryAssembly;
            }
        }

        private static Assembly WebEntryAssembly
        {
            get
            {
                if (HttpContext.Current == null ||
                    HttpContext.Current.ApplicationInstance == null)
                {
                    return null;
                }

                var type = HttpContext.Current.ApplicationInstance.GetType();
                while (type != null && type.Namespace == "ASP")
                {
                    type = type.BaseType;
                }

                return type == null ? null : type.Assembly;
            }
        }


        public static string AssemblyTitle
        {
            get
            {
                if (EntryAssembly == null) return string.Empty;

                var attributes = EntryAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != string.Empty)
                    {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(EntryAssembly.CodeBase);
            }
        }
        //public static string AssemblyVersion
        //{
        //    get
        //    {
        //        Version ver = EntryAssembly.GetName().Version;
        //        return string.Format("{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build);
        //    }
        //}
        public static string AssemblyDescription
        {
            get
            {
                var attributes = EntryAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }
        public static string AssemblyProduct
        {
            get
            {
                var attributes = EntryAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }
        public static string AssemblyCopyright
        {
            get
            {
                var attributes = EntryAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        public static string AssemblyCompany
        {
            get
            {
                var attributes = EntryAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                    return string.Empty;
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        public static string ApplicationVersion => "v" + AssemblyVersion.ToString(3);

        public static string AssemblyGuid
        {
            get
            {
                var attributes = EntryAssembly.GetCustomAttributes(typeof(GuidAttribute), false);
                if (attributes.Length == 0)
                    return string.Empty;
                return ((GuidAttribute)attributes[0]).Value;
            }
        }
        public static AssemblyName AssemblyName => EntryAssembly != null ? EntryAssembly.GetName() : null;

        public static Version AssemblyVersion => AssemblyName != null ? AssemblyName.Version : null;

        public static float AssemblyVersionToSingle => ToSingle(AssemblyVersion);

        public static DateTime BuildDate => new DateTime(2000, 1, 1).Add(new TimeSpan(
            TimeSpan.TicksPerDay * AssemblyVersion.Build + // days since 1 January 2000
            TimeSpan.TicksPerSecond * 2 * AssemblyVersion.Revision));

        public static DateTime LastWriteTime => File.GetLastWriteTime(EntryAssembly.Location);

        public static int Compare(AssemblyName x, AssemblyName y)
        {
            return String.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }

        public static float ToSingle(this Version version)
        {
            float major = version.Major;
            if (version.Minor <= 10)
            {
                return major + version.Minor / 10f;
            }

            return major + version.Minor / 100f;
        }



        public static bool IsRelease(Assembly assembly)
        {
            var attributes = assembly.GetCustomAttributes(typeof(DebuggableAttribute), true);
            if (attributes.Length == 0)
                return true;

            var d = (DebuggableAttribute)attributes[0];
            if ((d.DebuggingFlags & DebuggableAttribute.DebuggingModes.Default) == DebuggableAttribute.DebuggingModes.None)
                return true;

            return false;
        }

        public static bool IsDebug(Assembly assembly)
        {
            var attributes = assembly.GetCustomAttributes(typeof(DebuggableAttribute), true);
            if (attributes.Length == 0)
                return true;

            var d = (DebuggableAttribute)attributes[0];
            if (d.IsJITTrackingEnabled) return true;
            return false;
        }

        #region Is Mono/Linux
        /// <summary>
        /// Gets a value indicating whether we're running under Mono.
        /// </summary>
        /// <value><c>true</c> if Mono; otherwise, <c>false</c>.</value>
        public static bool IsMono => Type.GetType("Mono.Runtime") != null;

        /// <summary>
        /// Gets a value indicating whether we're running under Linux or a Unix variant.
        /// </summary>
        /// <value><c>true</c> if Linux/Unix; otherwise, <c>false</c>.</value>
        public static bool IsLinux
        {
            get
            {
                var p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 128);
            }
        }

        #endregion
    }


}
