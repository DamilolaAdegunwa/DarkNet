using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.DI
{
    /// <summary>
    /// 定义组件创建的生命周期
    /// </summary>
    public enum DependencyLife
    {
        /// <summary>
        /// 每次创建
        /// </summary>
        Transient = 0,
        /// <summary>
        /// 永远都创建一次
        /// </summary>
        Singleton = 1
    }
}
