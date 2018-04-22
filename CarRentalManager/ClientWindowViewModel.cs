using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Data;

namespace CarRentalManager
{
    public class ClientWindowViewModel : Bindable
    {
        private Client loggedInClient;
        private ClientInformationLogic clientLogic;
        private Car selectedCar;
        private NewOrderHandlingLogic orderHandling;        


        public Client LoggedInClient
        {
            get
            {
                return this.loggedInClient;
            }

            set
            {
                this.loggedInClient = value;
                this.OnPropertyChanged(nameof(this.LoggedInClient));
            }
        }

        public Car SelectedCar
        {
            get
            {
                return this.selectedCar;
            }

            set
            {
                this.selectedCar = value;
                this.OnPropertyChanged(nameof(this.SelectedCar));
            }
        }

        public ClientWindowViewModel(string loggedInUser)
        {
            this.clientLogic = new ClientInformationLogic();
            this.LoggedInClient = this.clientLogic.GetLoggedInClient(loggedInUser);

            this.orderHandling = new NewOrderHandlingLogic(this.loggedInClient);

            this.clientLogic.ClientLoggedIn += ClientLogic_ClientLoggedIn;
        }

        private void ClientLogic_ClientLoggedIn(object sender, EventArgs e)
        {
            this.RefreshClient();
        }

        public void RefreshClient()
        {
            this.OnPropertyChanged(nameof(this.LoggedInClient));
        }
        public void SelectACar(string imageSource)
        {
            this.orderHandling.SelectCar(imageSource);
            this.selectedCar = this.orderHandling.SelectedCar;
            OnPropertyChanged(nameof(this.SelectedCar));

        }

        public bool CheckDates(DateTime startDate, DateTime endDate)
        {
            return this.orderHandling.CheckCarAvailibility(startDate, endDate);
        }

        public void SelectService(List<string>serivceList)
        {
            this.orderHandling.SearchSelectedServices(serivceList);
        }
    }
}
