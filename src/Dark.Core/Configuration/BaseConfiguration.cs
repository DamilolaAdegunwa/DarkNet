using Dark.Core.Configuration.Startup;
using Dark.Core.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Domain.Uow;

namespace Dark.Core.Configuration
{
    #region 1.0 借口 IBaseConfiguration
    /// <summary> 
    /// 用于暴露给外部可配置的接口
    /// </summary>
    public interface IBaseConfiguration
    {
        IAuthorizationConfiguration AuthConfig { get; }

        string DefaultNameOrConnectionString { get; set; }

        IIocManager IocManager { get; set; }

        IUnitOfWorkDefaultOptions UnitOfWorkOpts { get; }
        /// <summary>
        /// 对注入的服务进行替换
        /// </summary>
        /// <param name="type"></param>
        /// <param name="replaceAction"></param>
        void ReplaceService(Type type, Action replaceAction);

        void ReplaceService<TInter, TImpl>(DependencyLife lifeStyle)
            where TInter : class
            where TImpl : TInter;
    }

    #endregion

    #region 2.0 具体实现类 BaseConfiguration

    public class BaseConfiguration : IBaseConfiguration, ISingletonDependency
    {
        /// <summary>
        /// 授权的配置属性
        /// </summary>
        public IAuthorizationConfiguration AuthConfig { get; set; }

        public IIocManager IocManager { get; set; }

        public string DefaultNameOrConnectionString { get; set; }

        /// <summary>
        /// Used to configure unit of work defaults.
        /// </summary>
        public IUnitOfWorkDefaultOptions UnitOfWorkOpts { get; private set; }


        public Dictionary<Type, Action> ServiceReplaceActions { get; private set; }

        public BaseConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
            ServiceReplaceActions = new Dictionary<Type, Action>();
        }

        public void Initialize()
        {
            AuthConfig = IocManager.Resolve<IAuthorizationConfiguration>();
            UnitOfWorkOpts = IocManager.Resolve<IUnitOfWorkDefaultOptions>();
        }

        public void ReplaceService(Type type, Action replaceAction)
        {
            ServiceReplaceActions[type] = replaceAction;
        }

        /// <summary>
        /// Used to replace a service type.
        /// </summary>
        /// <typeparam name="TType">Type of the service.</typeparam>
        /// <typeparam name="TImpl">Type of the implementation.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="lifeStyle">Life style.</param>
        public void ReplaceService<TInter, TImpl>(DependencyLife lifeStyle = DependencyLife.Singleton)
            where TInter : class
            where TImpl : TInter
        {
            ReplaceService(typeof(TInter), () =>
            {
                IocManager.Register<TInter, TImpl>(lifeStyle);
            });
        }

    }


    #endregion

}
