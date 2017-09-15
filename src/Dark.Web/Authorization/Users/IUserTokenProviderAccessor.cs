using Microsoft.AspNet.Identity;

namespace Abp.Authorization.Users
{
    public interface IUserTokenProviderAccessor
    {
        IUserTokenProvider<TUser,int> GetUserTokenProviderOrNull<TUser>() 
            where TUser : IdUser;
    }
}