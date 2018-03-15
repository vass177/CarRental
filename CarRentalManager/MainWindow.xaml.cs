using CarRentalManager.Data;
using BusinessLogic.Exceptions;
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
using Data;

namespace CarRentalManager
{
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

            UserDataHandler userHandler = new UserDataHandler();
            var user = new User
            {
                UserName = "atoth",
                UserPassword = "ccc",
                IsClient = "C"
            };
            //userHandler.Insert(user);

            try
            {
                User u1 = (User)userHandler.Select(UserAttributeType.UserName, "atoth");
                //u1.UserPassword = "mégvalami";
                //userHandler.Update(u1);
                //User u2 = (User)userHandler.Select(UserAttributeType.UserName, "atoth");
                Console.WriteLine(u1.UserPassword);

                IQueryable<User> userList = (IQueryable<User>)userHandler.SelectMore(UserAttributeType.IsClient, "M");
                foreach (User item in userList)
                {
                    Console.WriteLine(item.UserName);
                }
            }
            catch (EntryNotFoundException e)
            {
                Console.WriteLine("Entry not found in table Users");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            clientWindow.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox passwordBox = (TextBox)sender;
            string password = passwordBox.Text;
            if (password.Length < 10)
            {
                passwordBox.Foreground = Brushes.Red;
                this.passwordValid = false;
            }
            else
            {
                if (this.ContainsLowerCase(password) && this.ContainsUpperCase(password) && this.ContainsSpecialChar(password))
                {
                    passwordBox.Foreground = Brushes.Black;
                    this.passwordValid = true;
                }
                else
                {
                    passwordBox.Foreground = Brushes.Red;
                    this.passwordValid = false;
                }
            }
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
    }
}
