﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    public class EntryNotFoundException : Exception
    {
        public EntryNotFoundException(string message)
            : base(message)
        {
        }
    }
}
