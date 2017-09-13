using Castle.Facilities.Logging;
using Dark.Web.ControllerFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCDemo
{
    public class MvcApplication : Dark.Web.BaseWebApplication<MvcDemoWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {

            Bootstrap.IocManager.IocContainer.AddFacility<LoggingFacility>(
              f => f.UseLog4Net().WithConfig("log4net.config")
            );

            //1：执行父类的程序启动方法
            base.Application_Start(sender, e);
        }
    }
}
