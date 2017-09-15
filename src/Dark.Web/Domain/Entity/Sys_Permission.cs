using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Authorization;
using Dark.Core.Domain.Entity;

namespace Dark.Web.Domain.Entity
{
    public class Sys_Permission :EntityBase
    {
        /// <summary>
        /// 创建人
        /// </summary>
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
