﻿using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Test_BL.FakedTest.Fakes
{
    public class FakeNewOrderHandlingLogic : INewOrderHandlingLogic
    {
        private DateTime[] dateArray = new DateTime[2];

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
            this.VoidMethodCalls++;
        }

        public decimal CalculateServicePrice()
        {
            return 10;
        }

        public bool CheckCarAvailibility(DateTime startDate, DateTime endDate)
        {
            this.dateArray[0] = startDate;
            this.dateArray[1] = endDate;
            if ((startDate - endDate).TotalDays < 0)
            {
                return false;
            }

            return true;
        }

        public void FinishOrder()
        {
            throw new NotImplementedException();
        }

        public List<Service> SearchSelectedServices(List<string> serviceList)
        {
            throw new NotImplementedException();
        }

        public void SelectCar(string imageSource)
        {
            throw new NotImplementedException();
        }
    }
}