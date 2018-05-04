﻿using CarRentalManager.Controls;
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
using Microsoft.Maps.MapControl.WPF;

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
            //MessageBox.Show(File.ReadAllLines("C:/Users/Franciska/source/repos/oenik_szt2_2018tavasz_pm4j60_gk607o_ilnrw96Q/ListCarHP.sql").ToString());

            this.adminWindowViewModel = new AdminWindowViewModel();

            LoadAdminStatistics();

            /*SeriesCollection mySeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "dummy",
                    Values = new ChartValues<int>{10, 20, 5, 2, 30}
                }
            };
            IncomeChart.Series = mySeries;*/

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
            Dictionary<string, int> myCarsList = new Dictionary<string, int>();
            myCarsList = adminWindowViewModel.SummaCars();

            SeriesCollection myPieCollection = new SeriesCollection();
            foreach (var item in myCarsList)
            {
                if (myCarsList[item.Key] != 0 )
                {
                    myPieCollection.Add(new PieSeries { Title = item.Key, Values = new ChartValues<int> { myCarsList[item.Key] } });
                }               
            }           
            UtilizationCarsChart.Series = myPieCollection;


            Dictionary<string, int> myServicesList = new Dictionary<string, int>();
            myServicesList = adminWindowViewModel.SummaServices();
            
            SeriesCollection myPieCollection2 = new SeriesCollection();
            foreach (var item in myServicesList)
            {
                if (myServicesList[item.Key] != 0)
                {
                    myPieCollection2.Add(new PieSeries { Title = item.Key, Values = new ChartValues<int> { myServicesList[item.Key] } });
                }
            }
            UtilizationServicesChart.Series = myPieCollection2;


            List<int> incomeData = this.adminWindowViewModel.getIncomeStatistics();

            LineSeries ls = new LineSeries();
            
            SeriesCollection mySeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<int>(incomeData),
                    Title = "Income"
                }
            };
            axisX.Labels = new string[] { "2014", "2015", "2016", "2017", "2018" };
            IncomeChart.Series = mySeries;

            List<decimal> carCoordinates = this.adminWindowViewModel.GetCarCoordinates();
            for (int i = 0; i < carCoordinates.Count()-1; i+=2)
            {
                // The pushpin to add to the map.
                Pushpin pin = new Pushpin();
                pin.Location = new Location((double)carCoordinates[i], (double)carCoordinates[i+1]);

                // Adds the pushpin to the map.
                carMap.Children.Add(pin);


            }

            /*Location myloc =new Location(42.32460,-071.069970);
            var pushpin = new Pushpin()
            {
                Background = new SolidColorBrush(Color.FromRgb(244,244,32))
            };
            MapLayer.SetPosition(pushpin, myloc);
            carMap.Children.Add(pushpin);*/


        }

        private void ModifyCar_Click(object sender, RoutedEventArgs e)
        {
            this.adminWindowViewModel.UpdateCar();
            ModifyButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
        }

        private void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            this.adminWindowViewModel.DeleteCar();
            ModifyButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
        }

        private void carsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (carsListBox.SelectedItem != null)
            {
                ModifyButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
            }
        }
    }
}
