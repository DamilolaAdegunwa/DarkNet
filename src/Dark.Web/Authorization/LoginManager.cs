using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Castle.Core.Internal;
using Dark.Core.DI;
using Dark.Web.Authorization.Roles;
using Dark.Web.Authorization.Users;
using Dark.Web.Domain.Entity;
using Dark.Web.Models;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization
{
    public class LoginManager : ITransientDependency
    {

        protected IdUserManager UserManager { get; }
        protected IIocManager IocResolver { get; }
        protected IdRoleManager RoleManager { get; }

        protected LoginManager(
            IdUserManager userManager,
            IIocManager iocResolver,
            IdRoleManager roleManager)
        {

            IocResolver = iocResolver;
            RoleManager = roleManager;
            UserManager = userManager;

        }


        //protected virtual async Task<AjaxResult> LoginAsyncInternal(LoginView login)
        //{
        //    return await CreateLoginResultAsync(login);
        //}

        public virtual async Task<AjaxResult> LoginAsync(string account, string plainPassword, bool isRemember = false)
        {
            var result = await LoginAsyncInternal(account, plainPassword, isRemember);
            await SaveLoginAttempt(result, account);
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

            using (UnitOfWorkManager.Current.SetTenantId(tenantId))
            {
                //TryLoginFromExternalAuthenticationSources method may create the user, that's why we are calling it before AbpStore.FindByNameOrEmailAsync
                //var loggedInFromExternalSource = await TryLoginFromExternalAuthenticationSources(userNameOrEmailAddress, plainPassword, tenant);


                var idUser = await UserManager.FindByNameAsync(account);
                if (idUser == null)
                {
                    return AjaxResult.Fail(LoginPrompt.AccountNotExisit);
                }

                if (!loggedInFromExternalSource)
                {
                    UserManager.InitializeLockoutSettings(tenantId);

                    if (await UserManager.IsLockedOutAsync(user.Id))
                    {
                        return new AbpLoginResult<TTenant, TUser>(AbpLoginResultType.LockedOut, tenant, user);
                    }

                    var verificationResult = UserManager.PasswordHasher.VerifyHashedPassword(user.Password, plainPassword);
                    if (verificationResult == PasswordVerificationResult.Failed)
                    {
                        return await GetFailedPasswordValidationAsLoginResultAsync(user, tenant, shouldLockout);
                    }

                    if (verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
                    {
                        return await GetSuccessRehashNeededAsLoginResultAsync(user, tenant);
                    }

                    await UserManager.ResetAccessFailedCountAsync(user.Id);
                }

                return await CreateLoginResultAsync(user, tenant);
            }
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
            using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                var loginAttempt = new Sys_UserLoginAttempts
                {

                    UserId = (loginResult.Data as IdUser)?.Id,
                    Account = account,

                    Result = loginResult.PromptMsg,

                    BrowserInfo = ClientInfoProvider.BrowserInfo,
                    ClientIpAddress = ClientInfoProvider.ClientIpAddress,
                    ClientName = ClientInfoProvider.ComputerName,
                };

                await UserLoginAttemptRepository.InsertAsync(loginAttempt);
                await UnitOfWorkManager.Current.SaveChangesAsync();

                await uow.CompleteAsync();
            }
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
