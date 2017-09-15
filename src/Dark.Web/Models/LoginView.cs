using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Web.Models
{
    public class LoginModel
    {
        /// <summary>
        /// 手机,账号,邮箱
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 是否记忆
        /// </summary>
        public bool Remember { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Verification { get; set; }
    }

    
}
