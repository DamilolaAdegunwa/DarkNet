using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Application.Service.Dto
{
    /// <summary>
    /// 返回结果集
    /// </summary>
    public class PageListDto<T> where T:class
    {
        /// <summary>
        /// 返回的所有集合的数量
        /// </summary>
        long Total { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        List<T> Items { get; set; }


        public PageListDto(long total,List<T> items)
        {
            Total = total;
            Items = items;
        }
    }
}
