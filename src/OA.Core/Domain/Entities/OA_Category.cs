using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Entity;

namespace OA.Core.Domain.Entities
{
    public class OA_Category:FullEntity
    {

        public string Name { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 类别描述
        /// </summary>
        public string Description { get; set; }

        public OA_Category(string name, int parentId,string desc = "")
        {
            this.Name = name;
            this.ParentId = parentId;
            this.Description = desc;
        }
    }
}
