using Dark.Core.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Domain.Repository;
using OA.Core.Domain.Entities;

namespace OA.Application.ShopMallApp
{
    public class ShopAppService : AppService, IShopAppService
    {
        private readonly IRepository<OA_Category> _categoryRepository;

        public ShopAppService(IRepository<OA_Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Show()
        {

            var pList = PermissionManager.GetAllPermissions();

            Logger.Debug("商品展示");
        }
    }
}
