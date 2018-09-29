﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Dom.CSS.Selector
{
    

    public class negativePseudoClassSelector : simpleSelector
    {

        public negativePseudoClassSelector()
        {
            base.Type = enumSimpleSelectorType.negativePseudoClass;
        }

        public string elementE { get; set; }

        public string InnerNotText { get; set; }

        public string matchText { get; set; }


        private simpleSelector _elementSelector;
        public simpleSelector ElementSelector
        {
            get
            {
                if (_elementSelector == null && !string.IsNullOrEmpty(elementE))
                {
                    _elementSelector = SelectorParser.parseOneSelector(elementE);
                }

                return _elementSelector;
            }
        }

        private simpleSelector _InnerNotSelector;

        public simpleSelector InnerNotSelector
        {
            get
            {
                if (_InnerNotSelector == null && !string.IsNullOrEmpty(matchText))
                {
                    _InnerNotSelector = SelectorParser.parseOneSelector(matchText);
                } 
                return _InnerNotSelector;
            }
        }

    }
}
