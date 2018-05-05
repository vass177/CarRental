using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Test_BL.FakedTest.Fakes
{
    public class FakeClientInformationLogic : IClientInformationLogic
    {
        public IList<Client> Clients { get; set; }

        public IList<Rental> Rentals { get; set; }

        public Dictionary<Client, string> LoggedInClients { get; set; }

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
            throw new NotImplementedException();
        }

        public Client GetLoggedInClient(string userName)
        {
            throw new NotImplementedException();
        }

        public void UpdateClient()
        {
            throw new NotImplementedException();
        }
    }
}
