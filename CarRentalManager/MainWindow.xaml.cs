// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CarRentalManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using BusinessLogic;
    using Data;
    using Data.DataHandling;
    using Data.Exceptions;
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool passwordValid;

        public MainWindow()
        {
            this.InitializeComponent();
            this.passwordValid = false;

            /*CarDataHandler carHander = new CarDataHandler();
            Car c1 = new Car
            {
                CarCapacity = 2,
                CarHorsepower = 70,
                CarMotorcode = "ABGFDGFD3232",
                CarCategory = "A",
                CarPhotoPath = "images/smart.jpg",
                CarQuantity = 5,
                CarRentalPrice = 120000,
                CarType = "Smart",
                CoordLat = 0.136566m,
                CoordLong = 0.122213m
            };
            carHander.Insert(c1);

            IQueryable<Car> carList = (IQueryable<Car>)carHander.GetAll();
            foreach (Car item in carList)
            {
                Console.WriteLine(item.CarType+" "+item.CarHorsepower+" "+item.CarPhotoPath);
            }
            Console.WriteLine(" ");
            Car opel=(Car)carHander.Select(CarAttributeType.CarType, "Opel Astra OPC");
            Console.WriteLine(opel.CarRentalPrice+" "+opel.CarPhotoPath);

            IQueryable<Car> carHPList = (IQueryable<Car>)carHander.SelectMore(CarAttributeType.CarHorsePower, new decimal[] { 60, 154 });
            foreach (Car item in carHPList)
            {
                Console.WriteLine(item.CarType + " " + item.CarHorsepower + " " + item.CarPhotoPath);
            }*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow("valaki");
            clientWindow.Show();
        }

        private bool ContainsLowerCase(string text)
        {
            foreach (char c in text)
            {
                int asciiValue = (int)c;
                if (asciiValue > 96 && asciiValue < 123)
                {
                    return true;
                }
            }

            return false;
        }

        private bool ContainsUpperCase(string text)
        {
            foreach (char c in text)
            {
                int asciiValue = (int)c;
                if (asciiValue > 64 && asciiValue < 91)
                {
                    return true;
                }
            }

            return false;
        }

        private bool ContainsSpecialChar(string text)
        {
            foreach (char c in text)
            {
                int asciiValue = (int)c;
                if ((asciiValue > 32 && asciiValue < 48) || (asciiValue > 57 && asciiValue < 65) || (asciiValue > 90 && asciiValue < 97))
                {
                    return true;
                }
            }

            return false;
        }

        private async void Login_ButtonClickAsync(object sender, RoutedEventArgs e)
        {
            LoginAuthentication loginAuthentication = new LoginAuthentication();
            if (loginAuthentication.CheckLoginCredentials(this.LoginName_TextBox.Text, this.LoginPassword_TextBox.Password) == true)
            {
                await this.ShowMessageAsync("Login message", "Successfull");
                if (loginAuthentication.IsClient() == true)
                {
                    this.LoginName_TextBox.Text = string.Empty;
                    this.LoginPassword_TextBox.Password = string.Empty;
                    ClientWindow cw = new ClientWindow(loginAuthentication.UserName);
                    cw.ShowDialog();
                }
                else
                {
                    this.LoginName_TextBox.Text = string.Empty;
                    this.LoginPassword_TextBox.Password = string.Empty;
                    AdminWindow aw = new AdminWindow();
                    aw.ShowDialog();
                }
            }
            else
            {
                this.LoginName_TextBox.Text = string.Empty;
                this.LoginPassword_TextBox.Password = string.Empty;
                await this.ShowMessageAsync("Login message", "Unsuccessfull");
            }
        }

        private async void Registration_ButtonAsync(object sender, RoutedEventArgs e)
        {
            if (this.passwordValid)
            {
                UserClientRegistration userClientRegistration = new UserClientRegistration();
                string usertype;
                string usertypename;
                if (this.UserType_ToggleSwitch.IsChecked == true)
                {
                    usertype = "Y";
                    usertypename = "client";
                }
                else
                {
                    usertype = "N";
                    usertypename = "admin";
                }

                if (this.Name_Textbox.Text.Length < 5 && this.Fullname_Textbox.Text.Length < 5 && this.Address_Textbox.Text.Length < 5 && this.Email_Textbox.Text.Length < 5)
                {
                    this.Pw_Textbox.Clear();
                    await this.ShowMessageAsync("Registration message", "Unsuccessfull, too short, min. 5 char in every field");
                }
                else
                {
                    userClientRegistration.AddNewUser(this.Name_Textbox.Text, this.Fullname_Textbox.Text, this.Pw_Textbox.Password, this.Address_Textbox.Text, this.Email_Textbox.Text, usertype);
                    this.Name_Textbox.Text = string.Empty;
                    this.Fullname_Textbox.Text = string.Empty;
                    this.Pw_Textbox.Password = string.Empty;
                    this.Address_Textbox.Text = string.Empty;
                    this.Email_Textbox.Text = string.Empty;
                    await this.ShowMessageAsync("Registration message", this.Name_Textbox.Text + " user is created as " + usertypename);
                }
            }
        }

        /// <summary>
        /// This method checks after every change in the PasswordBox, whether it is valid or not
        /// </summary>
        /// <param name="sender">Name of sender</param>
        /// <param name="e">Name of e</param>
        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            string password = passwordBox.Password;
            if (password.Length < 10)
            {
                passwordBox.Background = Brushes.Yellow;
                this.passwordValid = false;
            }
            else
            {
                if (this.ContainsLowerCase(password) && this.ContainsUpperCase(password) && this.ContainsSpecialChar(password))
                {
                    passwordBox.Background = Brushes.White;
                    this.passwordValid = true;
                }
                else
                {
                    passwordBox.Background = Brushes.Yellow;
                    this.passwordValid = false;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
        }
    }
}
