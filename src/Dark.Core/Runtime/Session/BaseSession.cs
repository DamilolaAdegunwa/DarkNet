using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Runtime.Session
{

    public interface IBaseSession
    {
        long? UserId { get; }
    }

    /// <summary>
    /// 用于实现自定义的Session数据
    /// </summary>
    public abstract class BaseSession : IBaseSession
    {
        public abstract long? UserId { get; }
    }
    
}
