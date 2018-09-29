﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Dom
{
    /// <summary>
    /// this flahs must match flags on WhatToShow. 
    /// </summary>
    [Flags]
    public enum enumNodeType
    {
       ELEMENT = 0x1,
       ATTRIBUTE = 0x2, // historical
       TEXT = 0x4,
       CDATA_SECTION = 0x8, // historical
       ENTITY_REFERENCE = 0x10, // historical
       ENTITY = 0x20,   // historical
       PROCESSING_INSTRUCTION = 0x40,
       COMMENT = 0x80,
       DOCUMENT = 0x100,
       DOCUMENT_TYPE = 0x200,
       DOCUMENT_FRAGMENT = 0x400,
       NOTATION = 0x800    // historical
    }
}
