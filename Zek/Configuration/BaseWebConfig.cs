using System;
using System.Collections.Specialized;

namespace Zek.Configuration
{
    public class BaseWebConfig : AppConfigHelper
    {
        public static string VersionID
        {
            get { return GetString("VersionID"); }
            set { Set("VersionID", value); }
        }
        public static string HomeTitle
        {
            get { return GetString("HomeTitle"); }
            set { Set("HomeTitle", value); }
        }
        public static string HttpHomeUrl
        {
            get { return GetString("HttpHomeUrl"); }
            set { Set("HttpHomeUrl", value); }
        }
        public static string Charset
        {
            get { return GetString("Charset"); }
            set { Set("Charset", value); }
        }
        public static string Description
        {
            get { return GetString("Description"); }
            set { Set("Description", value); }
        }
        public static string Keywords
        {
            get { return GetString("Keywords"); }
            set { Set("Keywords", value); }
        }
        public static string Copyright
        {
            get { return GetString("Copyright"); }
            set { Set("Copyright", value); }
        }

        public static bool AllowAltUrl
        {
            get { return GetBool("AllowAltUrl"); }
            set { Set("AllowAltUrl", value); }
        }
        public static bool SiteOffline
        {
            get { return GetBool("SiteOffline"); }
            set { Set("SiteOffline", value); }
        }

        public static string AdminMail
        {
            get { return GetString("AdminMail"); }
            set { Set("AdminMail", value); }
        }
        public static string AdminIP
        {
            get { return GetString("AdminIP"); }
            set { Set("AdminIP", value); }
        }

        public static int DefaultGroupID
        {
            get { return GetInt32("DefaultGroupID"); }
            set { Set("DefaultGroupID", value); }
        }

        public static string Salt
        {
            get { return GetString("Salt"); }
            set { Set("Salt", value); }
        }
        public static string Key
        {
            get { return GetString("Key"); }
            set { Set("Key", value); }
        }

        public static int CommandTimeOut
        {
            get { return GetInt32("CommandTimeOut"); }
            set { Set("CommandTimeOut", value); }
        }


        /// <summary>
        /// პრეფიქსი.
        /// </summary>
        public static string Prefix
        {
            get { return GetString("Prefix"); }
            set { Set("Prefix", value); }
        }


        /// <summary>
        /// ქეშის ვადის გასვლის წუთების რაოდენობა
        /// </summary>
        public static int CacheExpiration
        {
            get { return GetInt32("CacheExpiration"); }
            set { Set("CacheExpiration", value); }
        }
        /// <summary>
        /// Cookie-ს ვადის გასვლის წუთების რაოდენობა
        /// </summary>
        public static int CookieExpires
        {
            get { return GetInt32("CookieExpires"); }
            set { Set("CookieExpires", value); }
        }

        public static string TmpDirectory
        {
            get { return GetString("TmpDirectory"); }
            set { Set("TmpDirectory", value); }
        }

        public static int ThumbnailSize
        {
            get { return GetInt32("ThumbnailSize"); }
            set { Set("ThumbnailSize", value); }
        }
        public static int MaxImageSize
        {
            get { return GetInt32("MaxImageSize"); }
            set { Set("MaxImageSize", value); }
        }
        public static int JpegQuality
        {
            get { return GetInt32("JpegQuality"); }
            set { Set("JpegQuality", value); }
        }

        public static bool UseSSL
        {
            get { return GetBool("UseSSL"); }
            set { Set("UseSSL", value); }
        }
        public static string SharedSSLUrl
        {
            get { return GetString("SharedSSLUrl"); }
            set { Set("SharedSSLUrl", value); }
        }


        /// <summary>
        /// Gets boolean value from NameValue collection
        /// </summary>
        /// <param name="config">NameValue collection</param>
        /// <param name="valueName">Name</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Result</returns>
        public static bool GetBool(NameValueCollection config, string valueName, bool defaultValue)
        {
            bool result;
            var str1 = config[valueName];
            if (str1 == null)
                return defaultValue;
            if (!bool.TryParse(str1, out result))
                throw new Exception($"Value must be boolean {valueName}");
            return result;
        }

        /// <summary>
        /// Gets integer value from NameValue collection
        /// </summary>
        /// <param name="config">NameValue collection</param>
        /// <param name="valueName">Name</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="zeroAllowed">Zero allowed</param>
        /// <param name="maxValueAllowed">Max value allowed</param>
        /// <returns>Result</returns>
        public static int GetInt(NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
        {
            int result;
            var str1 = config[valueName];
            if (str1 == null)
                return defaultValue;
            if (!int.TryParse(str1, out result))
            {
                if (zeroAllowed)
                {
                    throw new Exception($"Value must be non negative integer {valueName}");
                }
                throw new Exception($"Value must be positive integer {valueName}");
            }
            if (zeroAllowed && (result < 0))
                throw new Exception($"Value must be non negative integer {valueName}");
            if (!zeroAllowed && (result <= 0))
                throw new Exception($"Value must be positive integer {valueName}");
            if ((maxValueAllowed > 0) && (result > maxValueAllowed))
                throw new Exception($"Value too big {valueName}");
            return result;
        }
    }
}
