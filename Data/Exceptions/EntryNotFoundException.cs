﻿namespace Data.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// exception for not existing db entity
    /// </summary>
    public class EntryNotFoundException : Exception
    {
        public EntryNotFoundException(string message)
            : base(message)
        {
        }
    }
}
