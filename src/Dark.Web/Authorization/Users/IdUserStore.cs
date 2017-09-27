using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Dark.Core.Authorization.Users;
using Dark.Core.DI;
using Dark.Core.Domain.Repository;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization.Users
{
    public class IdUserStore : IUserStore<IdUser, int>,
        IUserPasswordStore<IdUser,int>,ITransientDependency
    {
        private readonly IRepository<Sys_Account> _accountRepository;
        
        public IdUserStore(IRepository<Sys_Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        #region 1.0 Account
        public async Task CreateAsync(IdUser user)
        {
            await _accountRepository.InsertAsync(user);
        }

        public async Task DeleteAsync(IdUser user)
        {
            await _accountRepository.DeleteAsync(user);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<IdUser> FindByIdAsync(int userId)
        {
            return (IdUser)await _accountRepository.FirstOrDefaultAsync(userId);
        }

        public async Task<IdUser> FindByNameAsync(string userName)
        {
            return (IdUser)await _accountRepository.FirstOrDefaultAsync(u => u.UserName.Equals(userName));
        }

        public async Task UpdateAsync(IdUser user)
        {
            await _accountRepository.UpdateAsync(user);
        }
        #endregion

        #region 2.0 Password 操作
        public async Task<string> GetPasswordHashAsync(IdUser user)
        {
            return await Task.FromResult(user.Password);
        }

        public async Task<bool> HasPasswordAsync(IdUser user)
        {
            return await Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }

        public async Task SetPasswordHashAsync(IdUser user, string passwordHash)
        {
            user.Password = passwordHash;
            await _accountRepository.UpdateAsync(user);
        } 
        #endregion


    }
}
