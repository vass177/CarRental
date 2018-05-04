using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataHandling;
using Data;

namespace BusinessLogic
{
    public class OrderHandling
    {
        private readonly RentalDataHandler rentalDBHandler;
        private readonly ServiceDataHandler serviceDBHandler;
        private readonly RentalServiceJoinDataHandler rentalJoinDBHandler;
        private Client loggedInClient;

        public event EventHandler RentalListChanged;

        public OrderHandling(Client loggedInClient)
        {
            rentalDBHandler = new RentalDataHandler();
            serviceDBHandler = new ServiceDataHandler();
            rentalJoinDBHandler = new RentalServiceJoinDataHandler();
            this.loggedInClient = loggedInClient;
        }

        private void OnRentalListChanged()
        {
            RentalListChanged?.Invoke(this, EventArgs.Empty);
        }

        public IList<Rental> GetAllRentalList()
        {
            var rentals = rentalDBHandler.SelectMore(RentalAttributeType.UserName,loggedInClient.UserName);

            return ((IQueryable<Rental>)rentals).ToList();
        }

        public List<int> HowMuchCarID()
        {
            //var myquery=from carid in Car
            

            return new List<int> { 1, 2 };
        }

        public int NumberOfRental(Car car)
        {
            IQueryable<Rental> allRental = (IQueryable<Rental>)rentalDBHandler.GetAll();
            
            return allRental.Count(x => x.CarID == car.CarID);
        }

        public int NumberOfServices(Service service)
        {
            IQueryable<RentalServiceJoin> allRental = (IQueryable<RentalServiceJoin>)rentalJoinDBHandler.GetAll();

            return allRental.Count(x => x.ServiceName == service.ServiceName);
        }

        public IList<Service> GetAllServiceList()
        {
            var services = serviceDBHandler.GetAll();

            return ((IQueryable<Service>)services).ToList();
        }

        public List<int> OrderRevenue()
        {
            IQueryable<Rental> rentalsByClient = (IQueryable<Rental>)rentalDBHandler.SelectMore(RentalAttributeType.UserName, loggedInClient.UserName);
            var q2014 = rentalsByClient.Where(x => (x.RentalStartDate < new DateTime(2015, 1, 1) && x.RentalStartDate > new DateTime(2014, 1, 1)));
            int sum2014 = 0;
            if(q2014!=null)
            {
                foreach (var item in q2014)
                {
                    sum2014 += (int)item.RentalFullPrice;
                }
            }
            
            Console.WriteLine("ÖSSSSSZEGGGG 2014:" + sum2014);

            var q2015 = rentalsByClient.Where(x => (x.RentalStartDate < new DateTime(2016, 1, 1) && x.RentalStartDate > new DateTime(2015, 1, 1)));
            int sum2015 = 0;
            if (q2015 != null)
            {
                foreach (var item in q2015)
                {
                    sum2015 += (int)item.RentalFullPrice;
                }
            }
            var q2016 = rentalsByClient.Where(x => (x.RentalStartDate < new DateTime(2017, 1, 1) && x.RentalStartDate > new DateTime(2016, 1, 1)));
            int sum2016 = 0;
            if (q2016 != null)
            {
                foreach (var item in q2016)
                {
                    sum2016 += (int)item.RentalFullPrice;
                }
            }
            var q2017 = rentalsByClient.Where(x => (x.RentalStartDate < new DateTime(2018, 1, 1) && x.RentalStartDate > new DateTime(2017, 1, 1)));
            int sum2017 = 0;
            if (q2017 != null)
            {
                foreach (var item in q2017)
                {
                    sum2017 += (int)item.RentalFullPrice;
                }
            }

            var q2018 = rentalsByClient.Where(x => (x.RentalStartDate < new DateTime(2019, 1, 1) && x.RentalStartDate > new DateTime(2018, 1, 1)));
            int sum2018 = 0;
            foreach (var item in q2018)
            {
                sum2018 += (int)item.RentalFullPrice;
            }
            Console.WriteLine("ÖSSSSSZEGGGG:" +sum2016);

            return new List<int> { sum2014, sum2015, sum2016, sum2017, sum2018 };
        }

    }
}
