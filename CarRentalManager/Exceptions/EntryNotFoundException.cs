// <copyright file="EntryNotFoundException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CarRentalManager.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EntryNotFoundException : Exception
    {
        public EntryNotFoundException(string message)
            : base(message)
        {
        }
    }
}
