using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.DI;
using Dark.Core.Domain.Repository;
using Dark.Web.Domain.Entity;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization.Roles
{
    public class IdRoleStore : IRoleStore<IdRole, int>, ITransientDependency
    {
        private IRepository<Sys_Role> _roleRepository;

        public IdRoleStore(IRepository<Sys_Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task CreateAsync(IdRole role)
        {
            await _roleRepository.InsertAsync(role);
        }

        public async Task DeleteAsync(IdRole role)
        {
            await _roleRepository.DeleteAsync(role);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<IdRole> FindByIdAsync(int roleId)
        {
            return (IdRole)await _roleRepository.FirstOrDefaultAsync(roleId);
        }

        public async Task<IdRole> FindByNameAsync(string roleName)
        {
            return (IdRole)await _roleRepository.FirstOrDefaultAsync(u => u.Name.Equals(roleName));
        }

        public async Task UpdateAsync(IdRole role)
        {
            await _roleRepository.UpdateAsync(role);
        }
    }
}
