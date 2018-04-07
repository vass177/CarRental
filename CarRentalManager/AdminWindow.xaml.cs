using CarRentalManager.Controls;
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
using System.Windows.Shapes;
using BusinessLogic;

namespace CarRentalManager
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class AdminWindow : MetroWindow
    {
        private AdminWindowViewModel adminWindowViewModel;

        public AdminWindow()
        {
            this.InitializeComponent();
        }

        private void MetroWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.button1.Height = this.adminGrid.ActualHeight / 4;
            this.button2.Height = this.adminGrid.ActualHeight / 4;
            this.button3.Height = this.adminGrid.ActualHeight / 4;
            this.button4.Height = this.adminGrid.ActualHeight / 4 * 1.1;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.adminWindowViewModel = new AdminWindowViewModel();
            this.DataContext = this.adminWindowViewModel;
        }

        private void DeleteClientClick(object sender, RoutedEventArgs e)
        {
            this.adminWindowViewModel.DeleteClient();
        }

        private void UpdateClientClick(object sender, RoutedEventArgs e)
        {
            this.adminWindowViewModel.UpdateClient();
        }
    }
}
