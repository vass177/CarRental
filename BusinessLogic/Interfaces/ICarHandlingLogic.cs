using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICarHandlingLogic
    {
        IList<Car> GetAllCarList();

        void DeleteCar(Car selectedCar);

        void DeleteCarOrders(IQueryable<Rental> rentals);

        void UpdateCar();

        void AddNewCar(Car newCar, string photoPath);
    }
}
