using Dark.Core;
using Dark.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dark.Web
{
    public class BaseWebApplication<TStartModule> : HttpApplication
        where TStartModule : BaseModule
    {

        public static Bootstrap Bootstrap { get { return Bootstrap.Create<TStartModule>(); } }



        protected virtual void Application_Start(object sender, EventArgs e)
        {
          
            Bootstrap.Start();
        }

        protected virtual void Application_End(object sender, EventArgs e)
        {
            Bootstrap.Dispose();
        }
    }
}
