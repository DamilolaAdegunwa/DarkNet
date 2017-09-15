using Castle.Core.Logging;
using Dark.Core.DI;
using Dark.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dark.Core.Authorization;
using Dark.Web.Extensions;
using System.Reflection;
using System.Net;

namespace Dark.Web.Authorization
{
    public class AuthorizeFilter : IAuthorizationFilter, ITransientDependency
    {
        private IAuthorizeHelper _authorizeHelper;
        private ILogger _logger;
        public AuthorizeFilter(IAuthorizeHelper authorizeHelper, ILogger logger)
        {
            _logger = logger;
            _authorizeHelper = authorizeHelper;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //检查如果方法或者controller头上是否有 allowAnonymous 特性,如果有,那么就无需检查登陆
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipAttribute), true)
                || filterContext.ActionDescriptor.IsDefined(typeof(SkipAttribute), true))
            {
                return;
            }

            var methodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return;
            }

            try
            {
                _authorizeHelper.AuthorizeAsync(methodInfo, methodInfo.DeclaringType);
            }
            catch (Exception ex)
            {
                _logger.Warn(ex.ToString(), ex);
                HandleUnauthorizedRequest(filterContext, methodInfo, ex);
            }
        }


        private void HandleUnauthorizedRequest(AuthorizationContext filterContext, MethodInfo methodInfo, Exception exception)
        {
            filterContext.HttpContext.Response.StatusCode =
               filterContext.RequestContext.HttpContext.User?.Identity?.IsAuthenticated ?? false
                   ? (int)HttpStatusCode.Forbidden
                   : (int)HttpStatusCode.Unauthorized;

            var isAjax = filterContext.HttpContext.Request.IsAjaxRequest();
            if (isAjax)
            {
                filterContext.Result = new JsonResult() { Data = AjaxResult.Fail("") };
            }
            else
            {
                //返回错误页面
                //filterContext.Result = CreateUnAuthorizedNonJsonResult(filterContext, ex);
            }

            if (isAjax )
            {
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
            }

        }
    }
}
