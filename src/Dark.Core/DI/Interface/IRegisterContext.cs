using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.DI
{
    public interface IRegisterContext
    {
        IIocManager IocManager { get; }

        Assembly Assembly { get; }

        bool IsInstall { get; set; }
    }
}
