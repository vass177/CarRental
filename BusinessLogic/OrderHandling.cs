using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataHandling;
using Data;

namespace BusinessLogic
{
    public class OrderHandling
    {
        private readonly RentalDataHandler rentalDBHandler;
        private Client loggedInClient;

        public event EventHandler RentalListChanged;

        public OrderHandling(Client loggedInClient)
        {
            rentalDBHandler = new RentalDataHandler();
            this.loggedInClient = loggedInClient;
        }

        private void OnRentalListChanged()
        {
            RentalListChanged?.Invoke(this, EventArgs.Empty);
        }

        public IList<Rental> GetAllRentalList()
        {
            var rentals = rentalDBHandler.SelectMore(RentalAttributeType.UserName,loggedInClient.UserName);

            return ((IQueryable<Rental>)rentals).ToList();
        }
    }
}
