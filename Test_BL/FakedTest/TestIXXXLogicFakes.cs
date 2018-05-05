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

        }
    }
}
