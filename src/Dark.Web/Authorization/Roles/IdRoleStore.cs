using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Authorization.Users;
using Dark.Core.DI;
using Dark.Core.Domain.Repository;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization.Roles
{
    public class IdRoleStore : IRoleStore<BaseRole, int>, ITransientDependency
    {
        private IRepository<BaseRole> _roleRepository;

        public IdRoleStore(IRepository<BaseRole> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task CreateAsync(BaseRole role)
        {
            await _roleRepository.InsertAsync(role);
        }

        public async Task DeleteAsync(BaseRole role)
        {
            await _roleRepository.DeleteAsync(role);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<BaseRole> FindByIdAsync(int roleId)
        {
            return await _roleRepository.FirstOrDefaultAsync(roleId);
        }

        public async Task<BaseRole> FindByNameAsync(string roleName)
        {
            return await _roleRepository.FirstOrDefaultAsync(u => u.Name.Equals(roleName));
        }

        public async Task UpdateAsync(BaseRole role)
        {
            await _roleRepository.UpdateAsync(role);
        }
    }
}
