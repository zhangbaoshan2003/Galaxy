using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL.Common
{
    public class CacheManager
    {
        MemoryCache _cache = MemoryCache.Default;
        static object _lock = new object();
        private static CacheManager _instance;
        public static CacheManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new CacheManager();
                    }
                }
                return _instance;
            }
        }

        private CacheManager()
        {

        }

        public object GetItem(string key)
        {
            object result = _cache[key];
            if (result != null)
                Debug.WriteLine("Hitted " + key);
            return result;
        }

        public object GetStringItem(string key)
        {
            object result = _cache[key];
            if (result != null)
                Debug.WriteLine("Hitted " + key);
            return result;
        }

        public void AddItem(object item, string key)
        {
            if (_cache[key] != null)
                return;

            lock (_lock)
            {
                if (_cache[key] != null)
                    return;

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddSeconds(60));
                _cache.Add(key, item, policy);
            }

        }

        public void AddItemByHour(object item, string key, double hours)
        {
            if (_cache[key] != null)
                return;

            lock (_lock)
            {
                if (_cache[key] != null)
                    return;

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddHours(hours));
                _cache.Add(key, item, policy);
            }
        }

        public void AddItemByMinute(object item, string key, int minutes)
        {
            if (_cache[key] != null)
                return;

            lock (_lock)
            {
                if (_cache[key] != null)
                    return;

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddHours(minutes));
                _cache.Add(key, item, policy);
            }
        }

        public void AddItemBySecond(object item, string key, int seconds)
        {
            if (_cache[key] != null)
                return;

            lock (_lock)
            {
                if (_cache[key] != null)
                    return;

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddSeconds(seconds));
                _cache.Add(key, item, policy);
            }
        }

        public void Dispose()
        {
            if (_cache != null)
                _cache.Dispose();
        }
    }
}
