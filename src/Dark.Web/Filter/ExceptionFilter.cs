using Castle.Core.Logging;
using Dark.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dark.Web.Filter
{
    public class ExceptionFilter : HandleErrorAttribute
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
            string areaName = filterContext.RouteData.Values["area"].ToString();
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            //找到当前登陆人的登陆信息
            string userData = filterContext.HttpContext.Request.Cookies["userData"].Value;

            //3:记录错误信息
            Exception ex = filterContext.Exception;
            while (ex != null)
            {
                _logger.ErrorFormat("区域:{0},控制器:{1},方法:{2}:错误信息:{4}", areaName, controllerName, actionName, ex.Message.ToString());
                ex = ex.InnerException;
            }

            //4:处理错误信息
            //4.1 如果是ajax请求,返回错误信息
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult() { Data = AjaxResult.Fail(ex.Message) };
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
