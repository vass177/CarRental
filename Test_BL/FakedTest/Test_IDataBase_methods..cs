using Data.DataHandling;
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
            this.myCarDataSet = new List<Data.Car>();
            this.myClientDataSet = new List<Data.Client>();
            this.myRentalDataSet = new List<Data.Rental>();
            this.myServiceDataSet = new List<Data.Service>();
            for (int i = 0; i < 5; i++)
            {
                Data.Car c = new Data.Car();
                c.CarID = id;
                c.CarType = "CarName" + c.CarID;
                this.myCarDataSet.Add(c);
                Data.Client cl = new Data.Client();
                cl.UserName = "username" + id;
                this.myClientDataSet.Add(cl);
                Data.Rental r = new Data.Rental();
                r.RentalID = id;
                r.UserName = "username" + r.RentalID;
                this.myRentalDataSet.Add(r);
                Data.Service s = new Data.Service();
                s.ServiceName = "Service_"+id++;
                this.myServiceDataSet.Add(s);
            }
            myFCarDb = new FakeDatabase<Data.Car>(myCarDataSet);
            myFClientDb = new FakeDatabase<Data.Client>(myClientDataSet);
            myFRentalDb = new FakeDatabase<Data.Rental>(myRentalDataSet);
            myFServiceDb = new FakeDatabase<Data.Service>(myServiceDataSet);
        }
        public static IEnumerable<object[]> DummyDataSetTestCases
        {
            get
            {
                List<Data.Car> carDS = new List<Data.Car>();
                List<Data.Client> clientDS = new List<Data.Client>();
                List<Data.Rental> rentalDS = new List<Data.Rental();
                List<Data.Service> serviceDS = new List<Data.Service>();
                for (int i = 0; i < 3; i++)
                {
                    carDS.Add(new Data.Car
                    {
                        CarID = i,
                        CarType = "Cartype_"+i
                    });
                    clientDS.Add(new Data.Client
                    {
                        UserName = "username_"+i
                    });
                    rentalDS.Add(new Data.Rental
                    {
                        RentalID = i,
                        UserName = "username_" + i
                    });
                    serviceDS.Add(new Data.Service
                    {
                        ServiceName = "service_" + i++
                    });
                }
                FakeDatabase<Data.Car> carFDb = new FakeDatabase<Data.Car>(carDS);
                FakeDatabase<Data.Client> clientFDb = new FakeDatabase<Data.Client>(clientDS);
                FakeDatabase<Data.Rental> rentalFDb = new FakeDatabase<Data.Client>(rentalDS);
                FakeDatabase<Data.Service> serviceFDb = new FakeDatabase<Data.Client>(serviceDS);

                List<object[]> testCases = new List<object[]>();
                testCases.Add(new object[] { carFDb, carDS });
                testCases.Add(new object[] { clientFDb, clientDS });
                testCases.Add(new object[] { rentalFDb, rentalDS });
                testCases.Add(new object[] { serviceFDb, serviceDS });
                return testCases;
            }
        }
        /// <summary>
        /// simple method test for all kind of fake datasets  
        /// </summary>
        /// <param name="input">object list to be tested</param>
        /// <param name="excpected">object list returned</param>
        [TestCaseSource("DummyDataSetTestCases")]
        public void GetAll_method_test<T>(IDataBase input, List<T> expected)
        {
            //ARRANGE+ACT+ASSERT
            Assert.That(input.GetAll(), Is.EqualTo(expected));
        }

    }
}
