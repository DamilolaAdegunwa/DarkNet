using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization.Roles
{
    public class IdRoleManager : RoleManager<IdRole, int>
    {
        private readonly IdRoleStore _roleStore;
        public IdRoleManager(IdRoleStore roleStore) : base(roleStore)
        {

        }
    }
}
