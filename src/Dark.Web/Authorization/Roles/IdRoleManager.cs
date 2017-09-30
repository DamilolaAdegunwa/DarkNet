using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Authorization.Users;
using Dark.Core.DI;
using Dark.Core.Domain.Repository;
using Dark.Core.Domain.Service;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization.Roles
{
    public class IdRoleManager : RoleManager<BaseRole, int>, IDomainService
    {
        private IdRoleStore _roleStore;
        private IRepository<Sys_UserRole> userRoleRepository;
        public IdRoleManager(IdRoleStore roleStore, IRepository<Sys_UserRole> _userRoleRepository) : base(roleStore)
        {
            this._roleStore = roleStore;
            userRoleRepository = _userRoleRepository;
        }

        /// <summary>
        /// 检查人员是否有授权
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> IsGrantAsync(int userId)
        {
            var role = await userRoleRepository.FirstOrDefaultAsync(u => u.UserId.Equals(userId));
            return role != null;
        }
    }
}
