using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Auditing
{
    public interface IClientInfoProvider
    {
        string ClientInfo { get; }

        string ClientIpAddress { get; }

        string ClientName { get; }
    }
}
