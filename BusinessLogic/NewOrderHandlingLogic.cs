// <copyright file="NewOrderHandlingLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data;
    using Data.DataHandling;

    public class NewOrderHandlingLogic
    {
        private readonly RentalDataHandler rentalDBHandler;
        private readonly ServiceDataHandler serviceDBHandler;
        private readonly CarDataHandler carDBHandler;

        private Client client;
        private Car selectedCar;
        private bool carAvailable;
        private List<Service> servList;
        private DateTime[] dateRange;
        private int carPrice;
        private int clientDiscount;
        private int finalPrice;

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

        public int ClientDiscount
        {
            get { return this.clientDiscount; }
        }

        public int FinalPrice
        {
            get { return this.finalPrice; }
        }

        public int CarPrice
        {
            get
            {
                return this.carPrice;
            }
        }

        public List<int> ServPriceList { get; private set; }

        public void SelectCar(string imageSource)
        {
            IQueryable<Car> selectedCarList = (IQueryable<Car>)this.carDBHandler.SelectMore(CarAttributeType.CarImageSource, imageSource);
            if (selectedCarList.Count() == 0)
            {
                this.carAvailable = false;
            }
            else
            {
                this.selectedCar = selectedCarList.First();
            }
        }

        public bool CheckCarAvailibility(DateTime startDate, DateTime endDate)
        {
            this.dateRange = new DateTime[] { startDate, endDate };
            IQueryable<Rental> rentals = (IQueryable<Rental>)this.rentalDBHandler.SelectMore(RentalAttributeType.RentalDateInterval, this.dateRange);

            foreach (var order in rentals)
            {
                Console.WriteLine(order.Car.CarType);
                Console.WriteLine(order.Client.ClientName);
                if (order.Car.CarID == this.selectedCar.CarID)
                {
                    return false;
                }
            }

            return true;
        }

        public List<Service> SearchSelectedServices(List<string> serviceList)
        {
            this.servList = new List<Service>();

            foreach (string service in serviceList)
            {
                Service selectedService = (Service)this.serviceDBHandler.Select(ServiceAttributeType.ServiceName, service);
                this.servList.Add(selectedService);
            }

            this.CalculateServicePrice();

            return this.servList;
        }

        public decimal CalculateServicePrice()
        {
            int dayCount = this.CalculateDays();
            this.ServPriceList = new List<int>();

            decimal serviceprice = 0;
            foreach (Service s in this.servList)
            {
                serviceprice += s.ServicePrice * dayCount;
                this.ServPriceList.Add((int)s.ServicePrice * dayCount);
            }

            return serviceprice;
        }

        public int CalculateDays()
        {
            TimeSpan days = this.dateRange[1] - this.dateRange[0];
            int dayCount = days.Days;

            this.carPrice = (int)this.SelectedCar.CarRentalPrice * dayCount;

            return dayCount;
        }

        public void CalculateFinalPrice()
        {
            int price = (int)(this.carPrice + this.CalculateServicePrice());
            this.clientDiscount = (int)(price * this.client.ClientDiscountStatus / 100);

            this.finalPrice = price - this.clientDiscount;
        }

        public void FinishOrder()
        {
            Rental newRental = new Rental
            {
                UserName = this.client.UserName,
                CarID = this.SelectedCar.CarID,
                RentalStartDate = this.dateRange[0],
                RentalEndDate = this.dateRange[1],
                RentalFullPrice = this.finalPrice
            };
            this.rentalDBHandler.Insert(newRental);
        }
    }
}
