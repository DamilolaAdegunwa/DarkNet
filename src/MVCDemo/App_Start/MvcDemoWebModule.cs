using Dark.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCDemo
{
    [DependOn(typeof(Dark.Web.MvcModule))]
    public class MvcDemoWebModule : BaseModule
    {

        

        public override void Initialize()
        {
            IocManager.RegisterConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void PostInitialize()
        {

        }
    }
}