using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.EntityFramework
{
    public abstract class BaseDbContext : DbContext
    {
        public BaseDbContext() : base()
        {

        }

        public BaseDbContext(string strCon) : base(strCon)
        {

        }
    }
}
