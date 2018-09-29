﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.IndexedDB.ByteConverter
{
 public  class DateTimeConverter : IByteConverter<DateTime>
    {
   
     public byte[] ToByte(DateTime input)
        {
            return BitConverter.GetBytes(input.ToInt64());
        }

        public DateTime FromByte(byte[] inputbytes)
        {
            Int64 dateint = BitConverter.ToInt64(inputbytes,0);
            return new DateTime().FromInt64(dateint);
        }
    }
}
