using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Runtime.Cache
{
    public interface ICacheManager
    {
        T Get<T>(string key);

        void Add(string key,object data,int cacheTime=30);

        bool Contains(string key);

        void Remove(string key);

        void RemoveAll();

        void RemoveByPattern(string pattern);
        

        object this[string key] { get; set; }

        int Count();
        /// <summary>
        /// 查询所有缓存数据
        /// </summary>
        List<KeyValuePair<string, object>> GetCacheList();
    }
}
