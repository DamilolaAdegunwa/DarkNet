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
using Dark.Core.Authorization;
using Dark.Core.Runtime.Session;

namespace Dark.Core.Application.Service
{
    public class AppService :AppServiceBase, IAppService, ITransientDependency
    {
        public IBaseSession BaseSession { get; set; }

        /// <summary>
        /// 注册Logger
        /// </summary>
        public AppService()
        {
            BaseSession = NullSession.Instance;
        }
    }
}
