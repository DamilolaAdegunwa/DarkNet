using Dark.Core;
using Dark.Core.Modules;
using Dark.Web.ControllerFactory;
using Dark.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dark.Web.Authorization;
using Dark.Core.Auditing;
using Dark.Web.Auditing;
using Dark.Core.Runtime.Session;
using Dark.Web.Runtime.Session;

namespace Dark.Web
{
    [DependOn(typeof(CoreModule))]
    public class MvcModule : BaseModule
    {
        public override void PreInitialize()
        {
            //1.定义mvc controller 实现的规则
            IocManager.AddRegisterConvention(new MvcRegisterConvention());
            //自定义
            IocManager.Register<IClientInfoProvider, WebClientInfoProvider>(Core.DI.DependencyLife.Transient);

            Configuration.ReplaceService<IPrincipalAccessor, HttpContextPrincipalAccessor>(Core.DI.DependencyLife.Transient);

            

        }

        public override void Initialize()
        {
            IocManager.RegisterConvention(typeof(MvcModule).Assembly);
            var controllerFactory = new WindsorControllerFactory(IocManager);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }


        public override void PostInitialize()
        {
            //1.注册认证过滤器
            GlobalFilters.Filters.Add(IocManager.Resolve<AuthorizeFilter>());
            //2.注册异常过滤器
            GlobalFilters.Filters.Add(IocManager.Resolve<ExceptionFilter>());
        }
    }
}
