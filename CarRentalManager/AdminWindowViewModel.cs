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
    using BusinessLogic;
    using Data;

    public class AdminWindowViewModel : Bindable
    {
        private OrderHandling adminOrderhandling;

        private IList<Client> clients;
        private Client selectedClient;
        private ClientInformationLogic clientListLogic;

        private IList<Car> cars;
        private Car selectedCar;
        private CarHandlingLogic carHandLogic;

        private int carSumma;

        public AdminWindowViewModel()
        {
            this.clientListLogic = new ClientInformationLogic();
            this.carHandLogic = new CarHandlingLogic();

            this.adminOrderhandling = new OrderHandling(null);

            this.RefreshClientList();
            this.RefreshCarList();

            this.clientListLogic.ClientListChanged += this.ClientListLogic_ClientListChanged;
            this.carHandLogic.CarListChanged += this.CarHandLogic_CarListChanged;
        }

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

        public int CarSumma
        {
            get
            {
                return this.carSumma;
            }

            set
            {
                this.carSumma = value;
                this.OnPropertyChanged(nameof(this.CarSumma));
            }
        }

        /// <summary>
        /// Returns the car utilization statitics in a dictionary (key: cartype as string, value: car count in rentals)
        /// </summary>
        /// <returns>Dictinary containing car types as key and car count as value</returns>
        public Dictionary<string, int> SummaCars()
        {
            Dictionary<string, int> tempCarList = new Dictionary<string, int>();
            IList<Car> getAllCars = this.carHandLogic.GetAllCarList();
            foreach (var item in getAllCars)
            {
                int i = 0;
                i = this.adminOrderhandling.NumberOfRental(item);
                tempCarList.Add(item.CarType, i);
            }

            return tempCarList;
        }

        /// <summary>
        /// Return car coordinates from the database
        /// </summary>
        /// <returns>List containing car coordinates</returns>
        public List<decimal> GetCarCoordinates()
        {
            List<decimal> carCoord = new List<decimal>();
            IList<Car> getAllCars = this.carHandLogic.GetAllCarList();
            foreach (var item in getAllCars)
            {
                carCoord.Add(item.CoordLat * 100);
                carCoord.Add(item.CoordLong * 100);
            }

            return carCoord;
        }

        /// <summary>
        /// Returns the service utilization statitics in a dictionary (key: serviceType as string, value: service count in rentals)
        /// </summary>
        /// <returns>Dictinary containing service names as key and service count as value</returns>
        public Dictionary<string, int> SummaServices()
        {
            Dictionary<string, int> tempServicesList = new Dictionary<string, int>();
            IList<Service> getAllServices = this.adminOrderhandling.GetAllServiceList();
            foreach (var item in getAllServices)
            {
                int i = 0;
                i = this.adminOrderhandling.NumberOfServices(item);
                tempServicesList.Add(item.ServiceName, i);
            }

            return tempServicesList;
        }

        /// <summary>
        /// return a list of income numbers (from 2014 to 2018)
        /// </summary>
        /// <returns>list of income statistics</returns>
        public List<int> GetIncomeStatistics()
        {
            return this.adminOrderhandling.OrderRevenue(false);
        }

        /// <summary>
        /// Deletes a client
        /// </summary>
        public void DeleteClient()
        {
            if (this.selectedClient != null)
            {
                this.clientListLogic.DeleteClient(this.selectedClient);

                this.RefreshClientList();
            }
        }

        /// <summary>
        /// Deletes a car
        /// </summary>
        public void DeleteCar()
        {
            if (this.selectedCar != null)
            {
                this.carHandLogic.DeleteCar(this.selectedCar);

                this.RefreshCarList();
            }
        }

        /// <summary>
        /// updates client list
        /// </summary>
        public void UpdateClient()
        {
            if (this.selectedClient != null)
            {
                this.clientListLogic.UpdateClient();

                this.RefreshClientList();
            }
        }

        /// <summary>
        /// updates car list
        /// </summary>
        public void UpdateCar()
        {
            if (this.selectedCar != null)
            {
                this.carHandLogic.UpdateCar();

                this.RefreshCarList();
            }
        }

        /// <summary>
        /// Refreshes client list
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private void ClientListLogic_ClientListChanged(object sender, EventArgs e)
        {
            this.RefreshClientList();
        }

        /// <summary>
        /// updates client list
        /// </summary>
        private void RefreshClientList()
        {
            this.clients = this.clientListLogic.GetAllClientList();

            this.OnPropertyChanged(nameof(this.Clients));
        }

        /// <summary>
        /// updates car list
        /// </summary>
        private void RefreshCarList()
        {
            this.cars = this.carHandLogic.GetAllCarList();

            this.OnPropertyChanged(nameof(this.Cars));
        }

        /// <summary>
        /// Refreshes car list
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private void CarHandLogic_CarListChanged(object sender, EventArgs e)
        {
            this.RefreshCarList();
        }
    }
}
