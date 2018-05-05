// <copyright file="ClientDataHandler.cs" company="PlaceholderCompany">
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
    public enum ClientAttributeType
    {
        UserName,
        ClientName,
        ClientDiscountStatus
    }

    public class ClientDataHandler : IDataBase
    {
        private RentalDBEntities database;

        public ClientDataHandler()
        {
            this.database = new RentalDBEntities();
        }

        /// <summary>
        /// Deletes a Client object from db
        /// </summary>
        /// <param name="deletableItem">Client object to be deleted</param>
        public void Delete(object deletableItem)
        {
            this.database.Clients.Remove((Client)deletableItem);
            this.database.SaveChanges();
        }

        /// <summary>
        /// Gets all the entries from db
        /// </summary>
        /// <returns>List containing all the Client objects</returns>
        public object GetAll()
        {
            return this.database.Clients;
        }

        /// <summary>
        /// Inserts new entry in db
        /// </summary>
        /// <param name="newItem">Client object to be inserted</param>
        public void Insert(object newItem)
        {
            this.database.Clients.Add((Client)newItem);
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
            if (!((ClientAttributeType)attributeType == ClientAttributeType.UserName))
            {
                throw new InvalidSearchTypeException("ClientAttributeType");
            }

            try
            {
                return this.database.Clients.Single(x => x.UserName == (string)attributeValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new EntryNotFoundException("Client");
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
            ClientAttributeType attribute = (ClientAttributeType)attributeType;

            // for DiscountStatus, attributvalue will be a decimal[] array
            decimal discountRange1 = 0;
            decimal discountRange2 = 0;
            if (attribute == ClientAttributeType.ClientDiscountStatus)
            {
                discountRange1 = ((decimal[])attributeValue)[0];
                discountRange2 = ((decimal[])attributeValue)[1];
            }

            switch (attribute)
            {
                case ClientAttributeType.ClientName:
                    return this.database.Clients.Where(x => x.ClientName == (string)attributeValue);
                case ClientAttributeType.ClientDiscountStatus:
                    return this.database.Clients.Where(x => (x.ClientDiscountStatus >= discountRange1 && x.ClientDiscountStatus <= discountRange2));
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
