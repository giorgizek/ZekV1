using System;
using System.Text;
using System.Web;
using Zek.Configuration;
using Zek.Core;
using Zek.Security;

namespace Zek.Web
{
    public class HttpHelper
    {
        /// <summary>
        /// ამოწმებს მიმდინარე request უნდათ თუ არა XSS ინჯექშენების გაკეთება ანუ საიტის გატეხვა.
        /// </summary>
        /// <returns>აბრუნებს true-ს როცა საიტის გატეხვა უნდა, სხვა შემთხვევაში false.</returns>
        public static bool IsXss()
        {
            return IsXss(HttpContext.Current.Request);
        }
        /// <summary>
        /// ამოწმებს request უნდათ თუ არა XSS ინჯექშენების გაკეთება ანუ საიტის გატეხვა.
        /// </summary>
        /// <param name="request">რექუესტი, რომლის შემოწმებაც გვინდა.</param>
        /// <returns>აბრუნებს true-ს როცა საიტის გატეხვა უნდა, სხვა შემთხვევაში false.</returns>
        public static bool IsXss(HttpRequest request)
        {
            var url = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(request.QueryString.ToString()));

            if (url.Length > 0)
            {
                if (url.Contains("<") || url.Contains(">") || url.Contains("\"") || url.Contains("./") || url.Contains("../") || url.Contains("'") || url.Contains(".aspx"))
                {
                    if (request.QueryString["do"] == null || request.QueryString["do"] != "search")
                        return true;
                    if (request.QueryString["subaction"] == null || request.QueryString["subaction"] != "search")
                        return true;
                }
            }

            url = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(request.RawUrl));

