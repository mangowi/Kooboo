﻿using Kooboo.Sites.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Sites.TaskQueue.TaskExecutor
{
   public interface ITaskExecutor<T>  : IExecutor
    {  
        
    }

   public interface IExecutor
    {
        bool Execute(SiteDb SiteDb, string JsonModel);
    }
}
