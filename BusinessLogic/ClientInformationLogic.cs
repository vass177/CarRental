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
        private readonly RentalDataHandler rentalDBHandler;
        private readonly UserDataHandler userDBHandler;
        private readonly RentalServiceJoinDataHandler rentalJoinDBHandler;

        public event EventHandler ClientListChanged;

        public event EventHandler ClientLoggedIn;

        public ClientInformationLogic()
        {
            this.clientDBHandler = new ClientDataHandler();
            this.rentalDBHandler = new RentalDataHandler();
            this.userDBHandler = new UserDataHandler();
            this.rentalJoinDBHandler = new RentalServiceJoinDataHandler();
        }

        private void OnLogIn()
        {
            this.ClientLoggedIn?.Invoke(this, EventArgs.Empty);
        }

        private void OnClientListChanged()
        {
            this.ClientListChanged?.Invoke(this, EventArgs.Empty);
        }

        public IList<Client> GetAllClientList()
        {
            var clients = this.clientDBHandler.GetAll();

            return ((IQueryable<Client>)clients).ToList();
        }

        public void DeleteClient(Client selectedClient)
        {
            IQueryable<Rental> clientRentals =  (IQueryable<Rental>) this.rentalDBHandler.SelectMore(RentalAttributeType.UserName, selectedClient.UserName);
            if(clientRentals!=null)
                this.DeleteClientOrders(clientRentals);

            User deletableUser = (User)this.userDBHandler.Select(UserAttributeType.UserName, selectedClient.UserName);
            this.clientDBHandler.Delete(selectedClient);
            this.userDBHandler.Delete(deletableUser);


            this.OnClientListChanged();
        }

        public void DeleteClientOrders(IQueryable<Rental> rentals)
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

        public void UpdateClient()
        {
            this.clientDBHandler.Update();

            this.OnClientListChanged();
        }

        public Client GetLoggedInClient(string userName)
        {
            Client loggedIn=(Client)this.clientDBHandler.Select(ClientAttributeType.UserName, userName);
            return loggedIn;
        }

    }
}
