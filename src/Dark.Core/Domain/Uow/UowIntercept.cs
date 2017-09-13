using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Domain.Uow
{
    /// <summary>
    /// 定义uow 工作单元
    /// </summary>
    public class UowIntercept : IInterceptor
    {
        //1:
        
        public UowIntercept()
        {

        }

        /// <summary>
        /// 拦截器
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            throw new NotImplementedException();
        }
    }
}
