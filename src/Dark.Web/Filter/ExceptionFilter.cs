using Castle.Core.Logging;
using Dark.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dark.Core.DI;

namespace Dark.Web.Filter
{
    public class ExceptionFilter : HandleErrorAttribute,ITransientDependency
    {

        private ILogger _logger;
        public ExceptionFilter(ILogger logger)
        {
            this._logger = logger;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            //1.如果异常已经处理,那么就不管了
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            //2.如果异常没有处理,那么就返回错误结果,及对于的错误页面
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            //找到当前登陆人的登陆信息
            //string userData = filterContext.HttpContext.Request.Cookies["userData"].Value;

            //3:记录错误信息
            Exception ex = filterContext.Exception;
            StringBuilder sbMsg = new StringBuilder();
            while (ex != null)
            {
                sbMsg.AppendFormat("控制器:{0},方法:{1}:错误信息:{2}", controllerName, actionName, ex.Message.ToString());
                ex = ex.InnerException;
            }
            _logger.ErrorFormat(sbMsg.ToString());
            //4:处理错误信息
            //4.1 如果是ajax请求,返回错误信息
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult() { Data = AjaxResult.Fail(sbMsg.ToString()) };
            }
            //4.2 如果是页面请求,那么就跳转到错误页面
            else
            {
                filterContext.Result = new RedirectResult("/Home/Error");
            }

            base.OnException(filterContext);

        }
    }
}
