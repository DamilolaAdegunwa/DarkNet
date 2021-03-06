﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.DI;

namespace Dark.Core.Runtime.Session
{

    public interface IBaseSession
    {
        long? UserId { get; }

        string Account { get; }
    }

    /// <summary>
    /// 用于实现自定义的Session数据
    /// </summary>
    public abstract class BaseSession : IBaseSession
    {
        public abstract long? UserId { get; }

        public abstract string Account { get; }
    }
    

}
