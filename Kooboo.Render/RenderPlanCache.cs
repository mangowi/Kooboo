﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Sites;
using Kooboo.Sites.Render;

namespace Kooboo.Render
{
  
    public static class RenderPlanCache
    {
        private static object _locker = new object();

        private static Dictionary<Guid, List<IRenderTask>> RenderPlans = new Dictionary<Guid, List<IRenderTask>>(); 

        public static List<IRenderTask> GetOrAddRenderPlan(Guid UniqueObjectId, Func<List<IRenderTask>> EvaluatePlan)
        {
            lock (_locker)
            {  
                if (!RenderPlans.ContainsKey(UniqueObjectId))
                {
                    var plan = EvaluatePlan();
                    RenderPlans[UniqueObjectId] = plan;
                }
                return RenderPlans[UniqueObjectId];
            }
        }
        
    }
}
