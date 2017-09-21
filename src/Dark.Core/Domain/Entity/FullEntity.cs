using Dark.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Entity
{
    /// <summary>
    /// 包含所有的属性字段Entity
    /// </summary>
    public class FullEntity : AuditedEntity, IHasDelete
    {
        public DateTime? DeleteTime
        {
            get; set;
        }

        public long? DeleteUser
        {
            get; set;
        }

        public bool IsDel
        {
            get; set;
        }

    }
}
