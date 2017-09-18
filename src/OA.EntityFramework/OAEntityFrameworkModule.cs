using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Modules;
using Dark.EntityFramework;
using OA.Core;
using OA.EntityFramework.EntityFramework;

namespace OA.EntityFramework
{
    [DependOn(typeof(EntityFrameworkModule), typeof(OACoreModule))]
    public class OAEntityFrameworkModule:BaseModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<OADbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterConvention(Assembly.GetExecutingAssembly());
        }
    }
}
