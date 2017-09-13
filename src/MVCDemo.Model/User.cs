using Dark.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo.Model
{
    public class User:EntityBase
    {
        public string Name { get; set; }
    }
}
