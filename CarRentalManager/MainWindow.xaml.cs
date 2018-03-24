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

            UserClientRegistration registration = new UserClientRegistration();
            //registration.AddNewUser("bnagy", "Nagy Béla", "admin", "1054 Budapest, Váci 7", "bnagy@gmail.com", "Y");

            //testing Login validation
            LoginAuthentication loginAuthentication = new LoginAuthentication();
            Console.WriteLine("Ez rossz kell legyen: "+loginAuthentication.CheckLoginCredentials("akovacs", "nemjojelszo"));
            Console.WriteLine("Ez jó kell legyen: "+loginAuthentication.CheckLoginCredentials("akovacs", "Szofttech1?"));
            Console.WriteLine("Ez jó kell legyen: " + loginAuthentication.CheckLoginCredentials("okiss", "Proba123%%"));
            Console.WriteLine("Ez rossz kell legyen, mert nincs ilyen user: " + loginAuthentication.CheckLoginCredentials("iproba", "nemjojelszo"));
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    ClientWindow clientWindow = new ClientWindow();
        //    clientWindow.Show();
        //}

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
}
