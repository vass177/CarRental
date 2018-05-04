namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data;
    using Data.DataHandling;

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

        public IList<Car> GetAllCarList()
        {
            var cars = this.carDBHandler.GetAll();

            return ((IQueryable<Car>)cars).ToList();
        }

        public void DeleteCar(Car selectedCar)
        {
            IQueryable<Rental> carRentals = (IQueryable<Rental>)this.rentalDBHandler.SelectMore(RentalAttributeType.CarID, selectedCar.CarID);
            this.DeleteCarOrders(carRentals);

            this.carDBHandler.Delete(selectedCar);
            this.OnCarListChanged();
        }

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

        public void UpdateCar()
        {
            this.carDBHandler.Update();

            this.OnCarListChanged();
        }

        public void AddNewCar(Car newCar, string photoPath)
        {
            newCar.CarPhotoPath = photoPath;

            // ez még átmeneti --> véglegesíteni
            newCar.CoordLat = 3;
            newCar.CoordLong = 4;

            this.carDBHandler.Insert(newCar);

            this.OnCarListChanged();
        }

        private void OnCarListChanged()
        {
            this.CarListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
