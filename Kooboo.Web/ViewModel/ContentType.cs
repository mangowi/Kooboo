﻿using Kooboo.Sites.Contents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Web.ViewModel
{
    public class ContentTypeItemViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int PropertyCount { get; set; }

        public DateTime LastModified { get; set; }

       
    }

    public class  ContentTypeViewModel
    {  
        public Guid Id { get; set; }
         
        public string Name { get; set; }

        public bool IsNested { get; set; }

        public List<ContentProperty> Columns { get; set; } 
    }
     
}
