﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dark.Core.Serializer
{
    public class JsonHelper
    {
        #region Json
        /// <summary>
        /// JsonConvert.SerializeObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON<T>(T obj) where T : class
        {
            return JsonConvert.SerializeObject(obj);
        }

       
        /// <summary>
        /// JsonConvert.DeserializeObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ToObject<T>(string content) where T : class
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// 将JSON 字符串集合转换为list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJSON"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(string strJSON) where T : class
        {
            return JsonConvert.DeserializeObject<List<T>>(strJSON);
        }

        #endregion Json

        
    }
}
