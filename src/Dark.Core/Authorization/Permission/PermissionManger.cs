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


    public class PermissionManager : PermissionDefineContextBase, IPermissionManager, ISingletonDependency
    {
        private IIocManager _iocManager;
        private IAuthorizationConfiguration _authConfig;

        public PermissionManager(IIocManager iocManager, IAuthorizationConfiguration authorizationConfiguration)
        {
            _authConfig = authorizationConfiguration;
            _iocManager = iocManager;
        }


        public void Initialize()
        {

            //1.向字典Permissions中添加数据
            _authConfig.Providers.ForEach(u =>
            {
                u.SetPermissions(this);
            });
            

            AddAllToPermission();
        }

        private void AddAllToPermission()
        {
            //2:向权限字典集合中添加权限数据
            var initPList = new List<Permission>();

            Permissions.Select(u => u.Value).ToList().ForEach(p =>
            {
                if (!initPList.Contains(p))
                {
                    initPList.Add(p);
                }
                initPList.AddRange(GetRecursivePList(p));
            });

            foreach (var item in initPList)
            {
                if (!Permissions.ContainsKey(item.Name))
                {
                    Permissions[item.Name] = item;
                }
            }
        }

        public IReadOnlyList<Permission> GetAllPermissions()
        {
            return Permissions.Select(u => u.Value).ToList();
        }

        /// <summary>
        /// 获取权限的集合 递归查找集合
        /// </summary>
        /// <param name="pList"></param>
        /// <returns></returns>
        private List<Permission> GetRecursivePList(Permission permission)
        {
            List<Permission> pList = new List<Permission>();
            permission.Children.ForEach(p =>
            {
                if (!pList.Contains(p))
                {
                    pList.Add(p);
                }
                pList.AddRange(GetRecursivePList(p));
            });

            return pList;
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
