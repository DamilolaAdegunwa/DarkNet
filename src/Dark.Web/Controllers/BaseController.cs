
using System;
using System.Text;
using System.Web.Mvc;
using Dark.Web.Extensions;

namespace Dark.Web.Controllers
{

    public class BaseController : Controller
    {
        //1:JSON重写,日期格式重置
        //2:Logger注入
        //3:权限检查
        //4:DTO规则验证

        #region 1.0 Json重构
        /// <summary>
        /// 对返回前端的json数据进行重写
        /// </summary>
        /// <param name="data"></param>
        /// <param name="formatteDate"></param>
        /// <returns></returns>
        protected JsonResult ToJSON(object data, string formatteDate = "yyyy-MM-dd")
        {
            return new JsonResult()
            {
                Data = data.ToJson(formatteDate, true, true),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
            };
        }
        #endregion
        
    }
}
