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
            DateTime[] dateRange = new DateTime[] { startDate, endDate };
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

        public void SearchSelectedServices(List<string>serviceList)
        {
            foreach (string service in serviceList)
            {
                Service selectedService= (Service)serviceDBHandler.Select(ServiceAttributeType.ServiceName, service);
                Console.WriteLine(selectedService.ServiceName +" "+selectedService.ServicePrice);
            }
        }
    }
}
