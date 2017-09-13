using Dark.Core.Application.Permission;
using Dark.Core.DI;
using Dark.Core.Domain.Uow;
using Dark.Core.Runtime.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Log;
using Castle.Core.Logging;

namespace Dark.Core.Application.Service
{
    public class AppService : IAppService, ITransientDependency
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 工作单元
        /// </summary>
        protected IUnitOfWork UnitWork { get; set; }

        /// <summary>
        /// 缓存管理
        /// </summary>
        protected ICacheManager CacheManager { get; set; }

        /// <summary>
        /// 权限检查
        /// </summary>
        public IPermissionManager PermissionManager { get; set; }

      


        /// <summary>
        /// 注册Logger
        /// </summary>
        public AppService()
        {
            //Logger = NullLogger.Instance;
        }
    }
}
