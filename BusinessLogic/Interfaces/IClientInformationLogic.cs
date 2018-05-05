using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IClientInformationLogic
    {
        IList<Client> GetAllClientList();

        void DeleteClient(Client selectedClient);

        void DeleteClientOrders(IQueryable<Rental> rentals);

        void UpdateClient();

        Client GetLoggedInClient(string userName);
     }
}
