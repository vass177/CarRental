﻿// <copyright file="RentalDataHandler.cs" company="PlaceholderCompany">
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
    public enum RentalAttributeType
    {
        RentalID,
        UserName,
        CarID,
        RentalDateInterval,
        RentalPriceInterval
    }

    public class RentalDataHandler : IDataBase
    {
        private RentalDBEntities database;

        public RentalDataHandler()
        {
            this.database = new RentalDBEntities();
        }

        /// <summary>
        /// Deletes a Rental object from db
        /// </summary>
        /// <param name="deletableItem">Rental object to be deleted</param>
        public void Delete(object deletableItem)
        {
            this.database.Rentals.Remove((Rental)deletableItem);
            this.database.SaveChanges();
        }

        /// <summary>
        /// Gets all the entries from db
        /// </summary>
        /// <returns>List containing all the Rental objects</returns>
        public object GetAll()
        {
            return this.database.Rentals;
        }

        /// <summary>
        /// Inserts new entry in db
        /// </summary>
        /// <param name="newItem">Rental object to be inserted</param>
        public void Insert(object newItem)
        {
            this.database.Rentals.Add((Rental)newItem);
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
            if (!((RentalAttributeType)attributeType == RentalAttributeType.RentalID))
            {
                throw new InvalidSearchTypeException("RentalAttributeType");
            }

            try
            {
                return this.database.Rentals.Single(x => x.RentalID == (int)attributeValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new EntryNotFoundException("Rental");
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
            RentalAttributeType attribute = (RentalAttributeType)attributeType;

            // for RentalDate, attributvalue will be a Date[] array
            DateTime dateRange1 = default(DateTime);
            DateTime dateRange2 = default(DateTime);
            if (attribute == RentalAttributeType.RentalDateInterval)
            {
                dateRange1 = ((DateTime[])attributeValue)[0];
                dateRange2 = ((DateTime[])attributeValue)[1];
            }

            // for RentalPrice, attributvalue will be a decimal[] array
            decimal priceRange1 = 0;
            decimal priceRange2 = 0;
            if (attribute == RentalAttributeType.RentalPriceInterval)
            {
                priceRange1 = ((decimal[])attributeValue)[0];
                priceRange2 = ((decimal[])attributeValue)[1];
            }

            switch (attribute)
            {
                case RentalAttributeType.RentalID:
                    return this.database.Rentals.Where(x => x.RentalID == (int)attributeValue);
                case RentalAttributeType.UserName:
                    return this.database.Rentals.Where(x => x.UserName == (string)attributeValue);
                case RentalAttributeType.CarID:
                    return this.database.Rentals.Where(x => x.CarID == (int)attributeValue);
                case RentalAttributeType.RentalDateInterval:
                    return this.database.Rentals.Where(x => (x.RentalStartDate >= dateRange1 && x.RentalStartDate <= dateRange2) || (x.RentalEndDate >= dateRange1 && x.RentalEndDate <= dateRange2) || (x.RentalStartDate <= dateRange1 && x.RentalEndDate >= dateRange2) || (x.RentalStartDate >= dateRange1 && x.RentalEndDate <= dateRange2));
                case RentalAttributeType.RentalPriceInterval:
                    return this.database.Rentals.Where(x => (x.RentalFullPrice >= priceRange1 && x.RentalFullPrice <= priceRange2));
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
