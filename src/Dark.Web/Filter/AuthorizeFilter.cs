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

namespace Dark.Web.Filter
{
    //[AttributeUsage(AttributeTargets.Class , Inherited = true)]
    public class AuthorizeFilter : IAuthorizationFilter, ITransientDependency
    {
        private ILogger _logger;
        public AuthorizeFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //检查如果方法或者controller头上是否有 allowAnonymous 特性,如果有,那么就无需检查登陆
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            //检查session中是否存在该用户
            var sessionUser = filterContext.HttpContext.Session[typeof(AccountUser).Name];

            if (sessionUser == null || !(sessionUser is AccountUser))
            {
                //HttpContext.Current.Session["CurrentUrl"] = filterContext.RequestContext.HttpContext.Request.RawUrl;
                //filterContext.Result = new RedirectResult(this._loginPath);
            }
        }


        private void HandleException(AuthorizationContext filterContext)
        {

        }
    }
}
