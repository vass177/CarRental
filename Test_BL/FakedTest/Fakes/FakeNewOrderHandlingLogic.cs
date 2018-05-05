using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Test_BL.FakedTest.Fakes
{
    /// <summary>
    /// An INewOrderHandlingLogic implementor, contains simple logic only for test purposes 
    /// </summary>
    public class FakeNewOrderHandlingLogic : INewOrderHandlingLogic
    {
        private DateTime[] dateArray = new DateTime[2];
        private int servicePrice = 0;

        public IList<Service> Services { get; set; }

        public int CarPrice { get; private set; }

        public int ClientDiscount { get; private set; }

        public int FinalPrice { get; private set; }

        public Car SelectedCar { get; private set; }

        public List<int> ServPriceList { get; private set; }

        public int VoidMethodCalls { get; private set; } = 0;

        public int CalculateDays()
        {
            if (this.dateArray[0] != null && this.dateArray[1] != null)
            {
                return (int)(this.dateArray[1] - this.dateArray[0]).TotalDays;
            }

            return -1;
        }

        public void CalculateFinalPrice()
        {
            this.FinalPrice = this.CarPrice + this.servicePrice;
        }

        public decimal CalculateServicePrice()
        {
            return this.servicePrice;
        }

        public bool CheckCarAvailibility(DateTime startDate, DateTime endDate)
        {
            this.dateArray[0] = startDate;
            this.dateArray[1] = endDate;
            int days = (int)(startDate - endDate).TotalDays;
            if ((startDate - endDate).TotalDays < 0)
            {
                return false;
            }

            this.CarPrice = days * 10;
            return true;
        }

        public void FinishOrder()
        {
            this.CalculateFinalPrice();
        }

        public List<Service> SearchSelectedServices(List<string> serviceList)
        {
            this.servicePrice = 0;
            if (!(this.Services == null || serviceList == null))
            {
                List<Service> s = new List<Service>();
                foreach (string item in serviceList)
                {
                    s.AddRange(this.Services.Where(x => x.ServiceName == item));
                    this.servicePrice += 10;
                }

                return s;
            }

            return null;
        }

        public void SelectCar(string imageSource)
        {
            this.VoidMethodCalls++;
        }
    }
}
