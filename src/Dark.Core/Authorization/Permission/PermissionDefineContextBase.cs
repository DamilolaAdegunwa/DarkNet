using Dark.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Authorization
{
    public interface IPermissionDefineContext
    {
        Permission CreatePermission(string name, string displayName = null, string description = null);
        /// <summary>
        /// Gets a permission with given name or null if can not find.
        /// </summary>
        /// <param name="name">Unique name of the permission</param>
        /// <returns>Permission object or null</returns>
        Permission GetPermissionOrNull(string name);
    }

    public abstract class PermissionDefineContextBase : IPermissionDefineContext
    {
        protected readonly Dictionary<string, Permission> Permissions;

        protected PermissionDefineContextBase()
        {
            Permissions = new Dictionary<string, Permission>();
        }

        public Permission CreatePermission(string name, string displayName = null, string description = null)
        {
            if (Permissions.ContainsKey(name))
            {
                throw new ArgumentNullException("这个权限的名称已经存在: " + name);
            }
            var permission = new Permission(name, displayName, description);
            //存放字典集合中
            Permissions[permission.Name] = permission;
            return permission;
        }

        public Permission GetPermissionOrNull(string name)
        {
            return Permissions.GetOrDefault(name);
        }


    }
}
