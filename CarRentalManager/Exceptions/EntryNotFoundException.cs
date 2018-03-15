﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalManager.Exceptions
{
    public class EntryNotFoundException : Exception
    {
        public EntryNotFoundException(string message)
            : base(message)
        {
        }
    }
}
