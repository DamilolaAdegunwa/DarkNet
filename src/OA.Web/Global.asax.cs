using Castle.Facilities.Logging;
using Dark.Core.Log;
using Dark.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OA.Web
{
    public class MvcApplication : BaseWebApplication<OAWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            //1：引入log4net
            Bootstrap.IocManager.IocContainer.AddFacility<LoggingFacility>(
                 f => f.UseDarkLog4Net().WithConfig("log4net.config")
               );
            //2：执行Dark.Web的application
            base.Application_Start(sender, e);
        }
    }
}
