﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Data.Interface
{
    // make sure that all IScriptable also implement ITextObject. 
    public interface IScriptable
    {
        // the k.request.paraname; 
        List<string> RequestParas { get; set; }
    }
}
