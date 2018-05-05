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

        public IList<Rental> Rental { get; set; }

        public void AddNewCar(Car newCar, string photoPath)
        {
            this.Cars.Add(newCar);
            this.CarsWithPhotoPaths.Add(newCar, photoPath);
        }

        public void DeleteCar(Car selectedCar)
        {
            
        }

        public void DeleteCarOrders(IQueryable<Rental> rentals)
        {
            
        }

        public IList<Car> GetAllCarList()
        {
            
        }

        public void UpdateCar()
        {
            
        }
    }
}
