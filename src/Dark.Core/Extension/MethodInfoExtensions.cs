using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Extension
{
    public static class MethodInfoExtensions
    {
        public static bool IsAsync(this MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.GetTypeInfo().IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                );
        }
    }
}
