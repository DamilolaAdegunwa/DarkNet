using Dark.Core.Application.Permission;
using Dark.Core.DI;
using Dark.Core.Extension;
using Dark.Core.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Authorization
{

    public interface IAuthorizeHelper
    {
        Task AuthorizeAsync(MethodInfo method, Type type);
        Task AuthorizeAsync(IEnumerable<IBaseAuthorizeAttribute> attributes);
    }


    public class AuthorizeHelper : IAuthorizeHelper, ITransientDependency
    {

        private IPermissionChecker _permissionChecker;

        private IBaseSession Session { get; set; }

        public AuthorizeHelper(IPermissionChecker permissionChecker, IBaseSession baseSession)
        {
            _permissionChecker = permissionChecker;
        }

        /// <summary>
        /// 检查该方法,是否有权限
        /// </summary>
        /// <param name="method"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task AuthorizeAsync(MethodInfo method, Type type)
        {

            //1.检查是否有SkipAttribute,如果存在Skip特性,那么就直接返回,不在进行检查
            if (GetAttributes<SkipAttribute>(method, type).Any())
            {
                return;
            }


            //2.检查session 是否有用户登陆记录,如果用户未登陆,那么直接异常
            if (!Session.UserId.HasValue)
            {
                throw new Exception("人员未登陆");
            }


            //3.检查该方法是否有BaseAuthorizeAttribute特性
            var authorizes = GetAttributes<BaseAuthorizeAttribute>(method, type);
            //3.1 如果没有授权的话,那么也直接返回
            if (!authorizes.Any())
            {
                return;
            }

            //4. 存在的话,那么就校验权限是否符合

            await AuthorizeAsync(authorizes);
        }

        public async Task AuthorizeAsync(IEnumerable<IBaseAuthorizeAttribute> authorizeAttributes)
        {

            if (!Session.UserId.HasValue)
            {
                throw new Exception("人员未登陆");
            }
            bool isGrant = false;;
            //检查特性是否有授权
            foreach (var authorizeAttribute in authorizeAttributes)
            {
                if (authorizeAttribute.Permissions.IsNullOrEmpty())
                {
                    continue;
                }
                foreach (var pName in authorizeAttribute.Permissions)
                {
                    if( await _permissionChecker.IsGrantAsync(pName))
                    {
                        isGrant = true;
                        break;
                    }
                }
            }

            if (isGrant)
            {
                return;
            }
            else
            {
                throw new Exception($"UserId={Session.UserId}:未授权");
            }
        }




        private IEnumerable<T> GetAttributes<T>(MethodInfo method, Type type) where T : Attribute
        {
            return
                method.GetCustomAttributes<T>().Union(type.GetCustomAttributes<T>());
        }


    }




}
