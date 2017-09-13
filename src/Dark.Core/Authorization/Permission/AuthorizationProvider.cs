using Dark.Core.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Authorization
{
    public abstract class AuthorizationProvider:ITransientDependency
    {
        public abstract void SetPermissions(IPermissionDefineContext context);
    }
}
