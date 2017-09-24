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
using Dark.Core.Reflections;
using Dark.Core.Domain.Uow;

namespace Dark.Core.DI.Install
{
    public class CoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>().ImplementedBy<UnitOfWorkDefaultOptions>().LifestyleSingleton(),
                Component.For<IModuleManager, ModuleManager>().ImplementedBy<ModuleManager>().LifestyleSingleton(),
                Component.For<IBaseConfiguration, BaseConfiguration>().ImplementedBy<BaseConfiguration>().LifestyleSingleton(),
                Component.For<IAuthorizationConfiguration, AuthorizationConfiguration>().ImplementedBy<AuthorizationConfiguration>().LifestyleSingleton(),
                Component.For<IAssemblyFinder, AssemblyFinder>().ImplementedBy<AssemblyFinder>().LifestyleSingleton(),
                Component.For<ITypeFinder, TypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton()
            );
        }
    }
}
