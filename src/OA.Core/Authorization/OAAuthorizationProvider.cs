using Dark.Core.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Core.Authorization
{
    public class OAAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefineContext context)
        {
            //后台管理
            var adminPermission = context.CreatePermission("Admin", "后台");
            adminPermission.CreateChildPermission("Admin.CacheManage", "缓存管理");
            adminPermission.CreateChildPermission("Admin.DictManage", "字典挂你");
            //商品管理
            var shopPermission = context.CreatePermission("Shop", "商品");
            shopPermission.CreateChildPermission("Shop.Manage", "商品管理");
        }
    }
}
