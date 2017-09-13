using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Application.Navigation
{
    /// <summary>
    /// 组合模式实现菜单
    /// </summary>
    public class Menu : IHasMenuItems
    {
        /// <summary>
        /// 主菜单的唯一名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 主菜单对外的显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 自定义的数据
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// 是否要进行权限验证
        /// </summary>
        public bool IsAuth { get; set; }

        /// <summary>
        /// 子菜单信息
        /// </summary>
        public IList<MenuItem> Items
        {
            get;
        }

        public Menu(string name, string dispName, object customData = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "Menu name can not be empty or null.");
            }
            
            if (string.IsNullOrEmpty(dispName))
            {
                throw new ArgumentNullException("displayName", "Menu displayName can not be empty or null.");
            }
            //给对象赋值
            this.Name = name;
            this.DisplayName = dispName;
            if (customData != null)
            {
                this.CustomData = customData;
            }
            Items = new List<MenuItem>();
        }

        public Menu AddMenuItem(MenuItem item)
        {
            this.Items.Add(item);
            return this;
        }
    }
}
