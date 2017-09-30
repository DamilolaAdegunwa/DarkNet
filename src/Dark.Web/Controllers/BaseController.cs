
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dark.Core.Runtime.Session;
using Dark.Web.Extensions;
using Dark.Web.Models;

namespace Dark.Web.Controllers
{

    public class BaseController : Controller
    {

        private IBaseSession BaseSession { get; set; }
        public BaseController()
        {
            BaseSession = NullSession.Instance;
        }
        //1:JSON重写,日期格式重置
        //2:Logger注入
        //3:权限检查
        //4:DTO规则验证

        protected async Task<JsonResult> ResultAsync(AjaxResult reuslt)
        {
            return await Task.FromResult(ToJSON(reuslt));
        }

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
