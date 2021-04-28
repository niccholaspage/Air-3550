// MainPage.xaml.cs - Air 3550 Project
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
 * The main page of our program, which will be shown
 * as the first page of the interface when the program
 * first launches.
 */

using System;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Windows.System;

namespace Air_3550.Views
{
    public sealed partial class MainPage : Page
    {
        // A class of parameters that other pages
        // can construct when navigating to this
        // page.
        public class Params
        {
            // For the main window, we just have a boolean
            // that when turned on, will show the purchase
            // confirmation below the search bar.
            public bool ShowPurchaseConfirmation;

            public Params(bool showPurchaseConfirmation)
            {
                ShowPurchaseConfirmation = showPurchaseConfirmation;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            // Register with the Loaded event, and focus
            // the departure city box to make it so the user
            // can immediately type in their departure city.
            this.Loaded += (_, __) =>
            {
                DepartureCityBox.Focus(FocusState.Programmatic);
            };

            // We cap out the min/max dates, as users can only book
            // flights starting from today's date and ending six
            // months in advance.
            DepartureDatePicker.MinDate = DateTime.Now;
            DepartureDatePicker.MaxDate = DateTime.Now.AddMonths(6);

            ReturnDatePicker.MinDate = DateTime.Now;
            ReturnDatePicker.MaxDate = DateTime.Now.AddMonths(6);
        }

        readonly MainViewModel ViewModel = new(); // Construct the view model.

        // When this page is navigated to, we need to check
        // the navigation parameter.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // If the parameter is our special params object
            // and the purchase confirmation is turned on,
            // we show it, then set the confirmation to false
            // to avoid showing it on future navigations to
            // this page.
            if (e.Parameter is Params pageParams && pageParams.ShowPurchaseConfirmation)
            {
                BookedFlightsInfoBar.IsOpen = true;

                pageParams.ShowPurchaseConfirmation = false;
            }
        }

        // We handle the search here by deferring to the
        // view model. If it returns true, we take the
        // user to the flight search page, passing in
        // the departure and destination cities/dates.
        private void handleSearch()
        {
            if (ViewModel.CheckAndGiveFeedbackOnSearch())
            {
                var departureDate = ViewModel.DepartureDate.Value.Date;
                DateTime? returnDate = ViewModel.ReturnDate?.Date;
                Frame.Navigate(typeof(FlightSearchPage), new FlightSearchPage.Params(ViewModel.DepartureAirportId.Value, ViewModel.DestinationAirportId.Value, departureDate, returnDate, null));
            }
        }

        // When the user clicks the search
        // button, we simply  handle the
        // search.
        private void SearchButton_Click(object _, RoutedEventArgs __)
        {
            handleSearch();
        }

        // When the trip type radio button changes, we check
        // whether the user selected round trip or one-way,
        // and update the view model disable/enable the return
        // date picker as needed.
        private void TripTypeRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is RadioButtons radioButtons)
            {
                // Round trip
                if (radioButtons.SelectedIndex == 0)
                {
                    ViewModel.IsRoundTrip = true;
                    ReturnDatePicker.IsEnabled = true;
                }
                else // One-way
                {
                    ViewModel.IsRoundTrip = false;
                    ViewModel.ReturnDate = null;
                    ReturnDatePicker.IsEnabled = false;
                }
            }
        }

        // When the user presses the enter
        // key, we simply  handle the
        // search.
        private void StackPanel_KeyDown(object _, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                handleSearch();
            }
        }
    }
}
