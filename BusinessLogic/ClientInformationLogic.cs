// <copyright file="ClientInformationLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data;
    using Data.DataHandling;
    using Interfaces;

    public class ClientInformationLogic : IClientInformationLogic
    {
        private readonly ClientDataHandler clientDBHandler;
        private readonly RentalDataHandler rentalDBHandler;
        private readonly UserDataHandler userDBHandler;
        private readonly RentalServiceJoinDataHandler rentalJoinDBHandler;

        public ClientInformationLogic()
        {
            this.clientDBHandler = new ClientDataHandler();
            this.rentalDBHandler = new RentalDataHandler();
            this.userDBHandler = new UserDataHandler();
            this.rentalJoinDBHandler = new RentalServiceJoinDataHandler();
        }

        public event EventHandler ClientListChanged;

        public event EventHandler ClientLoggedIn;

        /// <summary>
        /// Method, that gets all the clients in the database
        /// </summary>
        /// <returns>a List, containing all the clients</returns>
        public IList<Client> GetAllClientList()
        {
            var clients = this.clientDBHandler.GetAll();

            return ((IQueryable<Client>)clients).ToList();
        }

        /// <summary>
        /// Deletes a client with the connected user and rentals
        /// </summary>
        /// <param name="selectedClient">deletable client</param>
        public void DeleteClient(Client selectedClient)
        {
            IQueryable<Rental> clientRentals = (IQueryable<Rental>)this.rentalDBHandler.SelectMore(RentalAttributeType.UserName, selectedClient.UserName);
            if (clientRentals != null)
            {
                this.DeleteClientOrders(clientRentals);
            }

            User deletableUser = (User)this.userDBHandler.Select(UserAttributeType.UserName, selectedClient.UserName);
            this.clientDBHandler.Delete(selectedClient);
            this.userDBHandler.Delete(deletableUser);
            this.OnClientListChanged();
        }

        /// <summary>
        /// Deletes the rentals connected to a client
        /// </summary>
        /// <param name="rentals">List containing the rentals for a client</param>
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

        /// <summary>
        /// Updates the client database after modifications
        /// </summary>
        public void UpdateClient()
        {
            this.clientDBHandler.Update();

            this.OnClientListChanged();
        }

        /// <summary>
        /// Returns a client (who logged in) from database by the username
        /// </summary>
        /// <param name="userName">Username, who has logged in</param>
        /// <returns>Client object who logged in</returns>
        public Client GetLoggedInClient(string userName)
        {
            Client loggedIn = (Client)this.clientDBHandler.Select(ClientAttributeType.UserName, userName);
            return loggedIn;
        }

        /// <summary>
        /// Fires an event, that the clientlist has changed
        /// </summary>
        private void OnClientListChanged()
        {
            this.ClientListChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fires an event, when the login happens
        /// </summary>
        private void OnLogIn()
        {
            this.ClientLoggedIn?.Invoke(this, EventArgs.Empty);
        }
    }
}
