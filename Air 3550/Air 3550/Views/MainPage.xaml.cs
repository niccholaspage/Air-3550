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

using System;
using Air_3550.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Air_3550.Views
{
    public sealed partial class MainPage : Page
    {
        public class Params
        {
            public bool ShowPurchaseConfirmation;

            public Params(bool showPurchaseConfirmation)
            {
                ShowPurchaseConfirmation = showPurchaseConfirmation;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += (_, __) =>
            {
                DepartureCityBox.Focus(FocusState.Programmatic);
            };

            DepartureDatePicker.MinDate = DateTime.Now;
            DepartureDatePicker.MaxDate = DateTime.Now.AddMonths(6);

            ReturnDatePicker.MinDate = DateTime.Now;
            ReturnDatePicker.MaxDate = DateTime.Now.AddMonths(6);
        }

        readonly MainViewModel ViewModel = new();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Params pageParams && pageParams.ShowPurchaseConfirmation)
            {
                BookedFlightsInfoBar.IsOpen = true;

                pageParams.ShowPurchaseConfirmation = false;
            }
        }

        private void handleSearch()
        {
            if (ViewModel.CheckAndGiveFeedbackOnSearch())
            {
                var departureDate = ViewModel.DepartureDate.Value.Date;
                DateTime? returnDate = ViewModel.ReturnDate?.Date;
                Frame.Navigate(typeof(FlightSearchPage), new FlightSearchPage.Params(ViewModel.DepartureAirportId.Value, ViewModel.DestinationAirportId.Value, departureDate, returnDate, null));
            }
        }

        private void SearchButton_Click(object _, RoutedEventArgs __)
        {
            handleSearch();
        }

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

        private void StackPanel_KeyDown(object _, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                handleSearch();
            }
        }
    }
}
