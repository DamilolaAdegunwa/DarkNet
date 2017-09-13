using Dark.Core.Configuration.Startup;
using Dark.Core.DI;
using Dark.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Authorization
{

    public interface IPermissionManager
    {

        /// <summary>
        /// Gets <see cref="Permission"/> object with given <paramref name="name"/> or throws exception
        /// if there is no permission with given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Unique name of the permission</param>
        Permission GetPermission(string name);

        /// <summary>
        /// Gets <see cref="Permission"/> object with given <paramref name="name"/> or returns null
        /// if there is no permission with given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Unique name of the permission</param>
        Permission GetPermissionOrNull(string name);

        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <param name="tenancyFilter">Can be passed false to disable tenancy filter.</param>
        IReadOnlyList<Permission> GetAllPermissions();

    }


    public class PermissionManager : PermissionDefineContext, IPermissionManager, ITransientDependency
    {
        private IIocManager _iocManager;
        private IAuthorizationConfiguration _authConfig;

        public PermissionManager(IIocManager iocManager,IAuthorizationConfiguration authorizationConfiguration)
        {
            _authConfig = authorizationConfiguration;
            _iocManager = iocManager;
        }

        public IReadOnlyList<Permission> GetAllPermissions()
        {
            _authConfig.Providers.ForEach(u =>
            {
                u.SetPermissions(this);
            });

            
        }

        public Permission GetPermission(string name)
        {
            var permission = Permissions.GetOrDefault(name);
            if (permission == null)
            {
                throw new Exception("权限不存在: " + name);
            }
            return permission;
        }

    }
}
