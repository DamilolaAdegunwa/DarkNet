using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Castle.Core.Internal;
using Dark.Core.Auditing;
using Dark.Core.DI;
using Dark.Core.Domain.Repository;
using Dark.Web.Authorization.Roles;
using Dark.Web.Authorization.Users;
using Dark.Web.Domain.Entity;
using Dark.Web.Models;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization
{

    public interface ILoginManager
    {
        Task<AjaxResult> LoginAsync(LoginModel login);
    }

    public class LoginManager : ITransientDependency
    {

        protected IdUserManager UserManager { get; }
        protected IIocManager IocResolver { get; }
        protected IdRoleManager RoleManager { get; }
        private IClientInfoProvider _clientProvider;

        private IRepository<Sys_UserLoginAttempts> _loginAttemptsRepository;
        public LoginManager(
            IdUserManager userManager,
            IIocManager iocResolver,
            IdRoleManager roleManager,
            IClientInfoProvider clientInfoProvider,
            IRepository<Sys_UserLoginAttempts> loginAttemptsRepository)
        {

            IocResolver = iocResolver;
            RoleManager = roleManager;
            UserManager = userManager;
            _clientProvider = clientInfoProvider;
            _loginAttemptsRepository = loginAttemptsRepository;

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

            //Get and check tenant

            //TryLoginFromExternalAuthenticationSources method may create the user, that's why we are calling it before AbpStore.FindByNameOrEmailAsync
            //var loggedInFromExternalSource = await TryLoginFromExternalAuthenticationSources(account, plainPassword);


            var idUser = await UserManager.FindByNameAsync(account);
            //检查人员是否存在
            if (idUser == null)
            {
                return AjaxResult.Fail(LoginPrompt.AccountNotExisit);
            }
            //检查密码是否正常
            if (idUser.Password != plainPassword)
            {
                return AjaxResult.Fail(LoginPrompt.PwdError);
            }

            return await CreateLoginResultAsync(idUser);
        }


        protected virtual async Task<AjaxResult> CreateLoginResultAsync(IdUser user)
        {
            if (!user.IsActive)
            {
                return AjaxResult.Fail(LoginPrompt.DisableAccount);
            }

            user.LastTime = DateTime.Now;

            await UserManager.UpdateAsync(user);

            return AjaxResult.Ok(
                LoginPrompt.LoginSuccess,
                await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie)
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
            var loginAttempt = new Sys_UserLoginAttempts
            {

                UserId = (loginResult.Data as IdUser)?.Id,
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
