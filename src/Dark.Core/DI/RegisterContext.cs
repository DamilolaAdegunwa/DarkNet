using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.DI
{
    public class RegisterContext: IRegisterContext
    {
        public IIocManager IocManager { get;  }

        public Assembly Assembly { get;  }

        public bool IsInstall { get; set; }

        public RegisterContext(IIocManager iocManager,Assembly assembly,bool isInstall)
        {
            IocManager = iocManager;
            Assembly = assembly;
            IsInstall = isInstall;
        }
    }
}
