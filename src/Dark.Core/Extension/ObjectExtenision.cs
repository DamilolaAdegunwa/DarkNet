using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Extension
{
    /// <summary>
    /// 将字典集合转换为类型
    /// </summary>
    public static class ObjectExtenision
    {
        public static T DictToObj<T>(this T obj, Dictionary<string, string> dict)
        {
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (var item in props)
            {
                if (dict.ContainsKey(item.Name))
                {
                    item.SetValue(obj, ObjValueToPropType(dict[item.Name], item.PropertyType), null);
                }
            }
            return obj;
        }

        /// <summary>
        /// 将对象转换为对应类型的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="convertsionType"></param>
        /// <returns></returns>
        public static object ObjValueToPropType(object value, Type convertsionType)
        {
            //判断convertsionType类型是否为泛型，因为nullable是泛型类,
            if (convertsionType.IsGenericType &&
                //判断convertsionType是否为nullable泛型类
                convertsionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value.ToString().Length == 0)
                {
                    return null;
                }

                //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(convertsionType);
                //将convertsionType转换为nullable对的基础基元类型
                convertsionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, convertsionType);
        }
    }
}
