using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Events
{
    public class NullEventBus:IEventBus
    {
        /// <summary>
        /// Gets single instance of <see cref="NullEventBus"/> class.
        /// </summary>
        public static NullEventBus Instance { get { return SingletonInstance; } }
        private static readonly NullEventBus SingletonInstance = new NullEventBus();

        private NullEventBus()
        {
        }

       
    }
}
