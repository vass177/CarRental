using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalManager.Interfaces
{
    public interface IAdminWindowViewModel
    {
        IList<Client> Clients { get; }

        Client SelectedClient { get; }

        IList<Car> Cars { get; }

        Car SelectedCar { get; set; }

        int CarSumma { get; set; }

        Dictionary<string, int> SummaCars();

        List<decimal> GetCarCoordinates();

        Dictionary<string, int> SummaServices();

        List<int> GetIncomeStatistics();

        void DeleteClient();

        void DeleteCar();

        void UpdateClient();
    }
}
