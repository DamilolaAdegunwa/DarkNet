using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Utils
{
    public class ExcelTool
    {
        /// <summary>
        /// 将list集合转换为六
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static MemoryStream ToStream<T>(List<T> data, string sheetName = "数据") where T : new()
        {
            //1:通过T来找到标题信息
            //2:给Excel中的body赋值
            return null;
        }



        /// <summary>
        /// 将多个对象转换为多个sheet的Excel数据流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static MemoryStream ToMulSheetStream<T>(List<Dictionary<string, List<T>>> data) where T : new()
        {
            return null;
        }
    }
}
