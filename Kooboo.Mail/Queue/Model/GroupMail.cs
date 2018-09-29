﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Mail.Queue.Model
{
   public class GroupMail
    {
        public string MessageContent { get; set; }

        public List<string> Members { get; set; }

        public string MailFrom { get; set; }
    }
}
