﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Dom.CSS
{
    /// <summary>
    /// http://dev.w3.org/csswg/cssom/#the-csscharsetrule-interface
    /// </summary>
    [Serializable]
    public class CSSCharsetRule : CSSRule
    {

        public CSSCharsetRule()
        {
            base.type = enumCSSRuleType.CHARSET_RULE;
        }
        public string encoding { get; set; }
        
    }
}
