using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Authorization;
using Dark.Core.Runtime.Session;

namespace OA.Core.Authorization
{
    public class PermissionChecker : IPermissionChecker
    {
        private IPermissionManager _permissionManager;

        private IBaseSession _session;

        public PermissionChecker(IPermissionManager permissionManager, IBaseSession baseSession)
        {
            _permissionManager = permissionManager;
            _session = baseSession;
        }

        public Task<bool> IsGrantedAsync(string permissionName)
        {

            throw new NotImplementedException();
        }

        public Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            //1：检查
            throw new NotImplementedException();
        }

    }
}
