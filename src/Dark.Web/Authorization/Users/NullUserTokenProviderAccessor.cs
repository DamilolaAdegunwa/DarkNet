using Dark.Core.DI;
using Microsoft.AspNet.Identity;

namespace Abp.Authorization.Users
{
    public class NullUserTokenProviderAccessor : IUserTokenProviderAccessor, ISingletonDependency
    {

        IUserTokenProvider<IdUser, int> IUserTokenProviderAccessor.GetUserTokenProviderOrNull<IdUser>()
        {
            return null;
        }
    }
}