using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Authorization
{
    public interface ISkipAttribute
    {


    }

    /// <summary>
    /// 不需要进行
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SkipAttribute : Attribute, ISkipAttribute
    {

    }
}
