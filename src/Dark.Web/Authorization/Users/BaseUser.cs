using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dark.Core.Authorization.Users;
using Dark.Core.Domain.Entity;
using Dark.Core.Entity;
using Microsoft.AspNet.Identity;

namespace Abp.Authorization.Users
{
    [Table("Sys_Account")]
    public abstract class BaseUser : FullEntity, IUser<int>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
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
        [Required]
        public bool IsLock { get; set; }

        /// <summary>
        /// 是否是激活状态
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

    }
}