using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Dark.Core.DI
{
    public class BasicRegisterConvention : IRegisterConvention
    {
        public void RegisterAssembly(IRegisterContext context)
        {
            //1:ITransient
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                .IncludeNonPublicTypes()
                .BasedOn<ITransientDependency>()
                .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                .WithService.Self()
                .WithService.DefaultInterfaces()
                .LifestyleTransient());

            //2:ISingleton
            context.IocManager.IocContainer.Register(
               Classes.FromAssembly(context.Assembly)
               .IncludeNonPublicTypes()
               .BasedOn<ISingletonDependency>()
               .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
               .WithService.Self()
               .WithService.DefaultInterfaces()
               .LifestyleSingleton());

            //3:注入拦截器
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<IInterceptor>()
                    .If(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                    .WithService.Self()
                    .LifestyleTransient()
                );
        }
    }
}
