using System;
using System.Collections.Generic;
using System.Web;
using Zek.Extensions;

namespace Zek.Web
{
    public class CookieHelper
    {
        public static void Set(string name, string value, TimeSpan ts)
        {
            var cookie = new HttpCookie(name, value) {Expires = DateTime.Now.Add(ts)};
            //cookie.Value = HttpContext.Current.Server.UrlEncode(value);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static void Set(string name, string value, int? expirationDays)
        {
            var cookie = new HttpCookie(name, value);
            if (expirationDays.HasValue)
                cookie.Expires = DateTime.Now.AddDays(expirationDays.Value);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }


        
        public static bool Exists(string name)
        {
            var cookie = HttpContext.Current.Request.Cookies[name];
            return cookie != null;
        }
        public static Dictionary<string, string> GetAll()
        {
            var cookies = new Dictionary<string, string>();
            foreach (var key in HttpContext.Current.Request.Cookies.AllKeys)
            {
                cookies.Add(key, HttpContext.Current.Request.Cookies[key].Value);
            }
            return cookies;
        }

        public static void Clear()
        {
            var cookies = HttpContext.Current.Request.Cookies;
            foreach (HttpCookie cookie in cookies)
            {
                Delete(cookie.Name);
            }
        }
        public static void Delete(string name)
        {
            var cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-2);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }



        //public static string GetString(string cookieName, bool decode)
        //{
        //    if (HttpContext.Current.Request.Cookies[cookieName] == null)
        //    {
        //        return string.Empty;
        //    }

        //    try
        //    {
        //        string tmp = HttpContext.Current.Request.Cookies[cookieName].Value.ToString();
        //        if (decode)
        //            tmp = HttpContext.Current.Server.UrlDecode(tmp);
        //        return tmp;
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //}


        public static string GetString(string name)
        {
            var cookie = HttpContext.Current.Request.Cookies[name];
            return cookie != null ? cookie.Value : string.Empty;
        }
        public static bool GetBool(string name)
        {
            return GetString(name).ToBoolean();
        }
        public static int GetInt32(string name)
        {
            return GetString(name).ToInt32();
        }
        public static int GetInt16(string name)
        {
            return GetString(name).ToInt16();
        }
        public static int GetByte(string name)
        {
            return GetString(name).ToByte();
        }
        public static Guid? GetGuid(string name)
        {
            return GetString(name).ToGuid();
        }
    }
}
