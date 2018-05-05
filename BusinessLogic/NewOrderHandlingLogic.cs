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

    /// <summary>
    /// Class, that handles the process of a new order
    /// </summary>
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

        /// <summary>
        /// Gets a Car object from the database by the selected image source
        /// </summary>
        /// <param name="imageSource">Image source of the selected car</param>
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

        /// <summary>
        /// Checks, whether the selected car is available between the selected dates
        /// </summary>
        /// <param name="startDate">Selected starting date</param>
        /// <param name="endDate">Selected ending date</param>
        /// <returns>True, if car is available, false if not</returns>
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

        /// <summary>
        /// Returns a list of the selected services
        /// </summary>
        /// <param name="serviceList">list, containing services names in string</param>
        /// <returns>List, containing the selected Service objects</returns>
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

        /// <summary>
        /// Calculates the price of the selected services for the interval
        /// </summary>
        /// <returns>returns the sum of the prices</returns>
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

        /// <summary>
        /// Calculates the days between the dates
        /// </summary>
        /// <returns>returns the day count</returns>
        public int CalculateDays()
        {
            TimeSpan days = this.dateRange[1] - this.dateRange[0];
            int dayCount = days.Days;

            this.carPrice = (int)this.SelectedCar.CarRentalPrice * dayCount;

            return dayCount;
        }

        /// <summary>
        /// Calculates the final price: sum of car prices and services price
        /// </summary>
        public void CalculateFinalPrice()
        {
            int price = (int)(this.carPrice + this.CalculateServicePrice());
            this.clientDiscount = (int)(price * this.client.ClientDiscountStatus / 100);

            this.finalPrice = price - this.clientDiscount;
        }

        /// <summary>
        /// Finishes the new order, by inserting it in the database
        /// </summary>
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
