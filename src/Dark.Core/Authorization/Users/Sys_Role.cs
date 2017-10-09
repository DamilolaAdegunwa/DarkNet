using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Domain.Entity;

namespace Dark.Core.Authorization.Users
{
    public class Sys_Role : EntityBase
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 角色也可以分级
        /// </summary>
        public int? ParentId { get; set; }
    }
}
