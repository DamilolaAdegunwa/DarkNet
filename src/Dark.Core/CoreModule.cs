using Dark.Core.Authorization;
using Dark.Core.DI;
using Dark.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Configuration;

namespace Dark.Core
{
    public class CoreModule : BaseModule
    {

        public override void PreInitialize()
        {
            IocManager.AddRegisterConvention(new BasicRegisterConvention());
        }

        public override void Initialize()
        {

            foreach (var replaceAction in ((BaseConfiguration)Configuration).ServiceReplaceActions.Values)
            {
                replaceAction();
            }

            IocManager.RegisterConvention(typeof(CoreModule).Assembly);
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<PermissionManager>().Initialize();
        }
    }
}
