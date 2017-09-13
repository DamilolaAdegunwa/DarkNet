using Dark.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Application.Navigation
{
    public class MenuItem : IHasMenuItems
    {
        /// <summary>
        /// 唯一的名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 对外展示的名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// menu对应的请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// menu 的序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 菜单的图表
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否需要进行登陆验证
        /// </summary>
        public bool IsAuth { get; set; }
        /// <summary>
        /// 是否是叶子
        /// </summary>
        public bool IsLeaf => Items.IsNullOrEmpty();

        /// <summary>
        /// 
        /// </summary>
        public virtual IList<MenuItem> Items
        {
            get;
        }


        public MenuItem AddMenuItem(MenuItem item)
        {
            this.Items.Add(item);
            return this;
        }

    }
}
