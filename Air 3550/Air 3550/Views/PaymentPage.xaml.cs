// PaymentPage.xaml.cs - Air 3550 Project
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
 * This page shows the customer the
 * departure and potentially return
 * flight paths they have chosen to
 * buy, displaying the total and
 * allowing them to choose their
 * payment method to purchase them.
 */

using System;
using Air_3550.ViewModels;
using Database.Util;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Air_3550.Views
{
    public sealed partial class PaymentPage : Page
    {
        private Params pageParams;

        //Params class to allow the passing of params in 
        //through the OnNavigatedTo. We store the departure
        // and return flight path with date.
        public class Params
        {
            public FlightPathWithDate DepartingFlightPathWithDate;
            public FlightPathWithDate ReturnFlightPathWithDate;

            public Params(FlightPathWithDate departingFlightPathWithDate, FlightPathWithDate returnFlightPathWithDate)
            {
                DepartingFlightPathWithDate = departingFlightPathWithDate;
                ReturnFlightPathWithDate = returnFlightPathWithDate;
            }
        }


        // On construction, we register the Loaded event
        // and fetch the account balance and reward points
        // to display it in the UI.
        public PaymentPage()
        {
            this.InitializeComponent();

            this.Loaded += async (_, __) => await ViewModel.FetchBalances();
        }

        // Returns the cost of the flight paths
        // in a string format to be displayed
        // to the user.
        public string GetFormattedTotalCost()
        {
            return ViewModel.TotalCost.FormatAsMoney();
        }

        readonly PaymentViewModel ViewModel = new(); // Construct the view model

        // OnNavigatedTo is overrided to set the 
        // params of the PaymentPage so it has
        // the information from the previous flight
        // search page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            pageParams = e.Parameter as Params;

            ViewModel.DepartingFlightPathWithDate = pageParams.DepartingFlightPathWithDate;
            ViewModel.ReturnFlightPathWithDate = pageParams.ReturnFlightPathWithDate;

            DepartureFlightPathControl.DataContext = ViewModel.DepartingFlightPathWithDate;

            if (ViewModel.ReturnFlightPathWithDate != null)
            {
                ReturnFlightPathControl.DataContext = ViewModel.ReturnFlightPathWithDate;
            }
        }

        // On click of the purchase button,
        // the tickets are purchased based on
        // the scheduled flights across both
        // flight paths.
        private async void PurchaseButton_Click(object _, RoutedEventArgs __)
        {
            if (await ViewModel.PurchaseTrip())
            {
                //Navigate back to main page
                Frame.Navigate(typeof(MainPage), new MainPage.Params(true));

                //Clears the stack for the back button
                Frame.BackStack.Clear();
            }
        }
    }
}
