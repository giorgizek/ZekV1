using System;
using System.Web;

namespace Zek.Extensions.Web
{
    public static class HttpRequestBaseExtensions
    {
        public static string ApplicationRoot(this HttpRequestBase instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            var str = instance.Url.GetLeftPart(UriPartial.Authority) + instance.ApplicationPath;
            if (str.EndsWith("/", StringComparison.Ordinal))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        public static bool CanCompress(this HttpRequestBase instance)
        {
            var str = (instance.Headers["Accept-Encoding"] ?? string.Empty).ToUpperInvariant();
            if ((instance.Browser.MajorVersion < 7) && instance.Browser.IsBrowser("IE"))
            {
                return false;
            }
            if (!str.Contains("GZIP"))
            {
                return str.Contains("DEFLATE");
            }
            return true;
        }
    }
}
