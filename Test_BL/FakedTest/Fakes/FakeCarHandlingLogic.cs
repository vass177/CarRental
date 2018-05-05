using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Test_BL.FakedTest.Fakes
{
    /// <summary>
    /// ICarHandlingogic implementor for testing purposes
    /// </summary>
    public class FakeCarHandlingLogic : ICarHandlingLogic
    {
        public IList<Car> Cars { get; set; }

        public Dictionary<Car, string> CarsWithPhotoPaths { get; private set; }

        public IList<Rental> Rentals { get; set; }

        public int UpdatedTimes { get; private set; } = 0;

        public void AddNewCar(Car newCar, string photoPath)
        {
            this.Cars.Add(newCar);
            this.CarsWithPhotoPaths.Add(newCar, photoPath);
        }

        public void DeleteCar(Car selectedCar)
        {
            if (this.Cars.Contains(selectedCar))
            {
                this.Cars.Remove(selectedCar);
            }
        }

        public void DeleteCarOrders(IQueryable<Rental> rentals)
        {
            foreach (Rental item in rentals)
            {
                this.Rentals.Remove(item);
            }
        }

        public IList<Car> GetAllCarList()
        {
            return this.Cars;
        }

        public void UpdateCar()
        {
            this.UpdatedTimes++;
        }
    }
}
