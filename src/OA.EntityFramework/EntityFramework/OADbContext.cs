using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.EntityFramework;

namespace OA.EntityFramework.EntityFramework
{
    public class OADbContext : BaseDbContext
    {
        public OADbContext() : base()
        {

        }

        public OADbContext(string conStr) : base(conStr)
        {

        }
      
    }
}
