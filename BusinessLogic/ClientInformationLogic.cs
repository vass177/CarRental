﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataHandling;
using Data;

namespace BusinessLogic
{
    public class ClientInformationLogic
    {
        private readonly ClientDataHandler clientDBHandler;

        public event EventHandler ClientListChanged;

        public event EventHandler ClientLoggedIn;

        public ClientInformationLogic()
        {
            this.clientDBHandler = new ClientDataHandler();
        }

        private void OnLogIn()
        {
            ClientLoggedIn?.Invoke(this, EventArgs.Empty);
        }

        private void OnClientListChanged()
        {
            ClientListChanged?.Invoke(this, EventArgs.Empty);
        }
        
        public IList<Client> GetAllClientList()
        {
            var clients = clientDBHandler.GetAll();

            return ((IQueryable<Client>)clients).ToList();
        }

        public void DeleteClient(Client selectedClient)
        {
            clientDBHandler.Delete(selectedClient);

            OnClientListChanged();
        }

        public void UpdateClient()
        {
            clientDBHandler.Update();

            OnClientListChanged();
        }
        public Client GetLoggedInClient(string userName)
        {
            Client loggedIn=(Client)clientDBHandler.Select(ClientAttributeType.UserName, userName);
            return loggedIn;
        }
        

    }
}
