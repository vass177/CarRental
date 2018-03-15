﻿// <copyright file="CarDataHandler.cs" company="PlaceholderCompany">
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
        public void Delete(object deletableItem)
        {
            throw new NotImplementedException();
        }

        public void Insert(object newItem)
        {
            throw new NotImplementedException();
        }

        public object Select(object attributeType, object attributeValue)
        {
            throw new NotImplementedException();
        }

        public void Update(object updatableItem)
        {
            throw new NotImplementedException();
        }
    }
}
