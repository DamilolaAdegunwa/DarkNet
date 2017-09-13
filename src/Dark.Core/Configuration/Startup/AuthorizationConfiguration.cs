using Dark.Core.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Configuration.Startup
{
    public interface IAuthorizationConfiguration
    {
        List<AuthorizationProvider> Providers { get; }
    }

    public class AuthorizationConfiguration : IAuthorizationConfiguration
    {
        public List<AuthorizationProvider> Providers { get; }
    }
}
