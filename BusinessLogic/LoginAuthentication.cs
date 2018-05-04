﻿// <copyright file="LoginAuthentication.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Data;
    using Data.DataHandling;
    using Data.Exceptions;

    public class LoginAuthentication
    {
        private string userName;
        private User user;
        private UserDataHandler userDataHandler;

        public LoginAuthentication()
        {
            this.userDataHandler = new UserDataHandler();
        }

        public string UserName
        {
            get
            {
                return this.user.UserName;
            }
        }

        public bool CheckLoginCredentials(string user, string password)
        {
            if (this.UserExists(user))
            {
                string inputPassword = this.EncodePassword(password);
                if (inputPassword.Equals(this.user.UserPassword))
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

        public bool IsClient()
        {
            if (this.user.IsClient == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string EncodePassword(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA256").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        private bool UserExists(string u)
        {
            this.userName = u;

            try
            {
                this.user = (User)this.userDataHandler.Select(UserAttributeType.UserName, this.userName);
                return true;
            }
            catch (EntryNotFoundException)
            {
                return false;
            }
        }
    }
}
