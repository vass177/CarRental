using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IOrderHandling
    {
        IList<Rental> GetAllRentalList();

        List<int> HowMuchCarID();

        int NumberOfRental(Car car);

        int NumberOfServices(Service service);

        IList<Service> GetAllServiceList();

        List<int> OrderRevenue(bool onlyForClient);
    }
}
