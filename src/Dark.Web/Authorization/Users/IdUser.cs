using Dark.Web.Domain.Entity;
using Microsoft.AspNet.Identity;

namespace Abp.Authorization.Users
{
    public abstract class IdUser : Sys_Account, IUser<int>
    {
        
    }
}