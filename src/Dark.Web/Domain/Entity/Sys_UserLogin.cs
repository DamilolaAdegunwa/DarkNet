using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Domain.Entity;

namespace Dark.Web.Domain.Entity
{
    /// <summary>
    /// 登陆尝试
    /// </summary>
    public class Sys_UserLoginAttempts : IEntity<int>
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string Account { get; set; }

        public string Result { get; set; }

        public string ClientInfo { get; set; }

        public string ClientIp { get; set; }

        public string ClientName { get; set; }
    }
}
