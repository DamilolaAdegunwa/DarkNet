using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Dark.Core.Authorization.Users;
using Dark.Core.Domain.Entity;
using Microsoft.AspNet.Identity;

namespace Dark.Web.Authorization.Roles
{
    [Table("Sys_Role")]
    public abstract class BaseRole : EntityBase, IRole<int>
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
