// <copyright file="FakeClientInformationLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Test_BL.FakedTest.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLogic.Interfaces;
    using Data;

    /// <summary>
    /// An IClientInformationLogic implementor with no meaningful logic only for testing
    /// </summary>
    public class FakeClientInformationLogic : IClientInformationLogic
    {
        public IList<Client> Clients { get; set; }

        public IList<Rental> Rentals { get; set; }

        public Client LoggedInClient { get; private set; }

        public int UpdatedTimes { get; private set; } = 0;

        public void DeleteClient(Client selectedClient)
        {
            if (this.Clients.Contains(selectedClient))
            {
                this.Clients.Remove(selectedClient);
            }
        }

        public void DeleteClientOrders(IQueryable<Rental> rentals)
        {
            foreach (Rental item in rentals)
            {
                this.Rentals.Remove(item);
            }
        }

        public IList<Client> GetAllClientList()
        {
            return this.Clients;
        }

        public Client GetLoggedInClient(string userName)
        {
            foreach (Client item in this.Clients)
            {
                if (item.UserName.Equals(userName))
                {
                    return item;
                }
            }

            return null;
        }

        public void UpdateClient()
        {
            this.UpdatedTimes++;
        }
    }
}
