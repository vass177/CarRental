﻿// <copyright file="OrderHandling.cs" company="PlaceholderCompany">
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
    /// Class, that supports queries regarding Rental objects
    /// </summary>
    public class OrderHandling
    {
        private readonly RentalDataHandler rentalDBHandler;
        private readonly ServiceDataHandler serviceDBHandler;
        private readonly ClientDataHandler clientDBHandler;
        private readonly RentalServiceJoinDataHandler rentalJoinDBHandler;
        private Client loggedInClient;

        public OrderHandling(Client loggedInClient)
        {
            this.rentalDBHandler = new RentalDataHandler();
            this.serviceDBHandler = new ServiceDataHandler();
            this.clientDBHandler = new ClientDataHandler();
            this.rentalJoinDBHandler = new RentalServiceJoinDataHandler();
            this.loggedInClient = loggedInClient;
        }

        public event EventHandler RentalListChanged;

        /// <summary>
        /// Collects all the rentals, connecting to a client
        /// </summary>
        /// <returns>List, containing all the rentals for a client</returns>
        public IList<Rental> GetAllRentalList()
        {
            var rentals = this.rentalDBHandler.SelectMore(RentalAttributeType.UserName, this.loggedInClient.UserName);

            return ((IQueryable<Rental>)rentals).ToList();
        }

        /// <summary>
        /// Method is called for a Car object, and counts all Rentals connected to it
        /// </summary>
        /// <param name="car"> Car, that we are looking in the rentals </param>
        /// <returns>Number of rentals, in which the car is included</returns>
        public int NumberOfRental(Car car)
        {
            IQueryable<Rental> allRental = (IQueryable<Rental>)this.rentalDBHandler.GetAll();

            return allRental.Count(x => x.CarID == car.CarID);
        }

        /// <summary>
        /// Method is called for a Service object, and counts all Rentals connected to it
        /// </summary>
        /// <param name="service">Service, that we are looking in the rentals</param>
        /// <returns>Number of rentals, in which the service is included</returns>
        public int NumberOfServices(Service service)
        {
            IQueryable<RentalServiceJoin> allRental = (IQueryable<RentalServiceJoin>)this.rentalJoinDBHandler.GetAll();

            return allRental.Count(x => x.ServiceName == service.ServiceName);
        }

        /// <summary>
        /// Gets all the services, which can be ordered
        /// </summary>
        /// <returns>A list, that contains the services</returns>
        public IList<Service> GetAllServiceList()
        {
            var services = this.serviceDBHandler.GetAll();

            return ((IQueryable<Service>)services).ToList();
        }

        /// <summary>
        /// Calculates revenues for the company or payments for a client
        /// </summary>
        /// <param name="onlyForClient">The method has two functionality: one for a specific client (true) and one for the whole company (false)</param>
        /// <returns>A list that contains int values for 2014, 2015, 2016, 2017 and 2018 revenues</returns>
        public List<int> OrderRevenue(bool onlyForClient)
        {
            IQueryable<Rental> rentals;
            if (onlyForClient == true)
            {
                rentals = (IQueryable<Rental>)this.rentalDBHandler.SelectMore(RentalAttributeType.UserName, this.loggedInClient.UserName);
            }
            else
            {
                rentals = (IQueryable<Rental>)this.rentalDBHandler.GetAll();
            }

            var q2014 = rentals.Where(x => (x.RentalStartDate < new DateTime(2015, 1, 1) && x.RentalStartDate > new DateTime(2014, 1, 1)));
            int sum2014 = 0;
            if (q2014 != null)
            {
                foreach (var item in q2014)
                {
                    sum2014 += (int)item.RentalFullPrice;
                }
            }

            var q2015 = rentals.Where(x => (x.RentalStartDate < new DateTime(2016, 1, 1) && x.RentalStartDate > new DateTime(2015, 1, 1)));
            int sum2015 = 0;
            if (q2015 != null)
            {
                foreach (var item in q2015)
                {
                    sum2015 += (int)item.RentalFullPrice;
                }
            }

            var q2016 = rentals.Where(x => (x.RentalStartDate < new DateTime(2017, 1, 1) && x.RentalStartDate > new DateTime(2016, 1, 1)));
            int sum2016 = 0;
            if (q2016 != null)
            {
                foreach (var item in q2016)
                {
                    sum2016 += (int)item.RentalFullPrice;
                }
            }

            var q2017 = rentals.Where(x => (x.RentalStartDate < new DateTime(2018, 1, 1) && x.RentalStartDate > new DateTime(2017, 1, 1)));
            int sum2017 = 0;
            if (q2017 != null)
            {
                foreach (var item in q2017)
                {
                    sum2017 += (int)item.RentalFullPrice;
                }
            }

            var q2018 = rentals.Where(x => (x.RentalStartDate < new DateTime(2019, 1, 1) && x.RentalStartDate > new DateTime(2018, 1, 1)));
            int sum2018 = 0;
            foreach (var item in q2018)
            {
                sum2018 += (int)item.RentalFullPrice;
            }

            if (onlyForClient == true)
            {
                List<int> allRevenue = this.OrderRevenue(false);
                double discount = ((double)sum2018 / (double)allRevenue[4]) * 100;
                Client updatedClient = (Client) this.clientDBHandler.Select(ClientAttributeType.UserName, loggedInClient.UserName);
                updatedClient.ClientDiscountStatus = (int)discount;
                this.loggedInClient.ClientDiscountStatus = (int)discount;
                this.clientDBHandler.Update();
            }

            return new List<int> { sum2014, sum2015, sum2016, sum2017, sum2018 };
        }

        /// <summary>
        /// Fires the rental list changed event
        /// </summary>
        private void OnRentalListChanged()
        {
            this.RentalListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
