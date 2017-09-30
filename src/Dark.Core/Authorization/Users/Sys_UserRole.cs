using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Domain.Entity;

namespace Dark.Core.Authorization.Users
{
    public class Sys_UserRole : EntityBase
    {

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }

       
    }
}
