using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Abp.Authorization.Users;
using Dark.Core.Authorization.Users;
using Dark.Core.Domain.Entity;
using Dark.Core.Domain.Repository;
using Dark.Core.Domain.Service;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Dark.Web.Authorization.Users
{
    public class UserManager : UserManager<Sys_Account, int>, IDomainService
    {
        public UserManager(UserStore store):base(store)
        {

        }

        public async override Task<ClaimsIdentity> CreateIdentityAsync(Sys_Account user, string authenticationType)
        {
            return await base.CreateIdentityAsync(user, authenticationType);
        }

    }


}
