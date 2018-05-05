// <copyright file="UserDataHandler.cs" company="PlaceholderCompany">
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
    public enum UserAttributeType
    {
        UserName,
        IsClient
    }

    public class UserDataHandler : IDataBase
    {
        private RentalDBEntities database;

        public UserDataHandler()
        {
            this.database = new RentalDBEntities();
        }

        /// <summary>
        /// Deletes a User object from db
        /// </summary>
        /// <param name="deletableItem">User object to be deleted</param>
        public void Delete(object deletableItem)
        {
            this.database.Users.Remove((User)deletableItem);
            this.database.SaveChanges();
        }

        /// <summary>
        /// Gets all the entries from db
        /// </summary>
        /// <returns>List containing all the User objects</returns>
        public object GetAll()
        {
            return this.database.Users;
        }

        /// <summary>
        /// Inserts new entry in db
        /// </summary>
        /// <param name="newItem">User object to be inserted</param>
        public void Insert(object newItem)
        {
            this.database.Users.Add((User)newItem);
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
            if (!((UserAttributeType)attributeType == UserAttributeType.UserName))
            {
                throw new InvalidSearchTypeException("UserAttributeType");
            }

            try
            {
                return this.database.Users.Single(x => x.UserName == (string)attributeValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new EntryNotFoundException("User");
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
            UserAttributeType attribute = (UserAttributeType)attributeType;

            switch (attribute)
            {
                case UserAttributeType.UserName:
                    return this.database.Users.Where(x => x.UserName == (string)attributeValue);
                case UserAttributeType.IsClient:
                    return this.database.Users.Where(x => x.IsClient == (string)attributeValue);
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
