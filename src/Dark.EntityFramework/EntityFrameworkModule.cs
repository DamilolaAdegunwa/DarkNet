using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Dark.Core.Modules;

namespace Dark.EntityFramework
{
    public class EntityFrameworkModule:BaseModule
    {
        public override void Initialize()
        {
            IocManager.RegisterConvention(Assembly.GetExecutingAssembly());

            ///
            IocManager.IocContainer.Register(
               Component.For(typeof(IDbContextProvider<>))
                   .ImplementedBy(typeof(UnitOfWorkDbContextProvider<>))
                   .LifestyleTransient()
               );
        }
    }
}
