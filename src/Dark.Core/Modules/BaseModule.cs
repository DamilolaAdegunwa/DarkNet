using Castle.Core.Logging;
using Dark.Core.Configuration;
using Dark.Core.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Modules
{
    public abstract class BaseModule
    {
        /// <summary>
        /// 应用ioc container
        /// </summary>
        protected internal IIocManager IocManager { get;internal set; }

        /// <summary>
        /// 日志
        /// </summary>
        protected ILogger Logger { get; set; }

        protected internal IBaseConfiguration Configuration { get; internal set; }


        public BaseModule(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        protected BaseModule()
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 注册之前,通常来用于添加注册协议
        /// </summary>
        public virtual void PreInitialize()
        {

        }
        /// <summary>
        /// 通常被用来注册
        /// </summary>
        public virtual void Initialize()
        {

        }



        /// <summary>
        /// 最后使用,在这里可以安全的解析其他类,自定义方法
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// 释放Ioc容器
        /// </summary>
        public void ShutDown()
        {
            IocManager.Dispose();
        }
    }
}
