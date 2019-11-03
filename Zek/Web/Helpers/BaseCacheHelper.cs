using System;
using System.Web;
using System.Web.Caching;
using Zek.Configuration;

namespace Zek.Web
{
    public class BaseCacheHelper
    {
        public static void Add(string key, object value)
        {
            Add(key, value, BaseWebConfig.CacheExpiration > 0 ? BaseWebConfig.CacheExpiration : 1440);
        }
        public static void Add(string key, object value, int cacheExpirationMinutes)
        {
            if (string.IsNullOrEmpty(key) || value == null) return;
            HttpContext.Current.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(cacheExpirationMinutes), Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="value">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public static void Add<T>(string key, T value)
        {
            Add(key, value, BaseWebConfig.CacheExpiration > 0 ? BaseWebConfig.CacheExpiration : 1440);
        }
        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="value">Item to be cached</param>
        /// <param name="key">Name of item</param>
        /// <param name="cacheExpirationMinutes"></param>
        public static void Add<T>(string key, T value, int cacheExpirationMinutes)
        {
            if (string.IsNullOrEmpty(key) || value == null) return;
            // NOTE: Apply expiration parameters as you see fit.
            // I typically pull from configuration file.

            // In this example, I want an absolute
            // timeout so changes will always be reflected
            // at that time. Hence, the NoSlidingExpiration.
            HttpContext.Current.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(cacheExpirationMinutes), Cache.NoSlidingExpiration);
        }
        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="value">Item to be cached</param>
        /// <param name="key">Name of item</param>
        /// <param name="absoluteExpiration"></param>
        public static void Add<T>(string key, T value, DateTime absoluteExpiration)
        {
            if (string.IsNullOrEmpty(key) || value == null) return;
            // NOTE: Apply expiration parameters as you see fit.
            // I typically pull from configuration file.

            // In this example, I want an absolute
            // timeout so changes will always be reflected
            // at that time. Hence, the NoSlidingExpiration.
            HttpContext.Current.Cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public static void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }
        
        /// <summary>
        /// ასუფთავებს მთლიან ქეშს.
        /// </summary>
        public static void Clear()
        {
            var enm = HttpContext.Current.Cache.GetEnumerator();
            while (enm.MoveNext())
                Remove((string)enm.Key);
        }


        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }

        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <returns>Cached item as type</returns>
        public static T Get<T>(string key) where T : class
        {
            try
            {
                return (T)HttpContext.Current.Cache[key];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <param name="value">Cached value. Default(T) if
        /// item doesn't exist.</param>
        /// <returns>Cached item as type</returns>
        public static bool Get<T>(string key, out T value)
        {
            try
            {
                if (!Exists(key))
                {
                    value = default(T);
                    return false;
                }

                value = (T)HttpContext.Current.Cache[key];
            }
            catch
            {
                value = default(T);
                return false;
            }

            return true;
        }
    }
}
