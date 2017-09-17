using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Modules;

namespace Dark.EntityFramework
{
    public class EntityFrameworkModule:BaseModule
    {
        public override void Initialize()
        {
            IocManager.RegisterConvention(Assembly.GetExecutingAssembly());
        }
    }
}
