using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Modules
{
    public class ModuleInfo
    {
        /// <summary>
        /// 是否是启动模块
        /// </summary>
        public bool IsStartModule { get; }

        /// <summary>
        /// 模块程序集
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// 模块类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 模块的实例
        /// </summary>
        public BaseModule Instance { get; }

        /// <summary>
        /// 依赖该模块的集合
        /// </summary>
        public List<ModuleInfo> Dependencies { get; }


        /// <summary>
        /// Creates a new AbpModuleInfo object.
        /// </summary>
        public ModuleInfo(Type type, BaseModule instance)
        {
            Type = type;
            Instance = instance;
            Assembly = Type.GetTypeInfo().Assembly;
            Dependencies = new List<ModuleInfo>();
        }

        public override string ToString()
        {
            return Type.AssemblyQualifiedName ??
                   Type.FullName;
        }
    }
}
