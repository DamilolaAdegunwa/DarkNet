using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.DI
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIocManager : IRegister, IResolver
    {
        /// <summary>
        /// Windsor 容器
        /// </summary>
        IWindsorContainer IocContainer { get; }
        void Dispose();
    }
}
