﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kooboo.Mail.Imap.Commands.FetchCommand.CommandReader;

namespace Kooboo.Mail.Imap.Commands.FetchCommand.ResponseItem
{
    public class UID : ICommandResponse
    {
        public string Name
        {
            get
            {
                return "UID"; 
            } 
        }

        public List<ImapResponse> Render(MailDb maildb, FetchMessage message, DataItem dataItem)
        {
            return new List<ImapResponse>
            {
                new ImapResponse(dataItem.FullItemName + " " + message.Message.Id)
            };
        }
    }
}
