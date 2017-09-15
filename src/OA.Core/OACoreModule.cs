using Dark.Core.Modules;
using OA.Core.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OA.Core
{

    public class OACoreModule : BaseModule
    {

        public override void PreInitialize()
        {
            Configuration.AuthConfig.Providers.Add(new OAAuthorizationProvider());
        }

        public override void Initialize()
        {
            IocManager.RegisterConvention(Assembly.GetExecutingAssembly());
        }
    }
}
