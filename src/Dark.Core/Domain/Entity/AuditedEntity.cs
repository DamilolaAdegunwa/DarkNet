using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Domain.Entity
{
    /// <summary>
    /// 包含 创建信息,更新信息的Entity
    /// </summary>
    public class AuditedEntity : EntityBase, IHasModify
    {
        public DateTime? LastTime
        {
            get;set;
        }

        public long? Modifier
        {
            get; set;
        }
    }
}
