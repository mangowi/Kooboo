﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo
{
    public class OptionsService
    {
        private static Dictionary<Type, OptionsEntry> _options = new Dictionary<Type, OptionsEntry>();

        /// <summary>
        /// Get options
        /// </summary>
        public static T Get<T>()
        {
            var type = typeof(T);
            OptionsEntry entry;
            if (!_options.TryGetValue(type, out entry))
            {
                entry = new OptionsEntry
                {
                    Type = type
                };
                _options.Add(type, entry);
            }
            return (T)entry.Value;
        }

        /// <summary>
        /// Configure options
        /// </summary>
        /// <remarks>Execute only in application startup</remarks>
        public static void Configure<T>(Action<T> configure)
        {
            var type = typeof(T);
            OptionsEntry entry;
            if (!_options.TryGetValue(type, out entry))
            {
                entry = new OptionsEntry
                {
                    Type = type
                };
                _options.Add(type, entry);
            }
            entry.Configure.Add(o => configure((T)o));
        }
    }
}
