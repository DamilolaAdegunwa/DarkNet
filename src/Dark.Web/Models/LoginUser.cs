using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Web.Models
{
    /// <summary>
    /// 账户信息
    /// </summary>
    public class AccountUser
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
