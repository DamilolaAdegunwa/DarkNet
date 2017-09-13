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
        Task<bool> IsGrantAsync(string permissionName);
        /// <summary>
        /// 检查具体的某个人是否有某个权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        Task<bool> IsGrantAsync(long userId, string permissionName);
    }
    #endregion

    #region 2.0 PermissionChecker 具体实现类
    /// <summary>
    /// 单例模式
    /// </summary>
    public class PermissionChecker : IPermissionChecker, ISingletonDependency
    {
        public async Task<bool> IsGrantAsync(string permissionName)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> IsGrantAsync(long userId, string permissionName)
        {
            return await Task.FromResult(true);
        }
    } 
    #endregion
}
