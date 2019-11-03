using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Win32;

namespace Zek.Core
{
    public static class AboutTools
    {
        public static string NiceWindowsVersion(this OperatingSystem os)
        {
            switch (os.Platform)
            {
                case PlatformID.Win32Windows:
                    switch (os.Version.Minor)
                    {
                        case 0: return "Windows 95";
                        case 10:
                            if (os.Version.Revision.ToString(CultureInfo.InvariantCulture) == "2222A")
                                return "Windows 98 SE";
                            return "Windows 98";
                        case 90: return "Windows ME";
                    }
                    break;
                case PlatformID.Win32NT:
                    switch (os.Version.Major)
                    {
                        case 3: return "Windows NT 3.51";
                        case 4: return "Windows NT 4.0";
                        case 5:
                            switch (os.Version.Minor)
                            {
                                case 0: return "Windows 2000";
                                case 1: return "Windows XP";
                                case 2: return "Windows 2003 Server";
                            }
                            break;
                        case 6:
                            switch (os.Version.Minor)
                            {
                                case 0: return "Windows Vista / 2008 Server";
                                case 1: return "Windows 7";
                            }
                            break;
                    }
                    break;
            }

            return $"Unknown ({os.VersionString})";
        }

        public static DateTime CompilationTime(this Version v)
        {
            return new DateTime(v.Build * TimeSpan.TicksPerDay + v.Revision * TimeSpan.TicksPerSecond * 2).AddYears(1999).AddDays(-1);
        }

        public static List<NetFrameworkVersion> FrameworkVersions()
        {
            var versions = new List<NetFrameworkVersion>();
            var oldComponentsKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Active Setup\Installed Components\{78705f0d-e8db-4b2d-8193-982bdda15ecd}\") ??
                                   Registry.LocalMachine.OpenSubKey(@"HKLM\Software\Microsoft\Active Setup\Installed Components\{FDC11A6F-17D1-48f9-9EA3-9051954BAA24}\");
            if (oldComponentsKey != null)
                versions.AddRange(SubKeys(oldComponentsKey, (n, k) =>
                    new NetFrameworkVersion
                    {
                        GlobalVersion = "v1.0",
                        FullVersion = (string)k.GetValue("Version"),
                        ServicePack = GetOldComponentServicePack(k)
                    }).ToList());


            var componentsKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Net Framework Setup\NDP\");
            versions.AddRange(SubKeys(componentsKey, (n, k) =>
                new NetFrameworkVersion
                {
                    GlobalVersion = n,
                    FullVersion = (string)k.GetValue("Version"),
                    ServicePack = (int?)k.GetValue("SP")
                }).ToList());
            return versions;
        }

        private static int? GetOldComponentServicePack(RegistryKey k)
        {
            var e = ((string)k.GetValue("Version")).Split(',');
            int sp;
            if (int.TryParse(e.LastOrDefault(), out sp))
                return sp != 0 ? sp : (int?)null;
            return null;
        }

        private static IEnumerable<T> SubKeys<T>(RegistryKey key, Func<string, RegistryKey, T> func)
        {
            var keyNames = key.GetSubKeyNames();
            foreach (var keyName in keyNames)
            {
                using (var k = key.OpenSubKey(keyName))
                    yield return func(keyName, k);
            }
        }

        public class NetFrameworkVersion
        {
            public string GlobalVersion { get; internal set; }
            public string FullVersion { get; internal set; }
            public int? ServicePack { get; internal set; }

            public override string ToString()
            {
                return GlobalVersion + (ServicePack != null ? " SP" + ServicePack : "");
            }
        }
    }
}