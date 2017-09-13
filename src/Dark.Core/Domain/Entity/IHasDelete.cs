using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Domain.Entity
{
    /// <summary>
    /// 软删除
    /// </summary>
    public interface IHasDelete
    {
        bool IsDelete { get; set; }

        long? DeleteUser { get; set; }

        DateTime? DeleteTime { get; set; }
    }
}
