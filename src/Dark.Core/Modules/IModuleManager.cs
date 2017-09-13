using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Modules
{
    public interface IModuleManager
    {
        ModuleInfo StartModule { get; }

        void Initialize(Type startType);

        void StartModules();

        void Shutdown();
    }
}
