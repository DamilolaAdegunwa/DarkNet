using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Domain.Uow
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UnitWorkAttribute : Attribute
    {
        /// <summary>
        /// 自动提交
        /// </summary>
        public bool AutoCommit { get; set; }
    }
}
