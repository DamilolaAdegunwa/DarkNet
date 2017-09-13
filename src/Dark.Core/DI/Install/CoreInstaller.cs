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
using Dark.Core.Application.Permission;
using Dark.Core.Log;

namespace Dark.Core.DI.Install
{
    public class CoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IModuleManager, ModuleManager>().ImplementedBy<ModuleManager>().LifestyleSingleton(),
                Component.For<IPermissionManager, PermissionManager>().ImplementedBy<PermissionManager>().LifestyleSingleton()
            );
        }
    }
}
