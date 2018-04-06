using MahApps.Metro.Controls;
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
using Data.DataHandling;
using Data.Exceptions;
using Data;
using BusinessLogic;
using MahApps.Metro.Controls.Dialogs;

namespace CarRentalManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool passwordValid;
        ViewModel VM;
        public MainWindow()
        {
            this.InitializeComponent();
            VM = new ViewModel();
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
            ClientWindow clientWindow = new ClientWindow();
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
            if (loginAuthentication.CheckLoginCredentials(LoginName_TextBox.Text, LoginPassword_TextBox.Password) == true)
            {
                await this.ShowMessageAsync("Login message", "Successfull");
                if (loginAuthentication.IsClient() == true)
                {
                    LoginName_TextBox.Text = "";
                    LoginPassword_TextBox.Password = "";
                    ClientWindow cw = new ClientWindow();
                    cw.ShowDialog();
                }
                else
                {
                    LoginName_TextBox.Text = "";
                    LoginPassword_TextBox.Password = "";
                    AdminWindow aw = new AdminWindow();
                    aw.ShowDialog();
                }
            }
            else
            {
                LoginName_TextBox.Text = "";
                LoginPassword_TextBox.Password = "";
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
                if (UserType_ToggleSwitch.IsChecked == true)
                {
                    usertype = "Y";
                    usertypename = "client";
                }
                else
                {
                    usertype = "N";
                    usertypename = "admin";
                }
                if (Name_Textbox.Text.Length < 5 && Fullname_Textbox.Text.Length < 5 && Address_Textbox.Text.Length < 5 && Email_Textbox.Text.Length < 5)
                {
                    Pw_Textbox.Clear();
                    await this.ShowMessageAsync("Registration message", "Unsuccessfull, too short, min. 5 char in every field");
                }
                else
                {
                    userClientRegistration.AddNewUser(Name_Textbox.Text, Fullname_Textbox.Text, Pw_Textbox.Password, Address_Textbox.Text, Email_Textbox.Text, usertype);
                    //Registration_Grid.Children.Clear();
                    Name_Textbox.Text = "";
                    Fullname_Textbox.Text = "";
                    Pw_Textbox.Password = "";
                    Address_Textbox.Text = "";
                    Email_Textbox.Text = "";
                    await this.ShowMessageAsync("Registration message", Name_Textbox.Text + " user is created as " + usertypename);
                }
            }
        }
        /// <summary>
        /// This method checks after every change in the PasswordBox, whether it is valid or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}
