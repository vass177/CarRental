using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalManager
{
    class ClientWindowViewModel
    {
        private string loggedInUser;

        public ClientWindowViewModel(string loggedInUser)
        {
            this.loggedInUser = loggedInUser;
        }
    }
}
