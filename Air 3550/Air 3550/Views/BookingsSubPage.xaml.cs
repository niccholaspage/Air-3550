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

/**
 * The bookings subpage is a tab that appears in the
 * account info page that shows a customer the bookings
 * they have made and allows them to view tickets for
 * each booking, as well as cancel tickets and get
 * their boarding passes. It is only shown to
 * customers.
 */

using System;
using Air_3550.Models;
using Air_3550.Repository;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Air_3550.Views
{
    public sealed partial class BookingsSubPage : Page
    {
        // On construction of the booking subpage,
        // we register with the loaded event and
        // tell the view model to get the bookings.
        public BookingsSubPage()
        {
            this.InitializeComponent();
            this.Loaded += async (_, __) => await ViewModel.GetBookings();
        }

        readonly BookingsViewModel ViewModel = new(); // Construct the view model.

        // When the boarding pass button is clicked,
        // we want to display the user their boarding
        // pass.
        private async void BoardingPass_Click(object sender, RoutedEventArgs e)
        {
            // We first get the button
            // that triggered this click.
            Button button = (Button)sender;

            // In the XAML, we bound the ticket of each
            // list item to the button's command parameter,
            // so we retrieve it here.
            var ticket = (Ticket)button.CommandParameter;

            // We get a new database context, then construct
            // a boarding pass dialog, passing in the ticket,
            // customer name, and login ID, then show the
            // dialog to the user.
            using var db = new AirContext();
            BoardingPassDialog dialog = new(ticket, ViewModel.CustomerName, ViewModel.LoginId);
            // this is needed to avoid an exception due to a WinUI issue.
            dialog.XamlRoot = this.Content.XamlRoot;
            await dialog.ShowAsync();
        }

        // This is triggered when the cancel booking button inside of
        // the button flyout is clicked. This tells the view model
        // to cancel the booking.
        private async void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;                            // We grab the button that sent this event,
            var flyout = (Flyout)button.FindName("CancelFlyout");   // get its flyout,
            flyout.Hide();                                          // and hide it.


            // In the XAML, we bound the booking of each
            // list item to the button's command parameter,
            // so we retrieve it here.
            var booking = (Booking)button.CommandParameter;

            // We grab the index of the booking
            // we are currently viewing and save
            // it to a variable.
            var index = BookingsListView.SelectedIndex;

            BookingsListView.SelectedIndex = -1;    // We then unselect the booking,

            await ViewModel.CancelBooking(booking); // cancel it,
            BookingsListView.SelectedIndex = index; // and finally reselect it, forcing the UI to update.
        }
    }
}
