using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Castle.Core.Internal;
using Dark.Core.Auditing;
using Dark.Core.Authorization.Users;
using Dark.Core.DI;
using Dark.Core.Domain.Repository;
using Dark.Core.Runtime.Session;
using Dark.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Dark.Web.Authorization
{

    public interface ILoginManager
    {
        Task<AjaxResult> LoginAsync(LoginModel login);
        Task<bool> LoginOut();
    }

    public class LoginManager : ILoginManager, ITransientDependency
    {

        private IRepository<Sys_Account> _accountRepository { get; }

        private IRepository<Sys_UserRole> userRoleRepository { get; }


        private IIocManager IocResolver { get; }

        private IClientInfoProvider _clientProvider;

        private IRepository<Sys_UserLogin> _loginAttemptsRepository;

        private IAuthenticationManager _authenticationManager;

        public LoginManager(
            IRepository<Sys_Account> _accountRepository,
            IIocManager iocResolver,
            IRepository<Sys_UserRole> _userRoleRepository,
            IClientInfoProvider clientInfoProvider,
            IRepository<Sys_UserLogin> loginAttemptsRepository,
            IRepository<Sys_Account> accountRepository,
            IAuthenticationManager authenticationManager
            )
        {


            IocResolver = iocResolver;
            _clientProvider = clientInfoProvider;
            _loginAttemptsRepository = loginAttemptsRepository;
            userRoleRepository = _userRoleRepository;
            this._accountRepository = accountRepository;
            _authenticationManager = authenticationManager;
        }


        public virtual async Task<AjaxResult> LoginAsync(LoginModel login)
        {
            //1:登陆
            var result = await LoginAsyncInternal(login.Account, login.Password, login.Remember);
            //2:记录登陆尝试
            await SaveLoginAttempt(result, login.Account);
            //2:返回登陆结果
            return result;
        }

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="account"></param>
        /// <param name="plainPassword"></param>
        /// <param name="isRemember"></param>
        /// <returns></returns>
        protected virtual async Task<AjaxResult> LoginAsyncInternal(string account, string plainPassword, bool isRemember)
        {
            if (account.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (plainPassword.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(plainPassword));
            }


            Sys_Account user = await _accountRepository.FirstOrDefaultAsync(u => u.Account.Equals(account));
            //1.检查人员是否存在
            if (user == null)
            {
                return AjaxResult.Fail(LoginPrompt.AccountNotExisit);
            }
            //2.账号未被激活
            if (!user.IsActive)
            {
                return AjaxResult.Fail(LoginPrompt.DisableAccount);
            }
            //3.检查密码是否正常
            if (user.Password != plainPassword)
            {
                return AjaxResult.Fail(LoginPrompt.PwdError);
            }
            //3.检查账户是否有授权

            if (await userRoleRepository.FirstOrDefaultAsync(u => u.UserId.Equals(user.Id)) == null)
            {
                return AjaxResult.Fail(LoginPrompt.NoGrant);

            }

            return await CreateLoginResultAsync(user, isRemember);
        }

        /// <summary>
        /// 创建缓存cookie
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isRemember"></param>
        /// <returns></returns>
        protected virtual async Task<AjaxResult> CreateLoginResultAsync(Sys_Account user, bool isRemember)
        {
            //1:创建证件管理着
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            //2：创建证件
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Account));

            //3:登陆
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isRemember }, claimsIdentity);

            return await Task.FromResult(AjaxResult.Ok(
                LoginPrompt.LoginSuccess
            ));
        }
        
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> LoginOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return await Task.FromResult(true);
        }
        /// <summary>
        /// 登陆尝试
        /// </summary>
        /// <param name="loginResult"></param>
        /// <param name="tenancyName"></param>
        /// <param name="userNameOrEmailAddress"></param>
        /// <returns></returns>
        protected virtual async Task SaveLoginAttempt(AjaxResult loginResult, string account)
        {
            var user = loginResult.Data as Sys_Account;
            var loginAttempt = new Sys_UserLogin
            {

                UserId = user == null ? 0 : user.Id,
                Account = account,

                Result = loginResult.PromptMsg,

                ClientInfo = _clientProvider.ClientInfo,
                ClientIp = _clientProvider.ClientIpAddress,
                ClientName = _clientProvider.ClientName,
            };

            await _loginAttemptsRepository.InsertAsync(loginAttempt);
        }

        //protected virtual async Task<AbpLoginResult<TTenant, TUser>> GetFailedPasswordValidationAsLoginResultAsync(TUser user, TTenant tenant = null, bool shouldLockout = false)
        //{
        //    if (shouldLockout)
        //    {
        //        if (await TryLockOutAsync(user.TenantId, user.Id))
        //        {
        //            return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.LockedOut, tenant, user);
        //        }
        //    }

        //    return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.InvalidPassword, tenant, user);
        //}

        //protected virtual async Task<AbpLoginResult<TTenant, TUser>> GetSuccessRehashNeededAsLoginResultAsync(TUser user, TTenant tenant = null, bool shouldLockout = false)
        //{
        //    return await GetFailedPasswordValidationAsLoginResultAsync(user, tenant, shouldLockout);
        //}

        //protected virtual async Task<AjaxResult> CreateLoginResultAsync(Sys_Account user)
        //{
        //    if (!user.IsActive)
        //    {
        //        return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.UserIsNotActive);
        //    }

        //    if (await IsEmailConfirmationRequiredForLoginAsync(user.TenantId) && !user.IsEmailConfirmed)
        //    {
        //        return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.UserEmailIsNotConfirmed);
        //    }

        //    user.LastLoginTime = Clock.Now;

        //    await UserManager.AbpStore.UpdateAsync(user);

        //    await UnitOfWorkManager.Current.SaveChangesAsync();

        //    return new AbpLoginResult<TTenant, TUser>(
        //        tenant,
        //        user,
        //        await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie)
        //    );
        //}

    }
}
