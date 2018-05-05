using Data.DataHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_BL.FakedTest.Fakes
{
    /// <summary>
    /// For testing purposes only
    /// Simply comprises together interfaces IDataBase and IFakeProperties 
    /// without any meaningful logic or declaration
    /// </summary>
    /// <typeparam name="T">T: property type in IFakeProperties</typeparam>
    public interface IFakeDataBase<T> : IDataBase, IFakeProperties<T>
    {
    }
}
