﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Dom.CSS.Tokens
{
  public  class hash_token : cssToken
    {

      public hash_token()
      {
          this.Type = enumTokenType.hash;
      }


      public string value;

      public enumHashFlag typeFlag;

    }
}
