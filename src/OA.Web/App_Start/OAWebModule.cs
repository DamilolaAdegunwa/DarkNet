using Dark.Core.Modules;
using Dark.Web;
using OA.Application;
using OA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OA.EntityFramework;

namespace OA.Web
{
    [DependOn(typeof(MvcModule),
              typeof(OACoreModule), 
              typeof(OAApplicationModule),
              typeof(OAEntityFrameworkModule)
              )]
    public class OAWebModule : BaseModule
    {
        public override void Initialize()
        {
            IocManager.RegisterConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}