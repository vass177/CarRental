﻿// <copyright file="ServiceDataHandler.cs" company="PlaceholderCompany">
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
        
        public void Delete(object deletableItem)
        {
            this.database.Services.Remove((Service)deletableItem);
            this.database.SaveChanges();
        }

        public void Insert(object newItem)
        {
            this.database.Services.Add((Service)newItem);
            this.database.SaveChanges();
        }

        public object Select(object attributeType, object attributeValue)
        {
            // Service can only be selected by ServiceName
            if (!((ServiceAttributeType)attributeType==ServiceAttributeType.ServiceName))
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

        public object SelectMore(object attributeType, object attributeValue)
        {
            ServiceAttributeType attribute = (ServiceAttributeType)attributeType;

            // for servicePrice, attributvalue will be an int[] array
            int[] priceRange = new int[2];
            if (attribute==ServiceAttributeType.ServicePrice)
            {
                priceRange = (int[])attributeValue;
            }

            switch (attribute)
            {
                case ServiceAttributeType.ServiceName:
                    return this.database.Services.Where(x => x.ServiceName == (string)attributeValue);
                case ServiceAttributeType.ServicePrice:
                    return this.database.Services.Where(x => (x.ServicePrice >= priceRange[0] && x.ServicePrice <= priceRange[1]));
                default:
                    return null;
            }
        }

        public void Update()
        {
            this.database.SaveChanges();
        }

        public object GetAll()
        {
            return this.database.Services;
        }
    }
}
