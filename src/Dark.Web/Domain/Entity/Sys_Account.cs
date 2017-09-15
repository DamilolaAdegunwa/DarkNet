using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Entity;

namespace Dark.Web.Domain.Entity
{
    /// <summary>
    /// 系统账户信息
    /// </summary>
    public class Sys_Account : FullEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        public string Account { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否被锁住
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        /// 是否是激活状态
        /// </summary>
        public bool IsActive { get; set; }
    }
}
