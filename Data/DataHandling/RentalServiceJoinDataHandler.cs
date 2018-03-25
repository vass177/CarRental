// <copyright file="RentalServiceJoinDataHandler.cs" company="PlaceholderCompany">
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

    public enum RentalServiceAttributeType
    {
        RentalID,
        ServiceName
    }

    public class RentalServiceJoinDataHandler : IDataBase
    {
        private RentalDBEntities database;
        public RentalServiceJoinDataHandler()
        {
            this.database = new RentalDBEntities();
        }
        public void Delete(object deletableItem)
        {
            this.database.RentalServiceJoins.Remove((RentalServiceJoin)deletableItem);
            this.database.SaveChanges();
        }

        public object GetAll()
        {
            return this.database.RentalServiceJoins;
        }

        public void Insert(object newItem)
        {
            this.database.RentalServiceJoins.Add((RentalServiceJoin)newItem);
            this.database.SaveChanges();
        }

        public object Select(object attributeType, object attributeValue)
        {
            // No single select available in this table

            throw new InvalidSearchTypeException("RentalServiceAttributeType");
        }

        public object SelectMore(object attributeType, object attributeValue)
        {
            RentalServiceAttributeType attribute = (RentalServiceAttributeType)attributeType;
            
            switch (attribute)
            {
                case RentalServiceAttributeType.RentalID:
                    return this.database.RentalServiceJoins.Where(x => x.RentalID == (int)attributeValue);
                case RentalServiceAttributeType.ServiceName:
                    return this.database.RentalServiceJoins.Where(x => x.ServiceName == (string)attributeValue);
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
