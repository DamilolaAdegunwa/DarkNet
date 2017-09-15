using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Dark.Core.Application.Service;
using Dark.Core.Configuration;
using Dark.Core.DI;
using Dark.Core.DI.Install;
using Dark.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core
{
    /// <summary>
    /// 模块启动类
    /// </summary>
    public class Bootstrap
    {
        public IIocManager IocManager { get; }


        private Type StartType { get; set; }

        private ILogger _logger;


        private IModuleManager _moduleManger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startModule"></param>
        public Bootstrap(Type startModuleType) : this(startModuleType, DI.IocManager.Instance)
        {

        }


        public Bootstrap(Type startType, IocManager iocManager)
        {
            this.StartType = startType;

            IocManager = iocManager;

            _logger = NullLogger.Instance;

            InitIntercept();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="TTStart"></typeparam>
        /// <returns></returns>
        public static Bootstrap Create<TStart>() where TStart : BaseModule
        {
            return new Bootstrap(typeof(TStart));
        }

        /// <summary>
        /// 初始化拦截器
        /// </summary>
        private void InitIntercept()
        {

        }
        /// <summary>
        /// 解析日志
        /// </summary>
        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(Bootstrap));
            }
        }
        /// <summary>
        /// 注册本身
        /// </summary>
        private void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<Bootstrap>())
            {
                IocManager.IocContainer.Register(
                    Component.For<Bootstrap>().Instance(this)
                    );
            }
        }


        //通过模块系统,加载及注入对应的程序集
        public void Start()
        {

            //1:注册日志
            ResolveLogger();
            try
            {
                //2:注册当前程序集自身
                RegisterBootstrapper();
                //3:注册核心组件
                IocManager.IocContainer.Install(new CoreInstaller());

                IocManager.Resolve<BaseConfiguration>().Initialize();

                _moduleManger = IocManager.Resolve<ModuleManager>();
                //4:初始化module
                _moduleManger.Initialize(StartType);

                //5:执行所有模块注册
                _moduleManger.StartModules();

            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString(), ex);
            }

            //3:加载module
            //4:执行module的 init 和 postinit 方法
        }

        public void Dispose()
        {
            //1：执行模块卸载动作
            _moduleManger?.Shutdown();
            //IocManager.Dispose();
        }
    }
}
