using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Dark.Core.Authorization;
using Dark.Core.Domain.Uow;

namespace Dark.Core
{
    /// <summary>
    /// 领域服务和应用服务的公共类
    /// </summary>
    public class AppServiceBase
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 工作单元
        /// </summary>
        protected IUnitOfWorkManager UnitWorkManager { get; set; }


        /// <summary>
        /// 权限检查
        /// </summary>
        public IPermissionManager PermissionManager { get; set; }



        public IPermissionChecker PermissionChecker { get; set; }

        public AppServiceBase()
        {
            Logger = NullLogger.Instance;
        }
    }
}
