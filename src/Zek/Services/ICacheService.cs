using System;
using Microsoft.Extensions.Caching.Memory;

namespace Zek.Services
{
    public interface ICacheService
    {
        /// <summary>
        /// Set data in cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <param name="duration">duration in seconds.</param>
        /// <param name="sliding">sliding in seconds (sliding means that if inactive get then it will be removed).</param>
        void Set(string key, object content, int duration = 60, int? sliding = null);

        /// <summary>
        /// Get cached data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T Get<T>(string key, T defaultValue = default(T));
    }

    public class MemoryCacheService : ICacheService
    {
        protected IMemoryCache Cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            Cache = cache;
        }


        public void Set(string key, object content, int durationInSeconds, int? slidingInSeconds = null) => Set(key, content, DateTimeOffset.Now + TimeSpan.FromSeconds(durationInSeconds), slidingInSeconds);
        public void Set(string key, object content, DateTimeOffset? absoluteExpiration = null, int? slidingInSeconds = null)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = absoluteExpiration ?? DateTime.Now + TimeSpan.FromSeconds(60),
                SlidingExpiration = slidingInSeconds != null ? TimeSpan.FromSeconds(slidingInSeconds.Value) : (TimeSpan?)null,
                Priority = CacheItemPriority.Low
            };

            Cache.Set(key, content, options);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public T Get<T>(string key, T defaultValue = default(T))
        {
            object result;
            if (Cache.TryGetValue(key, out result))
            {
                return (T)result;
            }

            return defaultValue;
        }
    }
}
