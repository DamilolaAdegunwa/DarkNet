using Dark.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    public class OAApplicationModule:BaseModule
    {
        public override void Initialize()
        {
            IocManager.RegisterConvention(Assembly.GetExecutingAssembly());
        }
    }
}
