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
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.SqlClient;
using System.IO;

namespace CarRentalManager
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class AdminWindow : MetroWindow
    {
        private AdminWindowViewModel adminWindowViewModel;
        public SeriesCollection mySeries { get; set; }
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
            LoadAdminStatistics();
            //MessageBox.Show(File.ReadAllLines("C:/Users/Franciska/source/repos/oenik_szt2_2018tavasz_pm4j60_gk607o_ilnrw96Q/ListCarHP.sql").ToString());

            this.adminWindowViewModel = new AdminWindowViewModel();

            SeriesCollection mySeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "dummy",
                    Values = new ChartValues<int>{10, 20, 5, 2, 30}
                }
            };
            IncomeChart.Series = mySeries;

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

        private void MetroTabControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.tabControl.TabStripMargin = new Thickness(0, 0, 20, 0);
        }

        private void MetroTabControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.tabControl.TabStripMargin = new Thickness(0, 0, 0, 0);
        }

        private void Statistics_Loaded(object sender, RoutedEventArgs e)
        {
            adminWindowViewModel.CarSumma = 0;
            foreach (var item in adminWindowViewModel.Cars)
            {
                adminWindowViewModel.CarSumma++;
            }
            //MessageBox.Show(adminWindowViewModel.CarSumma.ToString());
            //CarSumma_Label.Content = adminWindowViewModel.CarSumma.ToString();
        }

        public void LoadAdminStatistics()
        {

        }
    }
}
