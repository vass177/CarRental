using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.DataHandling;

namespace BusinessLogic
{
    public class NewOrderHandlingLogic
    {
        private Client client;
        private Car selectedCar;
        private bool carAvailable;
        private readonly CarDataHandler carDBHandler;

        public NewOrderHandlingLogic(Client loggedInClient)
        {
            this.client = loggedInClient;
            this.carDBHandler = new CarDataHandler();
        }

        public void SelectCar(string imageSource)
        {
            IQueryable<Car> selectedCarList= (IQueryable<Car>)carDBHandler.SelectMore(CarAttributeType.CarImageSource, imageSource);
            if(selectedCarList.Count() == 0)
            {
                this.carAvailable = false;
            }
            else
            {
                //ez még nem végleges
                this.selectedCar = selectedCarList.First();
                Console.WriteLine(selectedCar.CarType);
            }

        }
    }
}
