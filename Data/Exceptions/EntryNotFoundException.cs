namespace Data.Exceptions
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
