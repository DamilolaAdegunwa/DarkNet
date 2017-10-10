using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.EntityFramework;
using OA.Core.Domain.Entities;

namespace OA.EntityFramework.EntityFramework
{
    public class OADbContext : BaseDbContext
    {
        /// <summary>
        /// 商品分类
        /// </summary>
        public IDbSet<OA_Category> OA_Category { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public IDbSet<OA_Commodity> OA_Commodity { get; set; }


      


        public OADbContext() : base("Default")
        {

        }

        public OADbContext(string conStr) : base(conStr)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
