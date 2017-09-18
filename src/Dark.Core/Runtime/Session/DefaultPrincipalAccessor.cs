using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dark.Core.DI;

namespace Dark.Core.Runtime.Session
{

    public interface IPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
    }

    public class DefaultPrincipalAccessor : IPrincipalAccessor, ISingletonDependency
    {
        public virtual ClaimsPrincipal Principal => System.Threading.Thread.CurrentPrincipal as ClaimsPrincipal;

        public static DefaultPrincipalAccessor Instance => new DefaultPrincipalAccessor();
    }
}
