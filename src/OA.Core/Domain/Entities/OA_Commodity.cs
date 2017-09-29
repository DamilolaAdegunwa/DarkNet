using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Entity;

namespace OA.Core.Domain.Entities
{
    public class OA_Commodity : FullEntity
    {
        public string Name { get; set; }

        public string Describe { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Picture { get; set; }

        public List<string> Pictures { get; set; }

        public OA_Commodity(int categoryId,string name)
        {
            this.CategoryId = categoryId;
            this.Name = name;
        }

        public OA_Commodity()
        {

        }
    }
}
