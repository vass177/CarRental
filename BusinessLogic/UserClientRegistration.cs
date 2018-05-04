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
