// <copyright file="ClientWindowViewModel.cs" company="PlaceholderCompany">
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

    public class ClientWindowViewModel : Bindable
    {
        private Client loggedInClient;
        private ClientInformationLogic clientLogic;
        private Car selectedCar;
        private NewOrderHandlingLogic orderHandling;
        private OrderHandling clientOrderHandling;

        private IList<Rental> rental;
        private List<Service> serviceList;
        private List<int> servicePriceList;
        private int carPrice;
        private int clientDiscount;
        private int totalPrice;

        private DateTime startDate;
        private DateTime endDate;

        public ClientWindowViewModel(string loggedInUser)
        {
            this.clientLogic = new ClientInformationLogic();
            this.LoggedInClient = this.clientLogic.GetLoggedInClient(loggedInUser);

            this.orderHandling = new NewOrderHandlingLogic(this.loggedInClient);
            this.clientOrderHandling = new OrderHandling(this.loggedInClient);
            this.RefreshOrderList();

            this.clientLogic.ClientLoggedIn += this.ClientLogic_ClientLoggedIn;
            this.clientOrderHandling.RentalListChanged += this.ClientOrderHandling_RentalListChanged;
        }

        public IList<Rental> Rental
        {
            get
            {
                return this.rental;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this.startDate;
            }

            set
            {
                this.startDate = value;
                this.OnPropertyChanged(nameof(this.StartDate));
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this.endDate;
            }

            set
            {
                this.endDate = value;
                this.OnPropertyChanged(nameof(this.EndDate));
            }
        }

        public int TotalPrice
        {
            get
            {
                return this.totalPrice;
            }

            set
            {
                this.totalPrice = value;
                this.OnPropertyChanged(nameof(this.TotalPrice));
            }
        }

        public int ClientDiscount
        {
            get
            {
                return this.clientDiscount;
            }

            set
            {
                this.clientDiscount = value;
                this.OnPropertyChanged(nameof(this.ClientDiscount));
            }
        }

        public int CarPrice
        {
            get
            {
                return this.carPrice;
            }

            set
            {
                this.carPrice = value;
                this.OnPropertyChanged(nameof(this.CarPrice));
            }
        }

        public List<Service> ServiceList
        {
            get
            {
                return this.serviceList;
            }

            set
            {
                this.serviceList = value;
                this.OnPropertyChanged(nameof(this.ServiceList));
            }
        }

        public List<int> ServicePriceList
        {
            get
            {
                return this.servicePriceList;
            }

            set
            {
                this.servicePriceList = value;
                this.OnPropertyChanged(nameof(this.ServicePriceList));
            }
        }

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

        /// <summary>
        /// Refreshing order list
        /// </summary>
        public void RefreshOrderList()
        {
            this.rental = this.clientOrderHandling.GetAllRentalList();

            this.OnPropertyChanged(nameof(this.Rental));
        }

        /// <summary>
        /// Refresh logged in client object
        /// </summary>
        public void RefreshClient()
        {
            this.OnPropertyChanged(nameof(this.LoggedInClient));
        }

        /// <summary>
        /// Select a car object by image source
        /// </summary>
        /// <param name="imageSource">Choosen image source</param>
        public void SelectACar(string imageSource)
        {
            this.orderHandling.SelectCar(imageSource);
            this.selectedCar = this.orderHandling.SelectedCar;
            this.OnPropertyChanged(nameof(this.SelectedCar));
        }

        /// <summary>
        /// Check if car is available between the two dates
        /// </summary>
        /// <param name="startDate">choosen starting date</param>
        /// <param name="endDate">choosen ending date</param>
        /// <returns>returns True, if car is available</returns>
        public bool CheckDates(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            return this.orderHandling.CheckCarAvailibility(startDate, endDate);
        }

        /// <summary>
        /// Fills a list with selected services and calculates the price
        /// </summary>
        /// <param name="serivceList">service list countaining service names in string</param>
        public void SelectService(List<string> serivceList)
        {
            this.ServiceList = this.orderHandling.SearchSelectedServices(serivceList);
            this.ServicePriceList = this.orderHandling.ServPriceList;
            this.orderHandling.CalculateFinalPrice();
            this.CarPrice = this.orderHandling.CarPrice;
            this.ClientDiscount = this.orderHandling.ClientDiscount;
            this.TotalPrice = this.orderHandling.FinalPrice;
        }

        /// <summary>
        /// Calling BL's finish order method to insert Rental into database
        /// </summary>
        public void FinishOrder()
        {
            this.orderHandling.FinishOrder();
            this.RefreshOrderList();
        }

        /// <summary>
        /// Gets client payments statistics
        /// </summary>
        /// <returns>return a list of payments by year</returns>
        public List<int> GetClientStatistics()
        {
            return this.clientOrderHandling.OrderRevenue(true);
        }

        /// <summary>
        /// Rental list changed event
        /// </summary>
        /// <param name="sender">....</param>
        /// <param name="e">...</param>
        private void ClientOrderHandling_RentalListChanged(object sender, EventArgs e)
        {
            this.RefreshOrderList();
        }

        /// <summary>
        /// Client log in event
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private void ClientLogic_ClientLoggedIn(object sender, EventArgs e)
        {
            this.RefreshClient();
        }
    }
}
