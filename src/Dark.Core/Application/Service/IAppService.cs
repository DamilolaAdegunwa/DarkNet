using Castle.Core.Logging;
using Dark.Core.Application.Permission;
using Dark.Core.DI;
using Dark.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Application.Service
{
    /// <summary>
    /// 提供公共的方法和属性和服务
    /// </summary>
    public interface IAppService
    {
        //1：日志记录
        ILogger Logger { get; set; }
        //2：unitwork
        //3：权限信息
        IPermissionManager PermissionManager { get; set; }
        //4：登陆人信息

    }
}
