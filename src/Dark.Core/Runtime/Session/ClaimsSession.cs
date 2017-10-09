using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.DI;

namespace Dark.Core.Runtime.Session
{
    public class ClaimsSession : BaseSession, ISingletonDependency
    {
        public override long? UserId
        {
            get
            {
                var userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == nameof(UserId));
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }

                long userId;
                if (!long.TryParse(userIdClaim.Value, out userId))
                {
                    return null;
                }

                return userId;
            }
        }

        public override string Account
        {
            get
            {
                var accountClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == nameof(Account));
                if (string.IsNullOrEmpty(accountClaim?.Value))
                {
                    return string.Empty;
                }
                return accountClaim.Value;
            }
        }

        protected IPrincipalAccessor PrincipalAccessor { get; }

        

        public ClaimsSession(IPrincipalAccessor principalAccessor)
        {

            PrincipalAccessor = principalAccessor;
        }

    }


  
}
