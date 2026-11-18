using System;
using System.Runtime.Caching;

namespace Core.Common.Utility
{
    public class MemoryCacheHelper
    {
        private const int CacheHours = 10; 

        public static void Insert<T>(string key, T value, int? cacheMinute=null)
        {
            lock (MemoryCache.Default)
            {
                MemoryCache.Default.Remove(key);
                var expires = cacheMinute==null ? new DateTimeOffset(DateTime.Now.AddHours(CacheHours)) : new DateTimeOffset(DateTime.Now.AddHours(cacheMinute.Value));
                MemoryCache.Default.Add(key, value, expires);
            }
        }


        public static T Get<T>(string key)
        {
            if (MemoryCache.Default.Contains(key))
            {
                return (T)MemoryCache.Default.Get(key);
            }
            return default(T);
        }

        public static bool Contains(string key)
        {
            return MemoryCache.Default.Contains(key);
        }

        public static void Remove(string key)
        {
            MemoryCache.Default.Remove(key);
        }
    }
}