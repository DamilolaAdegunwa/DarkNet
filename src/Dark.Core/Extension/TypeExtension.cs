using Dark.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Extension
{
    public static class TypeExtension
    {
        public static List<Type> GetDependonTypes(this Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(DependOnAttribute), true).Cast<DependOnAttribute>();

            List<Type> types = new List<Type>();
            if (attributes == null || attributes.Count() == 0)
            {
                return types;
            }

            foreach (var attrItem in attributes)
            {
                foreach (var dependon in attrItem.DependModules)
                {
                    types.Add(dependon);
                }
            }

            return types;
        }

        /// <summary>
        /// 获取类型的程序集
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Assembly GetAssembly(this Type type)
        {
            return type.GetTypeInfo().Assembly;
        }
    }
}
