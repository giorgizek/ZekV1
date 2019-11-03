using System.Linq;
using System.Net;
using System.Web;
using Zek.Net;

namespace Zek.Extensions.Web
{
    public static class HttpRequestExtensions
    {
        public static string GetUserHostAddresses(this HttpRequestBase request)
        {
            var userHostAddress = HttpContext.Current.Request.UserHostAddress;
            // Attempt to parse.  If it fails, we catch below and return "0.0.0.0"
            // Could use TryParse instead, but I wanted to catch all exceptions
            IPAddress.Parse(userHostAddress);
            return userHostAddress.Add(",", request.ServerVariables["X_FORWARDED_FOR"]);
        }

        public static string TryGetUserHostAddresses(this HttpRequestBase request)
        {
            try
            {
                return GetUserHostAddresses(request);
            }
            catch
            {
                return "0.0.0.0";
            }
        }

        public static string TryGetUserPublicHostAddress(this HttpRequestBase request)
        {
            try
            {
                return GetUserPublicHostAddress(request);
            }
            catch
            {
                // Always return all zeroes for any failure (my calling code expects it)
                return "0.0.0.0";
            }
        }
        public static string GetUserPublicHostAddress(this HttpRequestBase request)
        {
            var userHostAddress = request.UserHostAddress;
            // Attempt to parse.  If it fails, we catch below and return "0.0.0.0"
            // Could use TryParse instead, but I wanted to catch all exceptions
            IPAddress.Parse(userHostAddress);

            var xForwardedFor = request.ServerVariables["X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(xForwardedFor))
                return userHostAddress;

            // Get a list of public ip addresses in the X_FORWARDED_FOR variable
            var publicForwardingIps = xForwardedFor.Split(',').Where(ip => !IPAddressHelper.IsPrivate(ip)).ToList();

            // If we found any, return the last one, otherwise return the user host address
            return publicForwardingIps.Any() ? publicForwardingIps.Last() : userHostAddress;

        }

        
    }
}
