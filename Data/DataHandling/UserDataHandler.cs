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

        public void Delete(object deletableItem)
        {
            this.database.Users.Remove((User)deletableItem);
            this.database.SaveChanges();
        }

        public object GetAll()
        {
            return database.Users;
        }

        public void Insert(object newItem)
        {
            this.database.Users.Add((User)newItem);
            this.database.SaveChanges();
        }

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

        public object SelectMore(object attributeType, object attributeValue)
        {
            UserAttributeType attribute = (UserAttributeType)attributeType;

            switch (attribute)
            {
                case UserAttributeType.UserName:
                    return this.database.Users.Where(x => x.UserName==(string)attributeValue);
                case UserAttributeType.IsClient:
                    return this.database.Users.Where(x => x.IsClient==(string)attributeValue);
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
