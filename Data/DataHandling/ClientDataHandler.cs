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

    public enum ClientAttributeType
    {
        UserName,
        ClientName,
        ClientAddress,
        ClientEmail,
        ClientDiscountStatus
    }

    public class ClientDataHandler : IDataBase
    {
        private RentalDBEntities database;

        public ClientDataHandler()
        {
            this.database = new RentalDBEntities();
        }

        public void Delete(object deletableItem)
        {
            this.database.Clients.Remove((Client)deletableItem);
            this.database.SaveChanges();
        }

        public void Insert(object newItem)
        {
            this.database.Clients.Add((Client)newItem);
            this.database.SaveChanges();
        }

        public object Select(object attributeType, object attributeValue)
        {
            //ClientAttributeType attribute = (ClientAttributeType)attributeType;

            //try
            //{
            //    switch (attribute)
            //    {
            //        case UserAttributeType.UserName:
            //            return this.database.Users.Single(x => x.UserName.Equals((string)attributeValue));
            //        case UserAttributeType.UserPassword:
            //            return this.database.Users.Single(x => x.UserPassword.Equals((string)attributeValue));
            //        case UserAttributeType.IsClient:
            //            return this.database.Users.Single(x => x.IsClient.Equals((string)attributeValue));
            //        default:
            //            return null;
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.StackTrace);
            //    throw new EntryNotFoundException("User");
            //}
            return null;
        }

        public object SelectMore(object attributeType, object attributeValue)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            this.database.SaveChanges();
        }
    }
}
