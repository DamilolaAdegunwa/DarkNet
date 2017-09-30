using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Dark.Core.Authorization.Users;
using Dark.Core.DI;
using Dark.Core.Domain.Entity;
using Dark.Core.Domain.Repository;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization.Users
{
    public class IdUserStore : IUserStore<BaseUser, int>,
        IUserPasswordStore<BaseUser, int>, ITransientDependency
    {
        private readonly IRepository<BaseUser> _accountRepository;

        public IdUserStore(IRepository<BaseUser> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        //public IQueryable<Sys_Account> GetAll() => _accountRepository.GetAll();

        #region 1.0 Account
        public async Task CreateAsync(BaseUser user)
        {
            await _accountRepository.InsertAsync(user);
        }

        public async Task DeleteAsync(BaseUser user)
        {
            await _accountRepository.DeleteAsync(user);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

      
        public async Task<BaseUser> FindByIdAsync(int userId)
        {
            return await _accountRepository.FirstOrDefaultAsync(userId);
        }

        public async Task<BaseUser> FindByNameAsync(string account)
        {
            //1:通过账号来获取
            return await _accountRepository.FirstOrDefaultAsync(u => u.Account.Equals(account));
        }

        public async Task UpdateAsync(BaseUser user)
        {
            await _accountRepository.UpdateAsync(user);
        }
        #endregion

        #region 2.0 Password 操作
        public async Task<string> GetPasswordHashAsync(BaseUser user)
        {
            return await Task.FromResult(user.Password);
        }

        public async Task<bool> HasPasswordAsync(BaseUser user)
        {
            return await Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }

        public async Task SetPasswordHashAsync(BaseUser user, string passwordHash)
        {
            user.Password = passwordHash;
            await _accountRepository.UpdateAsync(user);
        }



        #endregion


    }
}
