// BookingsSubPage.xaml.cs - Air 3550 Project
//
// This program emulates a flight reservation system for a new airline,
// allowing customers to book and manage trips,as well as allowing employees
// to update the available flights and view stats on them.
//
// Authors:		Nicholas Nassar, Jacob Hammitte, Nikesh Dhital
// Class:		EECS 3550-001 Software Engineering, Spring 2021
// Instructor:	Dr. Thomas
// Date:		April 28, 2021
// Copyright:	Copyright 2021 by Nicholas Nassar, Jacob Hammitte, and Nikesh Dhital. All rights reserved.

using System;
using System.Linq;
using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BookingSubPage : Page
    {
        public BookingSubPage()
        {
            this.InitializeComponent();
            this.Loaded += async (_, __) => await ViewModel.GetBookings();
        }

        readonly BookingsViewModel ViewModel = new();

        private async void BoardingPass_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var ticket = (Ticket)button.CommandParameter;

            using (var db = new AirContext())
            {
                BoardingPassDialog dialog1 = new(ticket, ViewModel.CustomerName);
                dialog1.XamlRoot = this.Content.XamlRoot;
                await dialog1.ShowAsync();
            }
        }

        private async void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var flyout = (Flyout)button.FindName("CancelFlyout");
            flyout.Hide();

            var booking = (Booking)button.CommandParameter;
            await ViewModel.CancelBooking(booking);
            booking.Tickets.First().IsCanceled = true;
        }
    }
}
