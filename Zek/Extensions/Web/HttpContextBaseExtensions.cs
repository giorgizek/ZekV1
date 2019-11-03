using System;
using System.Web;
using System.Web.Routing;

namespace Zek.Extensions.Web
{
    public static class HttpContextBaseExtensions
    {
        public static bool IsLinux(this HttpContextBase instance)
        {
            var platform = (int)Environment.OSVersion.Platform;
            if (platform != 4)
            {
                return platform == 0x80;
            }
            return true;
        }

        public static bool IsMono(this HttpContextBase instance)
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        public static RequestContext RequestContext(this HttpContextBase instance)
        {
            return new RequestContext(instance, RouteTable.Routes.GetRouteData(instance) ?? new RouteData());
        }
    }
}
