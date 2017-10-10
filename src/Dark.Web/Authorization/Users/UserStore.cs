using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Authorization.Users;
using Dark.Core.DI;
using Dark.Core.Domain.Repository;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization.Users
{
    public class UserStore : 
        IUserStore<Sys_Account, int>,
        IUserPasswordStore<Sys_Account,int>, 
        ITransientDependency
    {
        private IRepository<Sys_Account> accountRepository;
        public UserStore(IRepository<Sys_Account> _accountRepository)
        {
            accountRepository = _accountRepository;
        }

        #region 1.0 UserStore
        public Task CreateAsync(Sys_Account user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Sys_Account user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<Sys_Account> FindByIdAsync(int userId)
        {
            return await accountRepository.FirstOrDefaultAsync(u => u.Id.Equals(userId));
        }

        public async Task<Sys_Account> FindByNameAsync(string userName)
        {
            return await accountRepository.FirstOrDefaultAsync(u => u.Account.Equals(userName));
        }
        #endregion

        #region 2.0 UserPasswordStore
        public Task<string> GetPasswordHashAsync(Sys_Account user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(Sys_Account user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(Sys_Account user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Sys_Account user)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
