using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace Dark.Core.Runtime.Cache
{
    /// <summary>
    /// 系统实现的的内存数据库,因为内存数据存放的数据不能超过4G
    /// </summary>
    public class RuntimeCache : ICacheManager
    {

        private ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        public object this[string key]
        {
            get
            {
                return Cache.Get(key);
            }

            set
            {
                Add(key, value);
            }
        }

        public void Add(string key, object data, int cacheTime = 30)
        {
            //1：检查是否存在
            if (data == null) return;
            //2：如果存在,那么时间增加,如果不存咋,那么久添加
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool Contains(string key)
        {
            return Cache.Contains(key);
        }
        /// <summary>
        /// 得到缓存的个数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return (int)Cache.GetCount();
        }

        public T Get<T>(string key) 
        {
            if (!Contains(key))
            {
                return default(T);
            }
            return (T)this[key];
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void RemoveAll()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }

        public void RemoveByPattern(string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) { return; }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            //循环
            foreach (var item in Cache)
                if (regex.IsMatch(item.Key))
                    Remove(item.Key);

        }

        public List<KeyValuePair<string, object>> GetCacheList()
        {
            List<KeyValuePair<string, object>> data = new List<KeyValuePair<string, object>>();
            foreach (var item in Cache)
                data.Add(item);
            return data;
        }


    }
}
