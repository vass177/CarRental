﻿// <copyright file="CarDataHandler.cs" company="PlaceholderCompany">
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

    /// <summary>
    /// attribute to search in database
    /// </summary>
    public enum CarAttributeType
    {
        CarType,
        CarHorsePower,
        CarCapacity,
        CarRentalPrice,
        CarCategory,
        CarImageSource
    }

    public class CarDataHandler : IDataBase
    {
        private RentalDBEntities database;

        public CarDataHandler()
        {
            this.database = new RentalDBEntities();
        }

        public CarDataHandler(object database)
        {

        }

        /// <summary>
        /// Deletes a Car object from db
        /// </summary>
        /// <param name="deletableItem">Car object to be deleted</param>
        public void Delete(object deletableItem)
        {
            this.database.Cars.Remove((Car)deletableItem);
            this.database.SaveChanges();
        }

        /// <summary>
        /// Gets all the entries from db
        /// </summary>
        /// <returns>List containing all the Car objects</returns>
        public object GetAll()
        {
            return this.database.Cars;
        }

        /// <summary>
        /// Inserts new entry in db
        /// </summary>
        /// <param name="newItem">Car object to be inserted</param>
        public void Insert(object newItem)
        {
            this.database.Cars.Add((Car)newItem);
            this.database.SaveChanges();
        }

        /// <summary>
        /// Selects item from db by unique attribute type
        /// </summary>
        /// <param name="attributeType">attribute type for search</param>
        /// <param name="attributeValue">attribute value</param>
        /// <returns>single item</returns>
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

        /// <summary>
        /// Selects more item from db by a search attribute
        /// </summary>
        /// <param name="attributeType">attribute type for search</param>
        /// <param name="attributeValue">attribute value</param>
        /// <returns>list containing all the resulting objects</returns>
        public object SelectMore(object attributeType, object attributeValue)
        {
            CarAttributeType attribute = (CarAttributeType)attributeType;

            // for CarHorsePower, attributvalue will be a decimal[] array
            decimal hpRange1 = 0;
            decimal hpRange2 = 0;
            if (attribute == CarAttributeType.CarHorsePower)
            {
                hpRange1 = ((decimal[])attributeValue)[0];
                hpRange2 = ((decimal[])attributeValue)[1];
            }

            // for CarCapacity, attributvalue will be a decimal[] array
            decimal capacityRange1 = 0;
            decimal capacityRange2 = 0;
            if (attribute == CarAttributeType.CarCapacity)
            {
                capacityRange1 = ((decimal[])attributeValue)[0];
                capacityRange2 = ((decimal[])attributeValue)[1];
            }

            // for CarRentalPrice, attributvalue will be a decimal[] array
            decimal priceRange1 = 0;
            decimal priceRange2 = 0;
            if (attribute == CarAttributeType.CarRentalPrice)
            {
                priceRange1 = ((decimal[])attributeValue)[0];
                priceRange2 = ((decimal[])attributeValue)[1];
            }

            switch (attribute)
            {
                case CarAttributeType.CarCategory:
                    return this.database.Cars.Where(x => x.CarCategory == (string)attributeValue);
                case CarAttributeType.CarRentalPrice:
                    return this.database.Cars.Where(x => (x.CarRentalPrice >= priceRange1 && x.CarRentalPrice <= priceRange2));
                case CarAttributeType.CarHorsePower:
                    return this.database.Cars.Where(x => (x.CarHorsepower >= hpRange1 && x.CarHorsepower <= hpRange2));
                case CarAttributeType.CarCapacity:
                    return this.database.Cars.Where(x => (x.CarCapacity >= capacityRange1 && x.CarCapacity <= capacityRange2));
                case CarAttributeType.CarImageSource:
                    return this.database.Cars.Where(x => x.CarPhotoPath == (string)attributeValue);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Saves database changes
        /// </summary>
        public void Update()
        {
            this.database.SaveChanges();
        }
    }
}
