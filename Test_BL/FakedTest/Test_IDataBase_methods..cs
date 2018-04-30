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
        static int id = 0;
        static Random rnd = new Random();
        private List<Data.Car> myCarDataSet;
        private FakeDatabase<Data.Car> myFCarDb;

        [OneTimeSetUp]
        public void SetUp()
        {
            for (int i = 0; i < 5; i++)
            {
                Data.Car c = new Data.Car();
                c.CarID
                fc.Attribute1 = (FakeAttributeEnum)rnd.Next(0, 2);
                fc.Attribute2 = (FakeAttributeEnum)rnd.Next(0, 2);
                fc.Attribute3 = (FakeAttributeEnum)rnd.Next(0, 2);
                myCarDataSet.Add(fc);
            }
            myFDb = new FakeDatabase<FakeClass>(myFakeDataSet);
        } 

        [Test]
        public 
    }
}