            if (url.Length > 0)
            {
                if (url.Contains("<") || url.Contains(">") || url.Contains("\"") || url.Contains("./") || url.Contains("'"))
                {
                    if (request.QueryString["do"] == null || request.QueryString["do"] != "search")
                        return true;
                    if (request.QueryString["subaction"] == null || request.QueryString["subaction"] != "search")
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// უკეთებს ლინკს კოდირებას.
        /// </summary>
        /// <param name="url">ლინკი რომლის კოდირებაც გვინდა.</param>
        /// <returns>აბრუნებს დაკოდირებულ ლინკს.</returns>
        public static string UrlEncode(string url)
        {
            var encoded = string.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                encoded = HttpUtility.UrlEncode(url);

                //UrlEncode encodes spaces to pluses. http://forums.asp.net/t/1231078.aspx
                encoded = encoded.Replace("+", "%20");

                //Encode ' so the href specification does not terminate
                encoded = encoded.Replace("'", "%27");
            }
            return encoded;
        }

        /// <summary>
        /// უკეთებს კოდირებას არგუმენტებს.
        /// გამოიყენება რამოდენიმე მნიშვნელობის პარამეტრად გადასაცემად (მაგ: საიტზე რეგისტრაციის დროს გადავცემთ userName, passwordHash1, email).
        /// </summary>
        /// <param name="args">არგუმენტების მასივი, რომლის კოდირებაც გვინდა.</param>
        /// <returns>აბრუნებს დაკოდირებულ string-ს.</returns>
        public static string IDLinkEncode(params object[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            var idLink = string.Empty;
            for (var i = 0; i < args.Length; i++)
			{
                idLink += (i > 0 ? "||" : string.Empty) + (args[i] != null ? args[i].ToString() : string.Empty);
			}

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(idLink));
            //return HttpUtility.UrlEncode(Convert.ToBase64String(Encoding.UTF8.GetBytes(idLink)));
        }
        /// <summary>
        /// დეკოდირებას უკეთებს ლინკს.
        /// </summary>
        /// <param name="id">ლინკი რომლის დეკოდირებაც გვინდა.</param>
        /// <returns>აბრუნებს string[] მასივს.</returns>
        public static string[] IDLinkDecode(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return Encoding.UTF8.GetString(Convert.FromBase64String(id)).Split(new[] { "||" }, StringSplitOptions.None);
            //return Encoding.UTF8.GetString(Convert.FromBase64String(HttpUtility.UrlDecode(id))).Split(new string[] { "||" }, StringSplitOptions.None);
        }


        /// <summary>
        /// აჰეშირებს ინფორმაციას (HttpHomeUrl, Key, Salt-ის კონფიგურაციის გამოყენებით).
        /// </summary>
        /// <param name="value">პარამეტრების მასივი, რომლის დაჰეშირებაც გვინდა</param>
        /// <returns>აბრუნებს ჰეშ კოდს</returns>
        public static string HashData(params string[] value)
        {
            return HashData(false, value);
        }
        /// <summary>
        /// აჰეშირებს ინფორმაციას (IP-ს და HttpHomeUrl, Key, Salt-ის კონფიგურაციის გამოყენებით).
        /// </summary>
        /// <param name="ip">თუ გვინდა რომ ჰეშირების დროს გამოიყენოს IP მისამართიც მაშინ true. სხვა შემთხვევაში false.</param>
        /// <param name="args">პარამეტრების მასივი, რომლის დაჰეშირებაც გვინდა.</param>
        /// <returns>აბრუნებს ჰეშ კოდს.</returns>
        public static string HashData(bool ip, params string[] args)
        {
            return CryptoHelper.MD5Hex(
                                    CryptoHelper.MD5Hex(StringHelper.Join("||", ip ? HttpContext.Current.Request.UserHostAddress : string.Empty,
                                                                                BaseWebConfig.HttpHomeUrl,
                                                                                BaseWebConfig.Key,
                                                                                StringHelper.Join("||", args)),
                                                        BaseWebConfig.Salt),
                                    BaseWebConfig.Salt);
        }


        /*/// <summary>
        /// The regex mobile.
        /// </summary>
        private static readonly Regex RegexMobile = new Regex(ConfigurationManager.AppSettings.Get("BlogEngine.MobileDevices"), RegexOptions.IgnoreCase | RegexOptions.Compiled);
        /// <summary>
        ///     Gets a value indicating whether the client is a mobile device.
        /// </summary>
        /// <value><c>true</c> if this instance is mobile; otherwise, <c>false</c>.</value>
        public static bool IsMobile
        {
            get
            {
                var context = HttpContext.Current;
                if (context != null)
                {
                    var request = context.Request;
                    if (request.Browser.IsMobileDevice)
                    {
                        return true;
                    }

                    if (!string.IsNullOrEmpty(request.UserAgent) && RegexMobile.IsMatch(request.UserAgent))
                    {
                        return true;
                    }
                }

                return false;
            }
        }*/


        /*/// <summary>
        /// The relative web root.
        /// </summary>
        private static string _relativeWebRoot;
        /// <summary>
        /// Gets the relative root of the website.
        /// A string that ends with a '/'.
        /// </summary>
        public static string RelativeWebRoot
        {
            get
            {
                return _relativeWebRoot ?? (_relativeWebRoot = VirtualPathUtility.ToAbsolute(ConfigurationManager.AppSettings["Zek.VirtualPath"]));
            }
        }
        /// <summary>
        /// Gets the absolute root of the website.
        /// </summary>
        /// <value>A string that ends with a '/'.</value>
        public static Uri AbsoluteWebRoot
        {
            get
            {
                var context = HttpContext.Current;
                if (context == null)
                    throw new WebException("The current HttpContext is null");

                Uri absoluteurl = (Uri)context.Items["absoluteurl"];
                if (absoluteurl == null)
                {
                    absoluteurl = new Uri(context.Request.Url.GetLeftPart(UriPartial.Authority) + RelativeWebRoot);
                    context.Items["absoluteurl"] = absoluteurl;
                }

                return absoluteurl;

            }
        }
        */

        public static string GetBaseURL()
        {
            var baseUrl = HttpContext.Current != null ? HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + '/' : string.Empty;
            return baseUrl.EndsWith("/") ? baseUrl.Remove(baseUrl.Length - 1) : baseUrl;
        }

        public static string GetCurrentPageUrl()
        {
            return GetCurrentPageUrl(GetBaseURL());
        }
        public static string GetCurrentPageUrl(string baseURL)
        {
            return baseURL + HttpContext.Current.Request.Path;//.TrimStart('/');
        }

        public static string GetCurrentPageAbsoluteDir()
        {
            return GetAbsoluteDir(HttpContext.Current.Request.Url.AbsoluteUri);
        }
        public static string GetAbsoluteDir(string absoluteUri)
        {
            return absoluteUri.Remove(absoluteUri.LastIndexOf('/'));
        }
        
        public static string GetCurrentPageLocalDir()
        {
            return GetLocalDir(HttpContext.Current.Request.Url.LocalPath);
        }
        public static string GetLocalDir(string localPath)
        {
            return localPath.Remove(localPath.  LastIndexOf('/'));
        }

        public static string GetThemePath(string theme)
        {
            return theme != null ? "App_Themes/" + theme : string.Empty;
        }
        /// <summary>
        /// იღებს თემის ლინკს.
        /// </summary>
        /// <param name="theme">თემის დასახელება</param>
        /// <returns>აბრუნებს თემის ლინკს.</returns>
        public static string GetThemeUrl(string theme)
        {
            return GetThemeUrl(GetBaseURL(), theme);
        }
        /// <summary>
        /// იღებს თემის ლინკს.
        /// </summary>
        /// <param name="baseURL">საიტის მთავარი გვერდის ლინკი.</param>
        /// <param name="theme">თემის დასახელება</param>
        /// <returns>აბრუნებს თემის ლინკს.</returns>
        public static string GetThemeUrl(string baseURL, string theme)
        {
            return !string.IsNullOrEmpty(theme) ? baseURL + "/" + GetThemePath(theme) : baseURL;
        }

        /// <summary>
        /// Retrieves the subdomain from the specified URL.
        /// </summary>
        /// <param name="url">The URL from which to retrieve the subdomain.</param>
        /// <returns>The subdomain if it exist, otherwise null.</returns>
        public static string GetSubDomain(Uri url)
        {
            if (url.HostNameType == UriHostNameType.Dns)
            {
                var host = url.Host;
                if (host.Split('.').Length <= 2) return null;

                var lastIndex = host.LastIndexOf(".", StringComparison.Ordinal);
                var index = host.LastIndexOf(".", lastIndex - 1, StringComparison.Ordinal);
                return host.Substring(0, index);
            }

            return null;
        }
    }
}
