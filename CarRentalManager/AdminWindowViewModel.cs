// <copyright file="AdminWindowViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CarRentalManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data;
    using BusinessLogic;

    public class AdminWindowViewModel : Bindable
    {
        private IList<Client> clients;
        private Client selectedClient;
        private ClientInformationLogic clientListLogic;

        private IList<Car> cars;
        private Client selectedCar;
        private CarHandlingLogic carHandLogic;

        public IList<Client> Clients
        {
            get
            {
                return this.clients;
            }
        }

        public Client SelectedClient
        {
            get
            {
                return this.selectedClient;
            }

            set
            {
                this.selectedClient = value;
                this.OnPropertyChanged(nameof(this.SelectedClient));
            }
        }

        public IList<Car> Cars
        {
            get
            {
                return this.cars;
            }
        }

        public Client SelectedCar
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

        public AdminWindowViewModel()
        {
            this.clientListLogic = new ClientInformationLogic();
            this.carHandLogic = new CarHandlingLogic();

            this.RefreshClientList();
            this.RefreshCarList();

            this.clientListLogic.ClientListChanged += this.ClientListLogic_ClientListChanged;
            this.carHandLogic.CarListChanged += this.CarHandLogic_CarListChanged;
        }

        public void DeleteClient()
        {
            if (this.selectedClient != null)
            {
                this.clientListLogic.DeleteClient(this.selectedClient);

                this.RefreshClientList();
            }
        }

        public void UpdateClient()
        {
            if (this.selectedClient != null)
            {
                this.clientListLogic.UpdateClient();

                this.RefreshClientList();
            }
        }

        private void ClientListLogic_ClientListChanged(object sender, EventArgs e)
        {
            this.RefreshClientList();
        }

        private void RefreshClientList()
        {
            this.clients = this.clientListLogic.GetAllClientList();

            this.OnPropertyChanged(nameof(this.Clients));
        }

        private void RefreshCarList()
        {
            this.cars = this.carHandLogic.GetAllCarList();

            this.OnPropertyChanged(nameof(this.Cars));
        }

        private void CarHandLogic_CarListChanged(object sender, EventArgs e)
        {
            this.RefreshCarList();
        }
    }
}
