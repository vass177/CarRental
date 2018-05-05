using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Test_BL.FakedTest.Fakes
{
    public class FakeOrderHandling : IOrderHandling
    {
        public IList<Rental> GetAllRentalList()
        {
            throw new NotImplementedException();
        }

        public IList<Service> GetAllServiceList()
        {
            throw new NotImplementedException();
        }

        public List<int> HowMuchCarID()
        {
            throw new NotImplementedException();
        }

        public int NumberOfRental(Car car)
        {
            throw new NotImplementedException();
        }

        public int NumberOfServices(Service service)
        {
            throw new NotImplementedException();
        }

        public List<int> OrderRevenue(bool onlyForClient)
        {
            throw new NotImplementedException();
        }
    }
}
