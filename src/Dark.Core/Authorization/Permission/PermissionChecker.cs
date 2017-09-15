using Dark.Core.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Authorization
{
    #region 1.0 IPermissionChecker 接口
    /// <summary>
    /// 权限检查接口
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        /// 检查当前用户是否有该权限
        /// </summary>
        /// <param name="perName"></param>
        /// <returns></returns>
        Task<bool> IsGrantedAsync(string permissionName);
        /// <summary>
        /// 检查具体的某个人是否有某个权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        Task<bool> IsGrantedAsync(long userId, string permissionName);
    }
    #endregion

    #region 2.0 PermissionChecker 具体实现类
    /// <summary>
    /// 单例模式
    /// </summary>
    public sealed class NullPermissionChecker : IPermissionChecker
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullPermissionChecker Instance { get; } = new NullPermissionChecker();

        public Task<bool> IsGrantedAsync(string permissionName)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            return Task.FromResult(true);
        }
    }
    #endregion
}
