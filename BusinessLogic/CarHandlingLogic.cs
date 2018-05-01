namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.DataHandling;
    using Data;

    public class CarHandlingLogic : ICarHandlingLogic
    {
        private readonly CarDataHandler carDBHandler;

        public event EventHandler CarListChanged;

        public CarHandlingLogic()
        {
            this.carDBHandler = new CarDataHandler();
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
            carDBHandler.Delete(selectedCar);

            OnCarListChanged();
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
