using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface INewOrderHandlingLogic
    {
        Car SelectedCar { get; }

        int ClientDiscount { get; }

        int FinalPrice { get; }

        int CarPrice { get; }

        List<int> ServPriceList { get; set; }

        void SelectCar(string imageSource);

        bool CheckCarAvailibility(DateTime startDate, DateTime endDate);

        List<Service> SearchSelectedServices(List<string> serviceList);

        decimal CalculateServicePrice();

        int CalculateDays();

        void CalculateFinalPrice();

        void FinishOrder();


    }
}
