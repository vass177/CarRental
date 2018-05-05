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

    /// <summary>
    /// attribute to search in database
    /// </summary>
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

        /// <summary>
        /// Deletes a RentalServiceJoin object from db
        /// </summary>
        /// <param name="deletableItem">RentalServiceJoin object to be deleted</param>
        public void Delete(object deletableItem)
        {
            this.database.RentalServiceJoins.Remove((RentalServiceJoin)deletableItem);
            this.database.SaveChanges();
        }

        /// <summary>
        /// Gets all the entries from db
        /// </summary>
        /// <returns>List containing all the RentalServiceJoin objects</returns>
        public object GetAll()
        {
            return this.database.RentalServiceJoins;
        }

        /// <summary>
        /// Inserts new entry in db
        /// </summary>
        /// <param name="newItem">RentalServiceJoin object to be inserted</param>
        public void Insert(object newItem)
        {
            this.database.RentalServiceJoins.Add((RentalServiceJoin)newItem);
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
            // No single select available in this table
            throw new InvalidSearchTypeException("RentalServiceAttributeType");
        }

        /// <summary>
        /// Selects more item from db by a search attribute
        /// </summary>
        /// <param name="attributeType">attribute type for search</param>
        /// <param name="attributeValue">attribute value</param>
        /// <returns>list containing all the resulting objects</returns>
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

        /// <summary>
        /// Saves database changes
        /// </summary>
        public void Update()
        {
            this.database.SaveChanges();
        }
    }
}
