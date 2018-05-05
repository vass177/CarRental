// <copyright file="UserClientRegistration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data;
    using Data.DataHandling;

    public class UserClientRegistration
    {
        private ClientDataHandler clientHandler;
        private UserDataHandler userHandler;
        private LoginAuthentication passwordCreator;

        public UserClientRegistration()
        {
            this.clientHandler = new ClientDataHandler();
            this.userHandler = new UserDataHandler();
            this.passwordCreator = new LoginAuthentication();
        }

        /// <summary>
        /// Add a new user to the database (also adds the client object, if the new user is a client and not admin)
        /// </summary>
        /// <param name="userName">input username</param>
        /// <param name="fullName">input name</param>
        /// <param name="password">raw password which will be encoded</param>
        /// <param name="address">input address</param>
        /// <param name="email">input email</param>
        /// <param name="isClient">Y/N value, whether the new user is a Client (Y)</param>
        public void AddNewUser(string userName, string fullName, string password, string address, string email, string isClient)
        {
            var user = new User
            {
                UserName = userName,
                UserPassword = this.passwordCreator.EncodePassword(password),
                IsClient = isClient
            };
            this.userHandler.Insert(user);

            if (isClient == "Y")
            {
                var client = new Client
                {
                    UserName = userName,
                    ClientName = fullName,
                    ClientAddress = address,
                    ClientEmail = email
                };
                this.clientHandler.Insert(client);
            }
        }
    }
}
