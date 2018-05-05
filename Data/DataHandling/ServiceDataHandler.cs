// <copyright file="ServiceDataHandler.cs" company="PlaceholderCompany">
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
    public enum ServiceAttributeType
    {
        ServiceName,
        ServicePrice
    }

    public class ServiceDataHandler : IDataBase
    {
        private RentalDBEntities database;

        public ServiceDataHandler()
        {
            this.database = new RentalDBEntities();
        }

        /// <summary>
        /// Deletes a Service object from db
        /// </summary>
        /// <param name="deletableItem">Service object to be deleted</param>
        public void Delete(object deletableItem)
        {
            this.database.Services.Remove((Service)deletableItem);
            this.database.SaveChanges();
        }

        /// <summary>
        /// Inserts new entry in db
        /// </summary>
        /// <param name="newItem">Service object to be inserted</param>
        public void Insert(object newItem)
        {
            this.database.Services.Add((Service)newItem);
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
            // Service can only be selected by ServiceName
            if (!((ServiceAttributeType)attributeType == ServiceAttributeType.ServiceName))
            {
                throw new InvalidSearchTypeException("ServiceAttributeType");
            }

            try
            {
                return this.database.Services.Single(x => x.ServiceName == (string)attributeValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new EntryNotFoundException("Service");
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
            ServiceAttributeType attribute = (ServiceAttributeType)attributeType;

            // for servicePrice, attributvalue will be an decimal[] array
            decimal priceRange1 = 0;
            decimal priceRange2 = 0;
            if (attribute == ServiceAttributeType.ServicePrice)
            {
                priceRange1 = ((decimal[])attributeValue)[0];
                priceRange2 = ((decimal[])attributeValue)[1];
            }

            switch (attribute)
            {
                case ServiceAttributeType.ServiceName:
                    return this.database.Services.Where(x => x.ServiceName == (string)attributeValue);
                case ServiceAttributeType.ServicePrice:
                    return this.database.Services.Where(x => (x.ServicePrice >= priceRange1 && x.ServicePrice <= priceRange2));
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

        /// <summary>
        /// Gets all the entries from db
        /// </summary>
        /// <returns>List containing all the Service objects</returns>
        public object GetAll()
        {
            return this.database.Services;
        }
    }
}
