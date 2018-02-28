// <copyright file="LoginAuthentication.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CarRentalManager.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class LoginAuthentication
    {
        private string user;
        private string passwordHash;
        private List<string> userList;

        public LoginAuthentication()
        {
            this.userList = new List<string>();
        }

        public List<string> UserList
        {
            get { return this.userList; }
            set { this.userList = value; }
        }

        public bool CheckLoginCredentials(string user, string password)
        {
            if (this.UserExists(user))
            {
                string inputPassword = this.EncodePassword(password);
                Console.WriteLine(inputPassword);
                Console.WriteLine(this.passwordHash);
                if (inputPassword.Equals(this.passwordHash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool UserExists(string u)
        {
            this.user = u;

            // it will iterate through the database, and collect all the usernames
            if (this.userList.Contains(this.user))
            {
                // if user exists, we get passwordHash from the user database
                this.passwordHash = this.EncodePassword("jelszo");
                return true;
            }
            else
            {
                return false;
            }
        }

        private string EncodePassword(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA256").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}
