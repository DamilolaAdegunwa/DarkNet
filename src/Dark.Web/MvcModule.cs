using Dark.Core;
using Dark.Core.Modules;
using Dark.Web.ControllerFactory;
using Dark.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dark.Web.Authorization;
using Dark.Core.Auditing;
using Dark.Web.Auditing;

namespace Dark.Web
{
    [DependOn(typeof(CoreModule))]
    public class MvcModule : BaseModule
    {
        public override void PreInitialize()
        {
            IocManager.AddRegisterConvention(new MvcRegisterConvention());
            //自定义
            IocManager.Register<IClientInfoProvider, WebClientInfoProvider>(Core.DI.DependencyLife.Transient);


        }

        public override void Initialize()
        {
            IocManager.RegisterConvention(Assembly.GetExecutingAssembly());
            var controllerFactory = new WindsorControllerFactory(IocManager);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }


        public override void PostInitialize()
        {
            GlobalFilters.Filters.Add(IocManager.Resolve<AuthorizeFilter>());
            GlobalFilters.Filters.Add(IocManager.Resolve<ExceptionFilter>());
            //GlobalFilters.Filters.Add(new )
        }
    }
}
