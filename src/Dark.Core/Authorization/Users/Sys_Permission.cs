using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Authorization;
using Dark.Core.Domain.Entity;

namespace Dark.Core.Authorization.Users
{
    public class Sys_Permission :EntityBase
    {
        /// <summary>
        /// 创建人
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }


        public string Description { get; set; }

        /// <summary>
        /// 用户对应的Id
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 角色对应的Id
        /// </summary>
        public int? RoleId { get; set; }
    }
}
