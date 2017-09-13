using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Application.Service.Dto
{
    public class PageDto
    {



        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 页面的数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 忽略的数量
        /// </summary>
        public long SkipCount { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order { get; }


        public PageDto(int page, int pageSize, string sort = "", string order = "asc")
        {
            this.Page = page;
            this.SkipCount = page * pageSize;
            this.PageSize = PageSize;
            if (!string.IsNullOrEmpty(sort))
            {
                this.Sort = sort;
            }

            this.Order = order;
        }
    }
}
