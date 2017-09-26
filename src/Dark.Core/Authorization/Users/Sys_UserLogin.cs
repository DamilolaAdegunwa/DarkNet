using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Domain.Entity;

namespace Dark.Core.Authorization.Users
{
    /// <summary>
    /// 登陆尝试
    /// </summary>
    public class Sys_UserLogin : EntityBase
    {
     
        public int? UserId { get; set; }

        public string Account { get; set; }

        public string Result { get; set; }

        public string ClientInfo { get; set; }

        public string ClientIp { get; set; }

        public string ClientName { get; set; }
    }
}
