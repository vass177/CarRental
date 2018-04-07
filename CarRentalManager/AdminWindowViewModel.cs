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

        public AdminWindowViewModel()
        {
            this.clientListLogic = new ClientInformationLogic();

            this.RefreshClientList();

            this.clientListLogic.ClientListChanged += this.ClientListLogic_ClientListChanged;
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
    }
}
