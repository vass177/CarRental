// <copyright file="CarDataHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CarRentalManager.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum CarAttributeType
    {
        CarType,
        CarPhotoPath,
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
        private RentalDatabaseEntities database;

        public CarDataHandler()
        {
            this.database = new RentalDatabaseEntities();
        }

        public void Delete(object deletableItem)
        {
            throw new NotImplementedException();
        }

        public void Insert(object newItem)
        {
            this.database.Cars.Add((Car)newItem);
            this.database.SaveChanges();
        }

        public object Select(object attributeType, object attributeValue)
        {
            CarAttributeType attribute = (CarAttributeType)attributeType;

            switch (attribute)
            {
                case CarAttributeType.CarType:
                    break;
                case CarAttributeType.CarPhotoPath:
                    break;
                case CarAttributeType.CarHorsePower:
                    break;
                case CarAttributeType.CarCapacity:
                    break;
                case CarAttributeType.CarMotorcode:
                    break;
                case CarAttributeType.CarRentalPrice:
                    break;
                case CarAttributeType.CarQuantity:
                    break;
                case CarAttributeType.CarCategory:
                    break;
                case CarAttributeType.CoordLat:
                    break;
                case CarAttributeType.CoordLong:
                    break;
                default:
                    break;
            }

            return null;
        }

        public object SelectMore(object attributeType, object attributeValue)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
