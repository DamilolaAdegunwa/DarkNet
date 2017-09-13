using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Mvc;
using System.Reflection;
using Dark.Core.DI;

namespace Dark.Web
{
    /// <summary>
    /// Web MVC 的注入规则
    /// </summary>
    public class MvcRegisterConvention : IRegisterConvention
    {
        public void RegisterAssembly(IRegisterContext context)
        {
            context.IocManager.IocContainer.Register(
               Classes.FromAssembly(context.Assembly)
               .BasedOn<Controller>()
               //非泛型
               .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
               .LifestyleTransient());
        }
    }
}
