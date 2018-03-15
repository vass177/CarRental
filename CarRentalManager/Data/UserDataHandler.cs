namespace CarRentalManager.Data
{
    using CarRentalManager.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum UserAttributeType
    {
        UserName,
        UserPassword,
        IsClient
    }

    public class UserDataHandler : IDataBase
    {
        private RentalDatabaseEntities database;

        public UserDataHandler()
        {
            this.database = new RentalDatabaseEntities();
        }

        public void Delete(object deletableItem)
        {
            this.database.Users.Remove((User)deletableItem);
            this.database.SaveChanges();
        }

        public void Insert(object newItem)
        {
            this.database.Users.Add((User)newItem);
            this.database.SaveChanges();
        }

        public object Select(object attributeType, object attributeValue)
        {
            UserAttributeType attribute = (UserAttributeType)attributeType;

            switch (attribute)
            {
                case UserAttributeType.UserName:
                    return this.database.Users.Single(x => x.UserName == (string)attributeValue);
                case UserAttributeType.UserPassword:
                    return this.database.Users.Single(x => x.UserPassword == (string)attributeValue);
                case UserAttributeType.IsClient:
                    return this.database.Users.Single(x => x.IsClient == (string)attributeValue);
                default:
                    throw new EntryNotFoundException("user");
            }
        }

        public void Update(object updatableItem)
        {
            throw new NotImplementedException();
        }
    }
}
