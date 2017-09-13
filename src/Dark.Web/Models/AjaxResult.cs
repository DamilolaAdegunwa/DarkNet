using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Web.Models
{
    public class AjaxResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public DoResult Result { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string PromptMsg { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 如需跳转URL
        /// </summary>
        public string RedirectURL { get; set; }


        public static AjaxResult Ok(string msg = "", object data = null, string rURL = "")
        {
            return new AjaxResult()
            {
                Result = DoResult.Success,
                PromptMsg = msg,
                Data = data,
                RedirectURL = rURL
            };
        }

        public static AjaxResult Fail(string msg, object data = null, string rURL = "")
        {
            return new AjaxResult()
            {
                Result = DoResult.Fail,
                PromptMsg = msg,
                Data = data,
                RedirectURL = rURL
            };
        }

    }



    public enum DoResult
    {
        Fail = 0,
        Success = 1,
        Overtime = 2,
        Other = 255
    }
}
