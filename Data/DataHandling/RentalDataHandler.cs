// <copyright file="RentalDataHandler.cs" company="PlaceholderCompany">
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
        public void Delete(object deletableItem)
        {
            this.database.Rentals.Remove((Rental)deletableItem);
            this.database.SaveChanges();
        }

        public object GetAll()
        {
            return this.database.Rentals;
        }

        public void Insert(object newItem)
        {
            this.database.Rentals.Add((Rental)newItem);
            this.database.SaveChanges();
        }

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

        public object SelectMore(object attributeType, object attributeValue)
        {
            RentalAttributeType attribute = (RentalAttributeType)attributeType;

            // for RentalDate, attributvalue will be a Date[] array
            DateTime[] dateRange = new DateTime[2];
            if (attribute == RentalAttributeType.RentalDateInterval)
            {
                dateRange = (DateTime[])attributeValue;
            }

            // for RentalPrice, attributvalue will be a decimal[] array
            decimal[] priceRange = new decimal[2];
            if (attribute == RentalAttributeType.RentalPriceInterval)
            {
                priceRange = (decimal[])attributeValue;
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
                    return this.database.Rentals.Where(x => (x.RentalStartDate >= dateRange[0] && x.RentalStartDate <= dateRange[1]) || (x.RentalEndDate >= dateRange[0] && x.RentalEndDate <= dateRange[1]));
                case RentalAttributeType.RentalPriceInterval:
                    return this.database.Rentals.Where(x => (x.RentalFullPrice >= priceRange[0] && x.RentalFullPrice <= priceRange[1]));
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
