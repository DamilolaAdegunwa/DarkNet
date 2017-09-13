using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo.Common.Crawler
{

    /// <summary>
    /// 定义规则集合
    /// </summary>
    public class CrawlerRule
    {
        //爬虫规则
        public string Rule { get; set; }
        //对于的属性名称
        public string PropertyName { get; set; }
        //是否是超链接
        public RuleType RuleType { get; set; } = RuleType.Text;
    }

    /// <summary>
    /// 获取的内容类别
    /// </summary>
    public enum RuleType
    {
        /// <summary>
        /// 获取文本
        /// </summary>
        Text,
        /// <summary>
        /// 获取标签内部的原生HTML
        /// </summary>
        Html,
        /// <summary>
        /// 获取超链接
        /// </summary>
        Href
    }
}
