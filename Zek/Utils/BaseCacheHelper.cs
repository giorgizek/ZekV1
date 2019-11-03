using System;
using System.Runtime.Caching;

namespace Zek.Utils
{
    public class BaseCacheHelper
    {
        private static MemoryCache _cache = new MemoryCache("MemoryCache");
        public static void Set(string key, object value, int cacheExpirationMinutes = 1440)
        {
            if (string.IsNullOrEmpty(key) || value == null) return;

            InternalSet(key, value, DateTime.Now.AddMinutes(cacheExpirationMinutes));
        }
        public static void Set(string key, object value, DateTime absoluteExpiration)
        {
            if (string.IsNullOrEmpty(key) || value == null) return;

            InternalSet(key, value, absoluteExpiration);
        }
        private static void InternalSet(string key, object value, DateTime absoluteExpiration)
        {
            _cache.Set(key, value, absoluteExpiration);
        }


        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        /// ასუფთავებს მთლიან ქეშს.
        /// </summary>
        public static void Clear()
        {
            var oldCache = _cache;
            _cache = new MemoryCache("MemoryCache");
            oldCache.Dispose();

            //var allKeys = _cache.Select(o => o.Key);
            //Parallel.ForEach(allKeys, key => _cache.Remove(key));
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return _cache[key] != null;
        }

        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <returns>Cached item as type</returns>
        public static T Get<T>(string key)
        {
            return (T)_cache[key];
        }
       
        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <param name="value">Cached value. Default(T) if
        /// item doesn't exist.</param>
        /// <returns>Cached item as type</returns>
        public static bool TryGet<T>(string key, out T value)
        {
            try
            {
                if (!Exists(key))
                {
                    value = default(T);
                    return false;
                }

                value = (T)_cache[key];
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
