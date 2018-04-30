using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_BL.FakedTest.Fakes
{

    interface IFakeProperties<T>
    {
        IList<T> Objects { get; }
        IList<T> DeletedObjects { get; }
        IList<T> SelectedObjects { get; }
        IList<T> InsertedObjects { get; }
    }
}
