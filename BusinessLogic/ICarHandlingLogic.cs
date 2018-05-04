﻿using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    /// <summary>
    /// Interface for basic data handling operations
    /// </summary>
    public interface ICarHandlingLogic
    {
        IList<Car> GetAllCarList();
        void DeleteCar(Car selectedCar);
        void UpdateCar();
        void AddNewCar(Car newCar, string photoPath);
    }
}
