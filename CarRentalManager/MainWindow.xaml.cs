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

            //testing Login validation
            LoginAuthentication loginAuthentication = new LoginAuthentication();
            Console.WriteLine("Ez rossz kell legyen: "+loginAuthentication.CheckLoginCredentials("akovacs", "nemjojelszo"));
            Console.WriteLine("Ez jó kell legyen: "+loginAuthentication.CheckLoginCredentials("akovacs", "Szofttech1?"));
            Console.WriteLine("Ez jó kell legyen: " + loginAuthentication.CheckLoginCredentials("okiss", "Proba123%%"));
            Console.WriteLine("Ez rossz kell legyen, mert nincs ilyen user: " + loginAuthentication.CheckLoginCredentials("iproba", "nemjojelszo"));
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
