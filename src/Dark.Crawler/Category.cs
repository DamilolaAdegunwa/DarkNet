﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark.Crawler
{
    public class Category:BaseModel
    {

        public string Code { get; set; }

        public string ParentCode { get; set; }

        public int CategoryLevel { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int State { get; set; } = 0;

    }
}
