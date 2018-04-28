using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_BL.FakedTest.Fakes;

namespace Test_BL.FakedTest
{
    /// <summary>
    /// Testing IDataBase implementor FakeDatabase 
    /// </summary>
    [TestFixture]
    class Test_IDataBase_methods
    {
        static Random rnd = new Random();
        private List<FakeClass> myFakeDataSet { get; set; }
        private FakeDatabase<List<FakeClass>> myFDb;
        [OneTimeSetUp]
        void SetUp()
        {
            for (int i = 0; i < 5; i++)
            {
                FakeClass fc = new FakeClass();
                fc.Attribute1 = (FakeAttributeEnum)rnd.Next(0, 2);
                fc.Attribute2 = (FakeAttributeEnum)rnd.Next(0, 2);
                fc.Attribute3 = (FakeAttributeEnum)rnd.Next(0, 2);
                myFakeDataSet.Add(fc);
            }
            myFakeDatabase = new FakeDatabase<List<FakeClass>>((List)myFakeDataSet);
            
        } 
    }
}
