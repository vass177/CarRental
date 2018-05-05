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
        public IList<Rental> Rentals { get; set; }

        public IList<Service> Services { get; set; }

        public IList<Tuple<Rental, IList<Service>>> RentalsWithServices { get; set; }

        public IList<Rental> GetAllRentalList()
        {
            return this.Rentals;
        }

        public IList<Service> GetAllServiceList()
        {
            return this.Services;
        }

        public List<int> HowMuchCarID()
        {
            return new List<int> { 0, 0 };
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
