// <copyright file="CarHandlingLogic.cs" company="PlaceholderCompany">
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
    using Interfaces;

    /// <summary>
    /// Class, that contains all the methods connecting to car handling
    /// </summary>
    public class CarHandlingLogic : ICarHandlingLogic
    {
        private readonly CarDataHandler carDBHandler;
        private readonly RentalDataHandler rentalDBHandler;
        private readonly RentalServiceJoinDataHandler rentalJoinDBHandler;

        public CarHandlingLogic()
        {
            this.carDBHandler = new CarDataHandler();
            this.rentalDBHandler = new RentalDataHandler();
            this.rentalJoinDBHandler = new RentalServiceJoinDataHandler();
        }

        public event EventHandler CarListChanged;

        /// <summary>
        /// Collects all the cars, that could be rent
        /// </summary>
        /// <returns>a List containing all the Car objects</returns>
        public IList<Car> GetAllCarList()
        {
            var cars = this.carDBHandler.GetAll();

            return ((IQueryable<Car>)cars).ToList();
        }

        /// <summary>
        /// Method, called for deleting a Car from database
        /// </summary>
        /// <param name="selectedCar">Car object, that will be deleted</param>
        public void DeleteCar(Car selectedCar)
        {
            IQueryable<Rental> carRentals = (IQueryable<Rental>)this.rentalDBHandler.SelectMore(RentalAttributeType.CarID, selectedCar.CarID);
            this.DeleteCarOrders(carRentals);

            this.carDBHandler.Delete(selectedCar);
            this.OnCarListChanged();
        }

        /// <summary>
        /// Deletes all the connected Rentals to a Car object, before the Car will be deleted
        /// </summary>
        /// <param name="rentals">List, that contains all the rentals, in which the Car is included</param>
        public void DeleteCarOrders(IQueryable<Rental> rentals)
        {
            List<Rental> rentalList = rentals.ToList();
            for (int i = 0; i < rentalList.Count(); i++)
            {
                List<RentalServiceJoin> rentalJoins = ((IQueryable<RentalServiceJoin>)this.rentalJoinDBHandler.SelectMore(RentalServiceAttributeType.RentalID, rentalList[i].RentalID)).ToList();
                for (int j = 0; j < rentalJoins.Count(); j++)
                {
                    this.rentalJoinDBHandler.Delete(rentalJoins[j]);
                }

                this.rentalDBHandler.Delete(rentalList[i]);
            }
        }

        /// <summary>
        /// Calles the database savechange for Car DB and fires an event to notify GUI about the change in the list
        /// </summary>
        public void UpdateCar()
        {
            this.carDBHandler.Update();

            this.OnCarListChanged();
        }

        /// <summary>
        /// Its not really used, and not really working
        /// </summary>
        /// <param name="newCar">a Car...</param>
        /// <param name="photoPath">an ImageSource</param>
        public void AddNewCar(Car newCar, string photoPath)
        {
            Console.WriteLine("Easter egg..");
        }

        /// <summary>
        /// Method, that fires an event for car list changing
        /// </summary>
        private void OnCarListChanged()
        {
            this.CarListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
