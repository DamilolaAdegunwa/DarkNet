using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Authorization
{

    public interface IBaseAuthorizeAttribute
    {
        /// <summary>
        /// 有哪些权限可以对该方法进行访问
        /// </summary>
        string[] Permissions { get; }
    }

    /// <summary>
    /// 用于授权的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class BaseAuthorizeAttribute : Attribute, IBaseAuthorizeAttribute
    {
        public string[] Permissions { get; }

        public BaseAuthorizeAttribute(params string[] permissions)
        {
            this.Permissions = permissions;
        }
    }
}
