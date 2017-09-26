using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Domain.Entity;

namespace Dark.Core.Authorization.Users
{
    public class Sys_UserRole : EntityBase
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }


        public Sys_UserRole(int userId,int roleId)
        {
            this.UserId = userId;
            this.RoleId = roleId;
        }
    }
}
