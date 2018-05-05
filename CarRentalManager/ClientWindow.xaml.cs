// <copyright file="ClientWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CarRentalManager
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using LiveCharts;
    using LiveCharts.Wpf;
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;

    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : MetroWindow
    {
        private ClientWindowViewModel clientWindowViewModel;
        private string userName;
        private bool availableCar;

        public ClientWindow(string userName)
        {
            this.InitializeComponent();

            this.userName = userName;
            this.availableCar = false;
        }

        /// <summary>
        /// Loads client statistics
        /// </summary>
        public void LoadUserStatistics()
        {
            List<int> clientData = this.clientWindowViewModel.GetClientStatistics();

            SeriesCollection mySeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<int>(clientData),
                    Title = "Payments"
                }
            };
            this.axisX.Labels = new string[] { "2014", "2015", "2016", "2017", "2018" };
            this.paymentChart.Series = mySeries;
        }

        /// <summary>
        /// Switches tab to car selection
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void Client_TabButton(object sender, RoutedEventArgs e)
        {
            this.NewRentalTabControl.SelectedItem = this.CarSelect_TabItem;
        }

        /// <summary>
        /// Switches tab to date selection
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void Car_TabButton(object sender, RoutedEventArgs e)
        {
            this.NewRentalTabControl.SelectedItem = this.Date_TabItem;
        }

        /// <summary>
        /// Switches tab to service selection
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void Date_TabButton(object sender, RoutedEventArgs e)
        {
            this.NewRentalTabControl.SelectedItem = this.Services_TabItem;
        }

        /// <summary>
        /// Rental completed message show
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">...</param>
        private async void Confirm_TabButtonAsync(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("Confirm message", "Successfull confirm");
            this.clientWindowViewModel.FinishOrder();
            this.NewRentalTabControl.SelectedItem = this.CarSelect_TabItem;
            this.LoadUserStatistics();
        }

        /// <summary>
        /// Window sized changed event to resize side buttons
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void MetroWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.button1.Height = this.myGrid.ActualHeight / 4;
            this.button2.Height = this.myGrid.ActualHeight / 4;
            this.button3.Height = this.myGrid.ActualHeight / 4;
            this.button4.Height = this.myGrid.ActualHeight / 4 * 1.1;
        }

        /// <summary>
        /// Mouse leave event for side button size change
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void MetroTabControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.tabControl.TabStripMargin = new Thickness(0, 0, 20, 0);
        }

        /// <summary>
        /// Mouse enter event for side button size change
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void MetroTabControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.tabControl.TabStripMargin = new Thickness(0, 0, 0, 0);
        }

        /// <summary>
        /// Window loaded event to initialize viewmodel
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void ClientWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.clientWindowViewModel = new ClientWindowViewModel(this.userName);
            this.DataContext = this.clientWindowViewModel;
            this.LoadUserStatistics();
        }

        /// <summary>
        /// Tab change if car is available
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void ButtonClick_SelectDate(object sender, RoutedEventArgs e)
        {
            if (this.availableCar)
            {
                // calling services tab
                this.NewRentalTabControl.SelectedItem = this.Services_TabItem;
            }
        }

        /// <summary>
        /// Start date selection
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void StartDatePicker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            this.informationLabel.Content = string.Empty;
            this.endDatePicker.SelectedDate = null;
            this.informationLabel.Content = "Select an ending date";
        }

        /// <summary>
        /// End date selection
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void EndDatePicker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.startDatePicker.SelectedDate != null && this.startDatePicker.SelectedDate < this.endDatePicker.SelectedDate)
            {
                this.informationLabel.Content = string.Empty;
                DateTime startDate = (DateTime)this.startDatePicker.SelectedDate;
                DateTime endDate = (DateTime)this.endDatePicker.SelectedDate;

                this.availableCar = this.clientWindowViewModel.CheckDates(startDate, endDate);
                if (this.availableCar)
                {
                    this.informationLabel.Content = "The selected car is available between " + startDate.ToString().Substring(0, 13) + " and " + endDate.ToString().Substring(0, 13);
                }
                else
                {
                    this.informationLabel.Content = "The selected car is NOT available, please select another car";
                }
            }
            else
            {
                if (this.startDatePicker.SelectedDate == null)
                {
                    this.informationLabel.Content = "Select a starting date!";
                }
                else if (this.startDatePicker.SelectedDate < this.endDatePicker.SelectedDate)
                {
                    this.informationLabel.Content = "Start date must be less, than end date";
                }

                this.availableCar = false;
            }
        }

        /// <summary>
        /// Flipview selection change event to select a car
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void CarFlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = this.carFlipView.SelectedIndex;
            Image item = (Image)this.carFlipView.SelectedItem;
            string[] image = item.Source.ToString().Split(';');
            string imageSource = image[1].Substring(10);
            this.clientWindowViewModel.SelectACar(imageSource);
        }

        /// <summary>
        /// Tab change to date selection
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void ButtonClick_SelectCar(object sender, RoutedEventArgs e)
        {
            // calling next tab
            this.NewRentalTabControl.SelectedItem = this.Date_TabItem;
        }

        /// <summary>
        /// Tab change to confirm rental tab
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="e">..</param>
        private void Service_TabButton(object sender, RoutedEventArgs e)
        {
            List<string> services = new List<string>();

            foreach (var item in this.servicesGrid.Children)
            {
                if (item is ToggleSwitch && (bool)(item as ToggleSwitch).IsChecked)
                {
                    string name = (item as ToggleSwitch).Name.Replace('_', ' ');
                    services.Add(name);
                }
            }

            this.clientWindowViewModel.SelectService(services);

            this.NewRentalTabControl.SelectedItem = this.Confirm_Tabitem;
        }
    }
}
