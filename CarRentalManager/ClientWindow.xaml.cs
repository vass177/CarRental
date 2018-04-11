﻿using CarRentalManager.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

namespace CarRentalManager
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : MetroWindow
    {
        private ClientWindowViewModel clientWindowViewModel;
        private string userName;

        public ClientWindow(string userName)
        {
            this.InitializeComponent();

            this.userName = userName;
        }

        private void Client_TabButton(object sender, RoutedEventArgs e)
        {
            NewRentalTabControl.SelectedItem = CarSelect_TabItem;
        }

        private void Car_TabButton(object sender, RoutedEventArgs e)
        {
            NewRentalTabControl.SelectedItem = Date_TabItem;
        }

        private void Date_TabButton(object sender, RoutedEventArgs e)
        {
            NewRentalTabControl.SelectedItem = Services_TabItem;
        }

        private void Service_TabButton(object sender, RoutedEventArgs e)
        {
            NewRentalTabControl.SelectedItem = Confirm_Tabitem;
        }

        private async void Confirm_TabButtonAsync(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("Confirm message", "Successfull confirm");
        }
        
        private void MetroWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.button1.Height = this.myGrid.ActualHeight / 4;
            this.button2.Height = this.myGrid.ActualHeight / 4;
            this.button3.Height = this.myGrid.ActualHeight / 4;
            this.button4.Height = this.myGrid.ActualHeight / 4 * 1.1;
        }

        private void MetroTabControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.tabControl.TabStripMargin = new Thickness(0, 0, 20, 0);
        }

        private void MetroTabControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.tabControl.TabStripMargin = new Thickness(0, 0, 0, 0);
        }

        private void ClientWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.clientWindowViewModel = new ClientWindowViewModel(this.userName);
        }
    }
}
