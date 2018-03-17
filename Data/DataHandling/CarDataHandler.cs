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
        CarMotorcode,
        CarRentalPrice,
        CarQuantity,
        CarCategory,
        CoordLat,
        CoordLong
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

        public void Insert(object newItem)
        {
            this.database.Cars.Add((Car)newItem);
            this.database.SaveChanges();
        }

        public object Select(object attributeType, object attributeValue)
        {
            CarAttributeType attribute = (CarAttributeType)attributeType;

            try
            {
                switch (attribute)
                {
                    case CarAttributeType.CarType:
                        return this.database.Cars.Single(x => x.CarType==(string)attributeValue);
                    case CarAttributeType.CarHorsePower:
                        return this.database.Cars.Single(x => x.CarHorsepower==(decimal)attributeValue);
                    case CarAttributeType.CarCapacity:
                        return this.database.Cars.Single(x => x.CarCapacity==(decimal)attributeValue);
                    case CarAttributeType.CarMotorcode:
                        return this.database.Cars.Single(x => x.CarMotorcode==(string)attributeValue);
                    case CarAttributeType.CarRentalPrice:
                        return this.database.Cars.Single(x => x.CarRentalPrice==(decimal)attributeValue);
                    case CarAttributeType.CarQuantity:
                        return this.database.Cars.Single(x => x.CarQuantity==(decimal)attributeValue);
                    case CarAttributeType.CarCategory:
                        return this.database.Cars.Single(x => x.CarCategory==(string)attributeValue);
                    case CarAttributeType.CoordLat:
                        return this.database.Cars.Single(x => x.CoordLat==(decimal)attributeValue);
                    case CarAttributeType.CoordLong:
                        return this.database.Cars.Single(x => x.CoordLong==(decimal)attributeValue);
                    default:
                        return null;
                }
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

            switch (attribute)
            {
                case CarAttributeType.CarType:
                    return this.database.Cars.Where(x => x.CarType.Equals((string)attributeValue));
                case CarAttributeType.CarHorsePower:
                    return this.database.Cars.Where(x => x.CarHorsepower.Equals((decimal)attributeValue));
                case CarAttributeType.CarCapacity:
                    return this.database.Cars.Where(x => x.CarCapacity.Equals((decimal)attributeValue));
                case CarAttributeType.CarMotorcode:
                    return this.database.Cars.Where(x => x.CarMotorcode.Equals((string)attributeValue));
                case CarAttributeType.CarRentalPrice:
                    return this.database.Cars.Where(x => x.CarRentalPrice.Equals((decimal)attributeValue));
                case CarAttributeType.CarQuantity:
                    return this.database.Cars.Where(x => x.CarQuantity.Equals((decimal)attributeValue));
                case CarAttributeType.CarCategory:
                    return this.database.Cars.Where(x => x.CarCategory.Equals((string)attributeValue));
                case CarAttributeType.CoordLat:
                    return this.database.Cars.Where(x => x.CoordLat.Equals((decimal)attributeValue));
                case CarAttributeType.CoordLong:
                    return this.database.Cars.Where(x => x.CoordLong.Equals((decimal)attributeValue));
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
