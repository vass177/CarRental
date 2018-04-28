using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_BL.FakedTest.Fakes
{
    /// <summary>
    /// Fake class only for testing FakeDatabase an IDataBase implementor
    /// </summary>
    internal class FakeClass
    {
        public FakeAttributeEnum Attribute1 { get; set; }
        public FakeAttributeEnum Attribute2 { get; set; }
    }
}
