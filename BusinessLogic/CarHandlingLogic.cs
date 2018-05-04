﻿namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.DataHandling;
    using Data;

    public class CarHandlingLogic
    {
        private readonly CarDataHandler carDBHandler;
        private readonly RentalDataHandler rentalDBHandler;

        public event EventHandler CarListChanged;

        public CarHandlingLogic()
        {
            this.carDBHandler = new CarDataHandler();
            this.rentalDBHandler = new RentalDataHandler();
        }
        private void OnCarListChanged()
        {
            CarListChanged?.Invoke(this, EventArgs.Empty);
        }

        public IList<Car> GetAllCarList()
        {
            var cars = carDBHandler.GetAll();

            return ((IQueryable<Car>)cars).ToList();
        }

        public void DeleteCar(Car selectedCar)
        {
            IQueryable<Rental> carRentals = (IQueryable<Rental>)rentalDBHandler.SelectMore(RentalAttributeType.CarID, selectedCar.CarID);
            DeleteCarOrders(carRentals);

            carDBHandler.Delete(selectedCar);
            OnCarListChanged();
        }

        public void DeleteCarOrders(IQueryable<Rental> rentals)
        {
            List<Rental> rentalList = rentals.ToList();
            for (int i = 0; i < rentalList.Count(); i++)
            {
                rentalDBHandler.Delete(rentalList[i]);
            }
        }

        public void UpdateCar()
        {
            carDBHandler.Update();

            OnCarListChanged();
        }

        public void AddNewCar(Car newCar, string photoPath)
        {
            newCar.CarPhotoPath = photoPath;

            // ez még átmeneti --> véglegesíteni
            newCar.CoordLat = 3;
            newCar.CoordLong = 4;

            carDBHandler.Insert(newCar);

            OnCarListChanged();
        }
    }
}
