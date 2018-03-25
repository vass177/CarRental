// <copyright file="CarDataHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Data.DataHandling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Exceptions;

    public enum CarAttributeType
    {
        CarType,
        CarHorsePower,
        CarCapacity,
        CarRentalPrice,
        CarCategory
    }

    public class CarDataHandler : IDataBase
    {
        private RentalDBEntities database;

        public CarDataHandler()
        {
            this.database = new RentalDBEntities();
        }

        public void Delete(object deletableItem)
        {
            this.database.Cars.Remove((Car)deletableItem);
            this.database.SaveChanges();
        }

        public object GetAll()
        {
            return database.Cars;
        }

        public void Insert(object newItem)
        {
            this.database.Cars.Add((Car)newItem);
            this.database.SaveChanges();
        }

        public object Select(object attributeType, object attributeValue)
        {
            // CarType will be unique, only one car will exist with a specific CarType
            if (!((CarAttributeType)attributeType == CarAttributeType.CarType))
            {
                throw new InvalidSearchTypeException("CarAttributeType");
            }

            try
            {
                return this.database.Cars.Single(x => x.CarType == (string)attributeValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new EntryNotFoundException("Car");
            }
        }

        public object SelectMore(object attributeType, object attributeValue)
        {
            CarAttributeType attribute = (CarAttributeType)attributeType;

            // for CarHorsePower, attributvalue will be a decimal[] array
            decimal[] hpRange = new decimal[2];
            if (attribute == CarAttributeType.CarHorsePower)
            {
                hpRange = (decimal[])attributeValue;
            }

            // for CarCapacity, attributvalue will be a decimal[] array
            decimal[] capacityRange = new decimal[2];
            if (attribute == CarAttributeType.CarCapacity)
            {
                capacityRange = (decimal[])attributeValue;
            }

            // for CarRentalPrice, attributvalue will be a decimal[] array
            decimal[] priceRange = new decimal[2];
            if (attribute == CarAttributeType.CarRentalPrice)
            {
                priceRange = (decimal[])attributeValue;
            }

            switch (attribute)
            {
                case CarAttributeType.CarCategory:
                    return this.database.Cars.Where(x => x.CarCategory == (string)attributeValue);
                case CarAttributeType.CarRentalPrice:
                    return this.database.Cars.Where(x => (x.CarRentalPrice >= priceRange[0] && x.CarRentalPrice <= priceRange[1]));
                case CarAttributeType.CarHorsePower:
                    return this.database.Cars.Where(x => (x.CarHorsepower >= hpRange[0] && x.CarRentalPrice <= hpRange[1]));
                case CarAttributeType.CarCapacity:
                    return this.database.Cars.Where(x => (x.CarCapacity >= capacityRange[0] && x.CarCapacity <= capacityRange[1]));
                default:
                    return null;
            }
        }

        public void Update()
        {
            this.database.SaveChanges();
        }
    }
}
