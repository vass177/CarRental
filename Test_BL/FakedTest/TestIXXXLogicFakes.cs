using Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_BL.FakedTest
{
    /// <summary>
    /// Test class for IXXXLogic implementor fakes 
    /// </summary>
    [TestFixture]
    public class TestIXXXLogicFakes
    {
        private static Random rnd = new Random();

        private List<Car> myCarDS;
        private List<Client> myClientDS;
        private List<Rental> myRentalDS;
        private List<Service> myServiceDS;
        private IList<Tuple<Rental, IList<Service>>> myRentalsWithServicesDS;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.myCarDS = new List<Car>();
            this.myClientDS = new List<Client>();
            this.myRentalDS = new List<Rental>();
            this.myServiceDS = new List<Service>();
            this.myRentalsWithServicesDS = new List<Tuple<Rental, IList<Service>>>();
            int maxFakeElements = 5;
            for (int id = 0; id < maxFakeElements; id++)
            {
                Car c = new Car();
                c.CarID = id;
                c.CarType = "CarName" + c.CarID;
                c.CarRentalPrice = id + 10;
                this.myCarDS.Add(c);
                Client cl = new Client();
                cl.UserName = "username" + id;
                this.myClientDS.Add(cl);
                Service s = new Service();
                s.ServiceName = "ServiceName" + id++;
                this.myServiceDS.Add(s);
            }

            for (int i = 0; i < maxFakeElements * 2; i++)
            {
                Rental r = new Rental();
                r.RentalID = i;
                r.UserName = this.myClientDS.ElementAt(rnd.Next(0, maxFakeElements)).UserName;
                r.CarID = this.myCarDS.ElementAt(rnd.Next(0, maxFakeElements)).CarID;
                int startMonth = rnd.Next(1, 12);
                int startDay = rnd.Next(1, 15);
                r.RentalStartDate =
                    new DateTime(2018, startMonth, startDay);
                r.RentalEndDate =
                    new DateTime(
                    2018,
                    startMonth + rnd.Next(0, 2),
                    startDay + rnd.Next(1, 14));
                this.myRentalDS.Add(r);
            }

        }
    }
}
