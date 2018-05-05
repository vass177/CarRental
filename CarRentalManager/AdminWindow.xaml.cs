// <copyright file="AdminWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CarRentalManager
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
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
    using CarRentalManager.Controls;
    using LiveCharts;
    using LiveCharts.Wpf;
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using Microsoft.Maps.MapControl.WPF;

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

        public SeriesCollection MySeries { get; set; }

        /// <summary>
        /// Loads admin window statistics
        /// </summary>
        public void LoadAdminStatistics()
        {
            Dictionary<string, int> myCarsList = new Dictionary<string, int>();
            myCarsList = this.adminWindowViewModel.SummaCars();

            SeriesCollection myPieCollection = new SeriesCollection();
            foreach (var item in myCarsList)
            {
                if (myCarsList[item.Key] != 0)
                {
                    myPieCollection.Add(new PieSeries { Title = item.Key, Values = new ChartValues<int> { myCarsList[item.Key] } });
                }
            }

            this.UtilizationCarsChart.Series = myPieCollection;

            Dictionary<string, int> myServicesList = new Dictionary<string, int>();
            myServicesList = this.adminWindowViewModel.SummaServices();

            SeriesCollection myPieCollection2 = new SeriesCollection();
            foreach (var item in myServicesList)
            {
                if (myServicesList[item.Key] != 0)
                {
                    myPieCollection2.Add(new PieSeries { Title = item.Key, Values = new ChartValues<int> { myServicesList[item.Key] } });
                }
            }

            this.UtilizationServicesChart.Series = myPieCollection2;

            List<int> incomeData = this.adminWindowViewModel.GetIncomeStatistics();

            SeriesCollection mySeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<int>(incomeData),
                    Title = "Income"
                }
            };
            this.axisX.Labels = new string[] { "2014", "2015", "2016", "2017", "2018" };
            this.IncomeChart.Series = mySeries;

            List<decimal> carCoordinates = this.adminWindowViewModel.GetCarCoordinates();
            for (int i = 0; i < carCoordinates.Count() - 1; i += 2)
            {
                // The pushpin to add to the map.
                Pushpin pin = new Pushpin();
                pin.Location = new Location((double)carCoordinates[i], (double)carCoordinates[i + 1]);

                // Adds the pushpin to the map.
                this.carMap.Children.Add(pin);
            }
        }

        /// <summary>
        /// resizing buttons after changing window size
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private void MetroWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.button1.Height = this.adminGrid.ActualHeight / 4;
            this.button2.Height = this.adminGrid.ActualHeight / 4;
            this.button3.Height = this.adminGrid.ActualHeight / 4;
            this.button4.Height = this.adminGrid.ActualHeight / 4 * 1.1;
        }

        /// <summary>
        /// initializing the view model
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show(File.ReadAllLines("C:/Users/Franciska/source/repos/oenik_szt2_2018tavasz_pm4j60_gk607o_ilnrw96Q/ListCarHP.sql").ToString());
            this.adminWindowViewModel = new AdminWindowViewModel();

            this.LoadAdminStatistics();

            this.DataContext = this.adminWindowViewModel;
        }

        /// <summary>
        /// button click to delete client
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private void DeleteClientClick(object sender, RoutedEventArgs e)
        {
            this.adminWindowViewModel.DeleteClient();
        }

        /// <summary>
        /// button click to update client
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private void UpdateClientClick(object sender, RoutedEventArgs e)
        {
            this.adminWindowViewModel.UpdateClient();
        }

        /// <summary>
        /// handling mouseover and mouseleave on side buttons
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private void MetroTabControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.tabControl.TabStripMargin = new Thickness(0, 0, 20, 0);
        }

        /// <summary>
        /// handling mouseover and mouseleave on side buttons
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private void MetroTabControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.tabControl.TabStripMargin = new Thickness(0, 0, 0, 0);
        }

        /// <summary>
        /// ???
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private void Statistics_Loaded(object sender, RoutedEventArgs e)
        {
            this.adminWindowViewModel.CarSumma = 0;
            foreach (var item in this.adminWindowViewModel.Cars)
            {
                this.adminWindowViewModel.CarSumma++;
            }
        }

        /// <summary>
        /// car modification message
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private async void ModifyCar_ClickAsync(object sender, RoutedEventArgs e)
        {
            this.adminWindowViewModel.UpdateCar();
            this.ModifyButton.Visibility = Visibility.Hidden;
            this.DeleteButton.Visibility = Visibility.Hidden;
            await this.ShowMessageAsync("Update car", "Car has been changed...");
        }

        /// <summary>
        /// delete car message
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private async void DeleteCar_ClickAsync(object sender, RoutedEventArgs e)
        {
            this.adminWindowViewModel.DeleteCar();
            this.ModifyButton.Visibility = Visibility.Hidden;
            this.DeleteButton.Visibility = Visibility.Hidden;
            await this.ShowMessageAsync("Delete car", "Car has been deleted...");
        }

        /// <summary>
        /// car listbox selection changed event to show Delete and Modify buttons
        /// </summary>
        /// <param name="sender">...</param>
        /// <param name="e">....</param>
        private void CarsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.carsListBox.SelectedItem != null)
            {
                this.ModifyButton.Visibility = Visibility.Visible;
                this.DeleteButton.Visibility = Visibility.Visible;
            }
        }
    }
}
