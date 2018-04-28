using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.DataHandling;

namespace BusinessLogic
{
    public class NewOrderHandlingLogic
    {
        private Client client;
        private Car selectedCar;
        private bool carAvailable;
        private List<Service> servList;
        private DateTime[] dateRange;

        private readonly CarDataHandler carDBHandler;
        private readonly RentalDataHandler rentalDBHandler;
        private readonly ServiceDataHandler serviceDBHandler;

        public NewOrderHandlingLogic(Client loggedInClient)
        {
            this.client = loggedInClient;
            this.carDBHandler = new CarDataHandler();
            this.rentalDBHandler = new RentalDataHandler();
            this.serviceDBHandler = new ServiceDataHandler();
        }

        public Car SelectedCar
        {
            get
            {
                return this.selectedCar;
            }
        }

        public List<int> ServPriceList { get; private set; }

        public void SelectCar(string imageSource)
        {
            IQueryable<Car> selectedCarList= (IQueryable<Car>)carDBHandler.SelectMore(CarAttributeType.CarImageSource, imageSource);
            if(selectedCarList.Count() == 0)
            {
                this.carAvailable = false;
            }
            else
            {
                //ez még nem végleges
                this.selectedCar = selectedCarList.First();
                Console.WriteLine(selectedCar.CarType);
            }
        }
        public bool CheckCarAvailibility(DateTime startDate, DateTime endDate)
        {
            dateRange = new DateTime[] { startDate, endDate };
            IQueryable<Rental> rentals= (IQueryable<Rental>)this.rentalDBHandler.SelectMore(RentalAttributeType.RentalDateInterval, dateRange);

            foreach (var item in rentals)
            {
                Console.WriteLine(item.UserName+"   "+item.RentalStartDate+ "   "+ item.RentalEndDate);
            }

            foreach (var order in rentals)
            {
                Console.WriteLine(order.Car.CarID);
                Console.WriteLine(selectedCar.CarID);
                if (order.Car.CarID==selectedCar.CarID)
                    return false;
            }
            return true;
        }

        public List<Service> SearchSelectedServices(List<string>serviceList)
        {
            servList = new List<Service>();
           
            foreach (string service in serviceList)
            {
                Service selectedService= (Service)serviceDBHandler.Select(ServiceAttributeType.ServiceName, service);
                Console.WriteLine(selectedService.ServiceName +" "+selectedService.ServicePrice);
                servList.Add(selectedService);
            }
            CalculateServicePrice();

            return servList;
        }
        public decimal CalculateServicePrice()
        {
            int dayCount = CalculateDays();
            ServPriceList = new List<int>();

            decimal serviceprice = 0;
            foreach (Service s in servList)
            {
                serviceprice += (s.ServicePrice) * dayCount;
                ServPriceList.Add((int)serviceprice);
                Console.WriteLine(s.ServiceName + ": " + s.ServicePrice + " (" + dayCount + ")");
            }
            return serviceprice;
        }
        public int CalculateDays()
        {
            TimeSpan days = dateRange[1] - dateRange[0];
            int dayCount = days.Days;
            Console.WriteLine("Napok száma: " + dayCount);

            return dayCount;
        }
        public void FinishOrder()
        {
            int dayCount = CalculateDays();

            // must include client discount !!!!!!!!!!!!!!!!!!!!!!
            decimal price = dayCount * SelectedCar.CarRentalPrice+CalculateServicePrice();

            Rental newRental = new Rental
            {
                UserName = client.UserName,
                CarID = SelectedCar.CarID,
                RentalStartDate = dateRange[0],
                RentalEndDate = dateRange[1],
                RentalFullPrice = price
            };
            rentalDBHandler.Insert(newRental);

        }
    }
}
