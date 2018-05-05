// <copyright file="FakeOrderHandling.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Test_BL.FakedTest.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLogic.Interfaces;
    using Data;

    /// <summary>
    /// An IOrderHandling implementor with simple logic only for testing
    /// </summary>
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
            return this.Rentals.Where(x => x.CarID == car.CarID).Count();
        }

        public int NumberOfServices(Service service)
        {
            int db = 0;
            foreach (Tuple<Rental, IList<Service>> egyTuple in this.RentalsWithServices)
            {
                if (egyTuple.Item2.Contains(service))
                {
                    db++;
                }
            }

            return db;
        }

        public List<int> OrderRevenue(bool onlyForClient)
        {
            return onlyForClient ? new List<int> { 10, 11, 12, 13 } : null;
        }
    }
}
