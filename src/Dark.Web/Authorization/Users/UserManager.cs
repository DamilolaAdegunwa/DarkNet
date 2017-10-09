using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Dark.Core.Authorization.Users;
using Dark.Core.Domain.Entity;
using Dark.Core.Domain.Repository;
using Dark.Core.Domain.Service;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization.Users
{
    /// <summary>
    /// 管理用户
    /// </summary>
    public interface IUserManager
    {
        Task<string> CreateIdentityAsync(Sys_Account user,string cookName);
    }

  

    public class UserManager : IUserManager, IDomainService
    {
        private IRepository<Sys_Account> accountRepository;
        public UserManager(IRepository<Sys_Account> _accountRepository)
        {
            accountRepository = _accountRepository;
        }


        /// <summary>
        /// 创建缓存
        /// </summary>
        /// <returns></returns>
        public async Task<string> CreateIdentityAsync(Sys_Account user, string cookName="Application.Cookie")
        {
            return await Task.FromResult("");
        }
    }
}
