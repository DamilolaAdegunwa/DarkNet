using Dark.Core.DI;
using Dark.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IocManager.RegisterConvention(typeof(CoreModule).Assembly);
        }
    }
}
