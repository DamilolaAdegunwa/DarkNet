using Dark.Core.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.ShopMallApp
{
    public class ShopAppService : AppService, IShopAppService
    {
        public void Show()
        {

            var pList = PermissionManager.GetAllPermissions();

            Logger.Debug("商品展示");
        }
    }
}
