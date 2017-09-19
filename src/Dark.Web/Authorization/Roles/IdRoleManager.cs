using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.DI;
using Dark.Core.Domain.Service;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization.Roles
{
    public class IdRoleManager : RoleManager<IdRole, int>, IDomainService
    {
        private IdRoleStore _roleStore;
        public IdRoleManager(IdRoleStore roleStore) : base(roleStore)
        {
            this._roleStore = roleStore;
        }
    }
}
