using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization.Users
{
    public class IdUserManager : UserManager<IdUser, int>
    {
        private IdUserStore _idUserStore;
        public IdUserManager(IdUserStore userStore) : base(userStore)
        {
            _idUserStore = userStore;
        }
    }
}
