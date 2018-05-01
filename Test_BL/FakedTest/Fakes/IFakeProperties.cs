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
    interface IFakeProperties<T>
    {
        IQueryable<T> Objects { get; }
        IQueryable<T> DeletedObjects { get; }
        IQueryable<T> SelectedObjects { get; }
        IQueryable<T> InsertedObjects { get; }
    }
}
