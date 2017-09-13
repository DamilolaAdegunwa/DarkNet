using Dark.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Domain.Entity
{
    /// <summary>
    /// 数据更新
    /// </summary>
    public interface IHasModify
    {
        long? Modifier { get; set; }

        DateTime? LastTime { get; set; }
    }
   
}
