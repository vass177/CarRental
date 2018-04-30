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

        private List<Data.Client> myClientDataSet;
        private FakeDatabase<Data.Client> myFClientDb;

        private List<Data.Rental> myRentalDataSet;
        private FakeDatabase<Data.Rental> myFRentalDb;

        private List<Data.Service> myServiceDataSet;
        private FakeDatabase<Data.Service> myFServiceDb;

        [OneTimeSetUp]
        public void SetUp()
        {
            for (int i = 0; i < 5; i++)
            {
                Data.Car c = new Data.Car();
                c.CarID = id;
                c.CarType = "CarName" + c.CarID;
                myCarDataSet.Add(c);
                Data.Client cl = new Data.Client();
                cl.UserName = "username" + id;
                myClientDataSet.Add(cl);
                Data.Rental r = new Data.Rental();
                r.RentalID = id;
                r.UserName = "username" + r.RentalID;
                myRentalDataSet.Add(r);
                Data.Service s = new Data.Service();
                s.ServiceName = "Service_"+id++;
                myServiceDataSet.Add(s);
            }
            myFCarDb = new FakeDatabase<Data.Car>(myCarDataSet);
            myFClientDb = new FakeDatabase<Data.Client>(myClientDataSet);
            myFRentalDb = new FakeDatabase<Data.Rental>(myRentalDataSet);
            myFServiceDb = new FakeDatabase<Data.Service>(myServiceDataSet);
        }
        /// <summary>
        /// simple test  
        /// </summary>
        /// <param name="input">object list to be tested</param>
        /// <param name="excpected">object list returned</param>
        [Test]
        public void GetAll_method_test(object input, object excpected)
        {
            //ARRANGE+ACT+ASSERT
            Assert.That(this.myFCarDb.GetAll(), Is.EqualTo(myCarDataSet));
        }
    }
}
