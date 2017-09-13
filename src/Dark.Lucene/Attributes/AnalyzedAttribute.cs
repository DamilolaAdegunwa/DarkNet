using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Lucene.Attributes.Lucene
{
    /// <summary>
    /// 定义该字段或者属性是否需要进行分析
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class AnalyzedAttribute : Attribute
    {
        public bool IsAnalyzed { get; set; } = true;
    }


}
