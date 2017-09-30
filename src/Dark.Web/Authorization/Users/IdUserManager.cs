using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Dark.Core.Authorization.Users;
using Dark.Core.Domain.Entity;
using Dark.Core.Domain.Service;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization.Users
{
    public class IdUserManager : UserManager<BaseUser, int>, IDomainService
    {
        private IdUserStore _idUserStore;
        public IdUserManager(IdUserStore userStore) : base(userStore)
        {
            _idUserStore = userStore;
        }
    }
}
