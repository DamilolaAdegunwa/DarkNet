using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Dark.Core.Modules;
using Dark.Core.Application.Service;
using Dark.Core.Log;
using Dark.Core.Configuration.Startup;
using Dark.Core.Configuration;

namespace Dark.Core.DI.Install
{
    public class CoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IModuleManager, ModuleManager>().ImplementedBy<ModuleManager>().LifestyleSingleton(),
                Component.For<IBaseConfiguration, BaseConfiguration>().ImplementedBy<BaseConfiguration>().LifestyleSingleton(),
                Component.For<IAuthorizationConfiguration, AuthorizationConfiguration>().ImplementedBy<AuthorizationConfiguration>().LifestyleSingleton()
            );
        }
    }
}
