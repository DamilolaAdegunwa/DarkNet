using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Core.Application.Navigation
{
    public interface IHasMenuItems
    {
        IList<MenuItem> Items { get;  }
    }
}
