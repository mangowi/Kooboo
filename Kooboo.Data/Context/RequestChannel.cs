﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Data.Context
{
  public enum  RequestChannel
    {
        Default = 0,
        InlineDesign = 8,
        Draft = 16,
        API =32 
    }
}
