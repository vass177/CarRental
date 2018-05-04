using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_BL.FakedTest.Fakes
{
    /// <summary>
    /// fake properties for IDataBase implementors
    /// only for testing purposes
    /// </summary>
    /// <typeparam name="T">represents a EntityFramework generated class</typeparam>
    public interface IFakeProperties<T>
    {
        IList<T> Objects { get; }

        IList<T> DeletedObjects { get; }

        IList<T> SelectedObjects { get; }

        IList<T> InsertedObjects { get; }
    }
}
