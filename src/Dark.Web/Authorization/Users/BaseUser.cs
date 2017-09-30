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
        /// ����
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
        /// �ǳ�
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// ��ϵ��ʽ
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// �Ƿ���ס
        /// </summary>
        [Required]
        public bool IsLock { get; set; }

        /// <summary>
        /// �Ƿ��Ǽ���״̬
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

    }
}