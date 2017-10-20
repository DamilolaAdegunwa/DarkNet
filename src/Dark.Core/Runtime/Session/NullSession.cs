using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Runtime.Session
{
    public class NullSession : BaseSession
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullSession Instance { get; } = new NullSession();

        /// <inheritdoc/>
        public override long? UserId => null;

        public override string Account => throw new NotImplementedException();

        private NullSession()
        {

        }
    }
}
