using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Xml;

namespace Zek.Web
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public class WebHelper
    {
        /// <summary>
        /// Gets server variable by name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Server variable</returns>
        public static string GetServerVariable(string name)
        {
            var result = string.Empty;
            try
            {
                if (HttpContext.Current.Request.ServerVariables[name] != null)
                {
                    result = HttpContext.Current.Request.ServerVariables[name];
                }
            }
            catch { }

            return result;
        }

        /// <summary>
        /// Disable browser cache
        /// </summary>
        public static void DisableBrowserCache()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Cache.SetExpires(new DateTime(1995, 5, 6, 12, 0, 0, DateTimeKind.Utc));
                HttpContext.Current.Response.Cache.SetNoStore();
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                HttpContext.Current.Response.Cache.AppendCacheExtension("post-check=0,pre-check=0");

            }
        }

        /// <summary>
        /// Gets a value indicating whether requested page is an admin page
        /// </summary>
        /// <returns>A value indicating whether requested page is an admin page</returns>
        public static bool IsAdmin()
        {
            var thisPageUrl = GetThisPageUrl(false);
            if (string.IsNullOrEmpty(thisPageUrl))
                return false;

            var adminUrl1 = GetWebAppLocation(false) + "admin";
            var adminUrl2 = GetWebAppLocation(true) + "admin";
            var flag1 = thisPageUrl.ToLowerInvariant().StartsWith(adminUrl1.ToLowerInvariant());
            var flag2 = thisPageUrl.ToLowerInvariant().StartsWith(adminUrl2.ToLowerInvariant());
            var isAdmin = flag1 || flag2;
            return isAdmin;
        }

        /// <summary>
        /// Gets a value indicating whether current connection is secured
        /// </summary>
        /// <returns>true - secured, false - not secured</returns>
        public static bool IsCurrentConnectionSecured()
        {
            var useSSL = false;
            if (HttpContext.Current != null)
            {
                useSSL = HttpContext.Current.Request.IsSecureConnection;
                //when your hosting uses a load balancer on their server then the Request.IsSecureConnection is never got set to true, use the statement below
                //just uncomment it
                //useSSL = HttpContext.Current.Request.ServerVariables["HTTP_CLUSTER_HTTPS"] == "on" ? true : false;
            }

            return useSSL;
        }
        
        /// <summary>
        /// Gets this page URL without query string.
        /// </summary>
        /// <returns></returns>
        public static string GetThisPageUrl()
        {
            return GetThisPageUrl(false);
        }

        /// <summary>
        /// Gets this page URL.
        /// </summary>
        /// <returns></returns>
        public static string GetThisPageUrl(bool includeQuerystring)
        {
            if (HttpContext.Current == null)
                return string.Empty;

            if (includeQuerystring)
            {
                var host = GetWebAppHost(IsCurrentConnectionSecured());
                if (host.EndsWith("/"))
                    host = host.Substring(0, host.Length - 1);
                return host + HttpContext.Current.Request.RawUrl;
            }
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
        }

        /// <summary>
        /// Gets Web App location
        /// </summary>
        /// <returns>Web App location</returns>
        public static string GetWebAppLocation()
        {
            var useSSL = IsCurrentConnectionSecured();
            return GetWebAppLocation(useSSL);
        }

        /// <summary>
        /// Gets Web App location
        /// </summary>
        /// <param name="useSSL">Use SSL</param>
        /// <returns>Web App location</returns>
        public static string GetWebAppLocation(bool useSSL)
        {
            var result = GetWebAppHost(useSSL);
            if (result.EndsWith("/"))
                result = result.Substring(0, result.Length - 1);
            result = result + HttpContext.Current.Request.ApplicationPath;
            if (!result.EndsWith("/"))
                result += "/";

            return result.ToLowerInvariant();
        }

        /// <summary>
        /// Gets Web App admin location
        /// </summary>
        /// <returns>Web App admin location</returns>
        public static string GetWebAppAdminLocation()
        {
            var useSSL = IsCurrentConnectionSecured();
            return GetWebAppAdminLocation(useSSL);
        }

        /// <summary>
        /// Gets Web App admin location
        /// </summary>
        /// <param name="useSSL">Use SSL</param>
        /// <returns>Web App admin location</returns>
        public static string GetWebAppAdminLocation(bool useSSL)
        {
            var result = GetWebAppLocation(useSSL);
            result += "Admin/";

            return result.ToLowerInvariant();
        }

        /// <summary>
        /// Gets Web App host location
        /// </summary>
        /// <param name="useSSL">Use SSL</param>
        /// <returns>Web App host location</returns>
        public static string GetWebAppHost(bool useSSL)
        {
            var result = "http://" + GetServerVariable("HTTP_HOST");
            if (!result.EndsWith("/"))
                result += "/";
            if (useSSL)
            {
                //shared SSL certificate URL
                var sharedSSLUrl = string.Empty;
                if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["SharedSSLUrl"]))
                    sharedSSLUrl = WebConfigurationManager.AppSettings["SharedSSLUrl"].Trim();

                result = !string.IsNullOrEmpty(sharedSSLUrl) ? sharedSSLUrl : result.Replace("http:/", "https:/");
            }
            else
            {
                if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["UseSSL"]) && Convert.ToBoolean(WebConfigurationManager.AppSettings["UseSSL"]))
                {
                    //SSL is enabled

                    //get shared SSL certificate URL
                    var sharedSslUrl = string.Empty;
                    if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["SharedSSLUrl"]))
                        sharedSslUrl = WebConfigurationManager.AppSettings["SharedSSLUrl"].Trim();
                    if (!string.IsNullOrEmpty(sharedSslUrl))
                    {
                        //shared SSL

                        /* we need to set a store URL here (IoC.Resolve<ISettingManager>().StoreUrl property)
                         * but we cannot reference Nop.BusinessLogic.dll assembly.
                         * So we are using one more app config settings - <add key="NonSharedSSLUrl" value="http://www.yourStore.com" />
                         */
                        var nonSharedSslUrl = string.Empty;
                        if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["NonSharedSSLUrl"]))
                            nonSharedSslUrl = WebConfigurationManager.AppSettings["NonSharedSSLUrl"].Trim();
                        if (string.IsNullOrEmpty(nonSharedSslUrl))
                            throw new Exception("NonSharedSSLUrl app config setting is not empty");
                        result = nonSharedSslUrl;
                    }
                }
            }

            if (!result.EndsWith("/"))
                result += "/";

            return result.ToLowerInvariant();
        }

        /// <summary>
        /// Reloads current page
        /// </summary>
        public static void ReloadCurrentPage()
        {
            var useSSL = IsCurrentConnectionSecured();
            ReloadCurrentPage(useSSL);
        }

        /// <summary>
        /// Reloads current page
        /// </summary>
        /// <param name="useSsl">Use SSL</param>
        public static void ReloadCurrentPage(bool useSsl)
        {
            var storeHost = GetWebAppHost(useSsl);
            if (storeHost.EndsWith("/"))
                storeHost = storeHost.Substring(0, storeHost.Length - 1);
            var url = storeHost + HttpContext.Current.Request.RawUrl;
            url = url.ToLowerInvariant();
            HttpContext.Current.Response.Redirect(url);
        }

        /// <summary>
        /// Ensures that requested page is secured (https://)
        /// </summary>
        public static void EnsureSsl()
        {
            if (!IsCurrentConnectionSecured())
            {
                var useSSL = false;
                if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["UseSSL"]))
                    useSSL = Convert.ToBoolean(WebConfigurationManager.AppSettings["UseSSL"]);
                if (useSSL)
                {
                    //if (!HttpContext.Current.Request.Url.IsLoopback)
                    //{
                    ReloadCurrentPage(true);
                    //}
                }
            }
        }

        /// <summary>
        /// Ensures that requested page is not secured (http://)
        /// </summary>
        public static void EnsureNonSsl()
        {
            if (IsCurrentConnectionSecured())
            {
                ReloadCurrentPage(false);
            }
        }


        /// <summary>
        /// Write XML to response
        /// </summary>
        /// <param name="xml">XML</param>
        /// <param name="fileName">Filename</param>
        public static void WriteResponseXml(string xml, string fileName)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                var document = new XmlDocument();
                document.LoadXml(xml);
                var decl = document.FirstChild as XmlDeclaration;
                if (decl != null)
                {
                    decl.Encoding = "utf-8";
                }
                var response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/xml";
                response.AddHeader("content-disposition", $"attachment; filename={fileName}");
                response.BinaryWrite(Encoding.UTF8.GetBytes(document.InnerXml));
                response.End();
            }
        }

        /// <summary>
        /// Write text to response
        /// </summary>
        /// <param name="txt">text</param>
        /// <param name="fileName">Filename</param>
        public static void WriteResponseTxt(string txt, string fileName)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                var response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/plain";
                response.AddHeader("content-disposition", $"attachment; filename={fileName}");
                response.BinaryWrite(Encoding.UTF8.GetBytes(txt));
                response.End();
            }
        }

        /// <summary>
        /// Write XLS file to response
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="targetFileName">Target file name</param>
        public static void WriteResponseXls(string filePath, string targetFileName)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/xls";
                response.AddHeader("content-disposition", $"attachment; filename={targetFileName}");
                response.BinaryWrite(File.ReadAllBytes(filePath));
                response.End();
            }
        }

        /// <summary>
        /// Write PDF file to response
        /// </summary>
        /// <param name="filePath">File napathme</param>
        /// <param name="targetFileName">Target file name</param>
        /// <remarks>For BeatyStore project</remarks>
        public static void WriteResponsePdf(string filePath, string targetFileName)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/pdf";
                response.AddHeader("content-disposition", $"attachment; filename={targetFileName}");
                response.BinaryWrite(File.ReadAllBytes(filePath));
                response.End();
            }
        }

        /// <summary>
        /// Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            var str = string.Empty;
            for (var i = 0; i < length; i++)
                str = string.Concat(str, random.Next(10).ToString(CultureInfo.InvariantCulture));
            return str;
        }


        /// <summary>
        /// Set response NoCache
        /// </summary>
        /// <param name="response">Response</param>
        public static void SetResponseNoCache(HttpResponse response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            //response.Cache.SetCacheability(HttpCacheability.NoCache) 

            response.CacheControl = "private";
            response.Expires = 0;
            response.AddHeader("pragma", "no-cache");
        }




        /// <summary>
        /// Get a value indicating whether content page is requested
        /// </summary>
        /// <returns>Result</returns>
        public static bool IsContentPageRequested()
        {
            var context = HttpContext.Current;
            var request = context.Request;

            if (!request.Url.LocalPath.ToLowerInvariant().EndsWith(".aspx") &&
                !request.Url.LocalPath.ToLowerInvariant().EndsWith(".asmx") &&
                !request.Url.LocalPath.ToLowerInvariant().EndsWith(".ashx"))
            {
                return false;
            }

            return true;
        }
    }
}
