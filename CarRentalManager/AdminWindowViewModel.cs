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
        private ClientInformationLogic clientListLogic;

        public IList<Client> Clients
        {
            get
            {
                return this.clients;
            }
        }

        public AdminWindowViewModel()
        {
            this.clientListLogic = new ClientInformationLogic();

            this.RefreshClientList();

            this.clientListLogic.ClientListChanged += this.ClientListLogic_ClientListChanged;
        }

        private void ClientListLogic_ClientListChanged(object sender, EventArgs e)
        {
            this.RefreshClientList();
        }

        private void RefreshClientList()
        {
            this.clients = this.clientListLogic.GetAllClientList();
        }
    }
}
