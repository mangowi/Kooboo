﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Data.Models
{
    public class Template : IGolbalObject
    {
        public Guid Id { get; set; }
        public long Version { get; set; }
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string OriginalIp { get; set; }
        public string Thumbnail { get; set; }
        public string Images { get; set; }
        public bool IsDelete { get; set; }
        public bool BinaryChange { get; set; } = true; 
    }
}
