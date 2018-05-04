// <copyright file="InvalidSearchTypeException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Data.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InvalidSearchTypeException : Exception
    {
        public InvalidSearchTypeException(string message)
            : base(message)
        {
        }
    }
}
