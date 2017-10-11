using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Abp.Authorization.Users;
using Castle.Core.Internal;
using Dark.Core.Auditing;
using Dark.Core.Authorization.Users;
using Dark.Core.DI;
using Dark.Core.Domain.Repository;
using Dark.Core.Runtime.Session;
using Dark.Web.Authorization.Users;
using Dark.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Dark.Web.Authorization
{

    public interface ILoginManager
    {
        Task<AjaxResult> LoginAsync(LoginModel login);
    }

    public class LoginManager : ILoginManager, ITransientDependency
    {


        private IRepository<Sys_UserRole> userRoleRepository { get; }

        private UserManager userManager { get; }

        private IIocManager IocResolver { get; }
        private IClientInfoProvider _clientProvider;

        private IRepository<Sys_UserLogin> _loginAttemptsRepository;
       


        public LoginManager(
            IRepository<Sys_Account> _accountRepository,
            IIocManager iocResolver,
            UserManager _userManager,
            IRepository<Sys_UserRole> _userRoleRepository,
            IClientInfoProvider clientInfoProvider,
            IRepository<Sys_UserLogin> loginAttemptsRepository,
            IAuthenticationManager authenticationManager
            )
        {
            

            IocResolver = iocResolver;
            _clientProvider = clientInfoProvider;
            _loginAttemptsRepository = loginAttemptsRepository;
            userRoleRepository = _userRoleRepository;
            userManager = _userManager;
            //_authenticationManager = authenticationManager;
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


            var idUser = await userManager.FindByNameAsync(account);
            //1.检查人员是否存在
            if (idUser == null)
            {
                return AjaxResult.Fail(LoginPrompt.AccountNotExisit);
            }
            //2.账号未被激活
            if (!idUser.IsActive)
            {
                return AjaxResult.Fail(LoginPrompt.DisableAccount);
            }
            //3.检查密码是否正常
            if (idUser.Password != plainPassword)
            {
                return AjaxResult.Fail(LoginPrompt.PwdError);
            }
            //3.检查账户是否有授权

            if (await userRoleRepository.FirstOrDefaultAsync(u => u.UserId.Equals(idUser.Id)) == null)
            {
                return AjaxResult.Fail(LoginPrompt.NoGrant);
            }
            return await CreateLoginResultAsync(idUser,isRemember);
        }


        protected virtual async Task<AjaxResult> CreateLoginResultAsync(Sys_Account user,bool isRemember)
        {
            //1.创建Identity
            var identity =await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            if (identity == null)
            {
                return AjaxResult.Fail("Identity 未知");
            }
            //2.进行登陆
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isRemember }, identity);

            return AjaxResult.Ok(
                LoginPrompt.LoginSuccess
            );
        }

        /// <summary>
        /// 检查是否是第三方登陆
        /// </summary>
        /// <param name="userNameOrEmailAddress"></param>
        /// <param name="plainPassword"></param>
        /// <returns></returns>
        //protected virtual async Task<bool> TryLoginFromExternalAuthenticationSources(string account, string plainPassword)
        //{
        //    if (!UserManagementConfig.ExternalAuthenticationSources.Any())
        //    {
        //        return false;
        //    }

        //    foreach (var sourceType in UserManagementConfig.ExternalAuthenticationSources)
        //    {
        //        using (var source = IocResolver.ResolveAsDisposable<IExternalAuthenticationSource<TTenant, TUser>>(sourceType))
        //        {
        //            if (await source.Object.TryAuthenticateAsync(userNameOrEmailAddress, plainPassword, tenant))
        //            {
        //                var tenantId = tenant == null ? (int?)null : tenant.Id;
        //                using (UnitOfWorkManager.Current.SetTenantId(tenantId))
        //                {
        //                    var user = await UserManager.AbpStore.FindByNameOrEmailAsync(tenantId, userNameOrEmailAddress);
        //                    if (user == null)
        //                    {
        //                        user = await source.Object.CreateUserAsync(userNameOrEmailAddress, tenant);

        //                        user.TenantId = tenantId;
        //                        user.AuthenticationSource = source.Object.Name;
        //                        user.Password = UserManager.PasswordHasher.HashPassword(Guid.NewGuid().ToString("N").Left(16)); //Setting a random password since it will not be used

        //                        if (user.Roles == null)
        //                        {
        //                            user.Roles = new List<UserRole>();
        //                            foreach (var defaultRole in RoleManager.Roles.Where(r => r.TenantId == tenantId && r.IsDefault).ToList())
        //                            {
        //                                user.Roles.Add(new UserRole(tenantId, user.Id, defaultRole.Id));
        //                            }
        //                        }

        //                        await UserManager.AbpStore.CreateAsync(user);
        //                    }
        //                    else
        //                    {
        //                        await source.Object.UpdateUserAsync(user, tenant);

        //                        user.AuthenticationSource = source.Object.Name;

        //                        await UserManager.AbpStore.UpdateAsync(user);
        //                    }

        //                    await UnitOfWorkManager.Current.SaveChangesAsync();

        //                    return true;
        //                }
        //            }
        //        }
        //    }

        //    return false;
        //}
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

                UserId = user==null?0:user.Id,
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
