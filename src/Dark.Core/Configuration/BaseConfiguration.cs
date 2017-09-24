using Dark.Core.Configuration.Startup;
using Dark.Core.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <summary>
        /// 对注入的服务进行替换
        /// </summary>
        /// <param name="type"></param>
        /// <param name="replaceAction"></param>
        void ReplaceService(Type type, Action replaceAction);
    }

    #endregion

    #region 2.0 具体实现类 BaseConfiguration

    public class BaseConfiguration : IBaseConfiguration, ISingletonDependency
    {
        /// <summary>
        /// 授权的配置属性
        /// </summary>
        public IAuthorizationConfiguration AuthConfig { get; set; }

        private IIocManager _iocManager;

        public string DefaultNameOrConnectionString { get; set; }


        public Dictionary<Type, Action> ServiceReplaceActions { get; private set; }

        public BaseConfiguration(IIocManager iocManager)
        {
            _iocManager = iocManager;
            ServiceReplaceActions = new Dictionary<Type, Action>();
        }

        public void Initialize()
        {
            AuthConfig = _iocManager.Resolve<IAuthorizationConfiguration>();
        }

        public void ReplaceService(Type type, Action replaceAction)
        {
            ServiceReplaceActions[type] = replaceAction;
        }

    }


    #endregion

}
